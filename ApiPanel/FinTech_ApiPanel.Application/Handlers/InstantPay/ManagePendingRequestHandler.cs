using FinTech_ApiPanel.Application.Abstraction.ITokenServices;
using FinTech_ApiPanel.Application.Commands.InstantPay;
using FinTech_ApiPanel.Application.Common;
using FinTech_ApiPanel.Domain.DTOs.GoterPay;
using FinTech_ApiPanel.Domain.DTOs.InstantPay;
using FinTech_ApiPanel.Domain.Entities.TransactionLogs;
using FinTech_ApiPanel.Domain.Entities.UserWallets;
using FinTech_ApiPanel.Domain.Interfaces.IFinancialComponentMasters;
using FinTech_ApiPanel.Domain.Interfaces.IOperatorMasters;
using FinTech_ApiPanel.Domain.Interfaces.ITransactionLogs;
using FinTech_ApiPanel.Domain.Interfaces.IUserFinancialComponents;
using FinTech_ApiPanel.Domain.Interfaces.IUserWallets;
using FinTech_ApiPanel.Domain.Shared.Enums;
using MediatR;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using System.Transactions;

namespace FinTech_ApiPanel.Application.Handlers.InstantPay
{
    public class ManagePendingRequestHandler : IRequestHandler<ManagePendingRequestCommand, ApiResponse>
    {
        private readonly ITransactionLogRepository _transactionLogRepository;
        private readonly ITokenService _tokenService;
        public readonly IWalletRepository _walletRepository;
        private readonly IUserFinancialComponentRepository _userFinancialComponentRepository;
        private readonly IFinancialComponentMasterRepository _financialComponentMasterRepository;
        private readonly IOperatorRepository _operatorRepository;

        public ManagePendingRequestHandler(ITransactionLogRepository transactionLogRepository,
            ITokenService tokenService,
            IWalletRepository walletRepository,
            IUserFinancialComponentRepository userFinancialComponentRepository,
            IFinancialComponentMasterRepository financialComponentMasterRepository,
            IOperatorRepository operatorRepository)
        {
            _transactionLogRepository = transactionLogRepository;
            _tokenService = tokenService;
            _walletRepository = walletRepository;
            _userFinancialComponentRepository = userFinancialComponentRepository;
            _financialComponentMasterRepository = financialComponentMasterRepository;
            _operatorRepository = operatorRepository;
        }

        public async Task<ApiResponse> Handle(ManagePendingRequestCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var loggedInUser = _tokenService.GetLoggedInUserinfo();
                if (loggedInUser == null)
                    return ApiResponse.UnauthorizedResponse("Access denied.");

                var transaction = await _transactionLogRepository.GetByIdAsync(request.TransactionId);
                if (transaction == null)
                    return ApiResponse.NotFoundResponse("Transaction not found.");

                var adminWallet = await _walletRepository.GetByUserIdAsync(loggedInUser.UserId);
                var userWallet = await _walletRepository.GetByUserIdAsync(transaction.UserId);
                if (userWallet == null || adminWallet == null)
                    return ApiResponse.BadRequestResponse("Wallets not found.");

                if (transaction.Status != (byte)Status.Pending)
                    return ApiResponse.BadRequestResponse("Only pending transactions can be updated.");

                if (request.Status != (byte)Status.Success && request.Status != (byte)Status.Failed)
                    return ApiResponse.BadRequestResponse("Invalid status. Only 'Success' or 'Failed' statuses are allowed.");

                using (var scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
                {
                    if (request.Status == (byte)Status.Success)
                    {
                        transaction.Status = (byte)Status.Success;
                        transaction.Remark = "Transaction marked success.";
                        await _transactionLogRepository.UpdateStatusAsync(transaction);

                        // Apply wallet operations depending on log type
                        if (transaction.LogType == (byte)LogType.Recharge)
                            await LogTransactionForRecharge(transaction, adminWallet, userWallet);
                        else if (transaction.LogType == (byte)LogType.DMTTransaction_IPay)
                            await LogTransactionForDMT(transaction, adminWallet, userWallet);
                        else if (transaction.LogType == (byte)LogType.AEPSWithdrawal_IPay)
                            await LogTransactionForCW(transaction, adminWallet, userWallet);
                        else if (transaction.LogType == (byte)LogType.AEPSDeposit_IPay)
                            await LogTransactionForCD(transaction, adminWallet, userWallet);
                    }
                    else if (transaction.LogType != (byte)LogType.AEPSWithdrawal_IPay)
                    {
                        transaction.Status = (byte)Status.Failed;
                        transaction.Remark = "Transaction marked failed.";
                        await _transactionLogRepository.UpdateStatusAsync(transaction);

                        // Refund wallet
                        userWallet.TotalBalance += (decimal)transaction.Amount;
                        await _walletRepository.UpdateAsync(userWallet);

                        var refundLog = new TransactionLog
                        {
                            LogType = (byte)LogType.Refund,
                            Amount = transaction.Amount,
                            AuditType = (byte)AuditType.Credit,
                            CreatedBy = loggedInUser.UserId,
                            ReferenceId = transaction.Id.ToString(),
                            MobileNumber = transaction.MobileNumber,
                            Status = (byte)Status.Success,
                            Remark = $"Refund processed for transaction #{transaction.Id} (Amount: {transaction.Amount})",
                            RefUserId = loggedInUser.UserId,
                            WalletUpdated = true,
                            RemainingAmount = userWallet.TotalBalance // snapshot after refund
                        };

                        await _transactionLogRepository.AddAsync(refundLog);
                    }

                    scope.Complete();
                }

                return ApiResponse.SuccessResponse("Transaction status updated successfully.");
            }
            catch (Exception ex)
            {
                return ApiResponse.BadRequestResponse($"An error occurred while updating the transaction status: {ex.Message}");
            }
        }

        private async Task LogTransactionForRecharge(TransactionLog transaction, Wallet adminWallet, Wallet userWallet)
        {
            try
            {
                await UpdateLogStatus(transaction.Id.ToString());

                var response = JsonConvert.DeserializeObject<RechargeDto>(transaction.Ipay_Response);

                var operatorInfo = await _operatorRepository.GetByCodeAsync(response.Data.Operator);
                if (operatorInfo == null)
                    throw new Exception("Invalid operator code.");

                var transactionValue = decimal.Parse(response.Data.TransactionValue);
                var payableValue = response.Data.PayableValue;
                decimal adminCommission = decimal.Parse(response.Data.Commission);

                byte subService = 0;
                if (operatorInfo.Type == (byte)RechargeType.Prepaid)
                    subService = (byte)FinancialComponenSubService.Prepaid;
                else if (operatorInfo.Type == (byte)RechargeType.Postpaid)
                    subService = (byte)FinancialComponenSubService.Postpaid;
                else if (operatorInfo.Type == (byte)RechargeType.DTH)
                    subService = (byte)FinancialComponenSubService.DTH;
                else
                    throw new Exception("Invalid operator type for commission calculation.");

                decimal userCommission = await GetComissionForRecharge(userWallet, transactionValue, subService, operatorInfo.Id);

                // Admin commission
                adminWallet.TotalBalance += adminCommission;
                await _transactionLogRepository.AddAsync(new TransactionLog
                {
                    UserId = adminWallet.UserId,
                    Amount = adminCommission,
                    RemainingAmount = adminWallet.TotalBalance,
                    RefUserId = userWallet.UserId,
                    AuditType = (byte)AuditType.Credit,
                    LogType = (byte)LogType.Recharge_Commission,
                    CreatedBy = adminWallet.UserId,
                    Status = (byte)Status.Success,
                    Remark = $"Admin commission credited: {adminCommission:F2} for recharge OrderId: {transaction.Ipay_OrderId}",
                    WalletUpdated = true,
                    ReferenceId = transaction.Id.ToString()
                });

                // Admin user commission debit
                adminWallet.TotalBalance -= userCommission;
                await _transactionLogRepository.AddAsync(new TransactionLog
                {
                    UserId = adminWallet.UserId,
                    RefUserId = userWallet.UserId,
                    Amount = userCommission,
                    RemainingAmount = adminWallet.TotalBalance,
                    AuditType = (byte)AuditType.Debit,
                    LogType = (byte)LogType.Recharge_Commission,
                    CreatedBy = adminWallet.UserId,
                    Status = (byte)Status.Success,
                    Remark = $"User commission share debited: {userCommission:F2} for recharge OrderId: {transaction.Ipay_OrderId}",
                    WalletUpdated = true,
                    ReferenceId = transaction.Id.ToString()
                });
                await _walletRepository.UpdateAsync(adminWallet);


                userWallet.TotalBalance += userCommission;
                await _transactionLogRepository.AddAsync(new TransactionLog
                {
                    UserId = userWallet.UserId,
                    RefUserId = adminWallet.UserId,
                    Amount = userCommission,
                    RemainingAmount = userWallet.TotalBalance,
                    AuditType = (byte)AuditType.Credit,
                    LogType = (byte)LogType.Recharge_Commission,
                    CreatedBy = userWallet.UserId,
                    Status = (byte)Status.Success,
                    Remark = $"Commission credited: {userCommission:F2} for recharge OrderId: {transaction.Ipay_OrderId}",
                    WalletUpdated = true,
                    ReferenceId = transaction.Id.ToString()
                });

                await _walletRepository.UpdateAsync(userWallet);
            }
            catch (Exception)
            {

                throw;
            }
        }

        private async Task<decimal> GetComissionForRecharge(Wallet userWallet, decimal transactionValue, byte subService, long operatorId)
        {
            try
            {
                decimal userCommission = 0;

                // Get user commission slab
                var userFinancialComponents = await _userFinancialComponentRepository.GetByUserIdAsync(userWallet.UserId);
                if (userFinancialComponents != null && userFinancialComponents.Any())
                {
                    var commissionSlabs = userFinancialComponents
                        .Where(x => x.Type == (byte)FinancialComponentType.Commission &&
                                    x.ServiceType == (byte)FinancialComponenService.Recharge &&
                                    x.ServiceSubType == subService &&
                                    x.OperatorId == operatorId);

                    var matchingSlab = commissionSlabs.FirstOrDefault(x =>
                        x.Start_Value <= transactionValue && x.End_Value >= transactionValue);

                    if (matchingSlab != null)
                    {
                        if (matchingSlab.CalculationType == (byte)CalculationType.Percentage)
                            userCommission = Math.Round(transactionValue * matchingSlab.Value / 100, 2);
                        else if (matchingSlab.CalculationType == (byte)CalculationType.Flat)
                            userCommission = matchingSlab.Value;
                    }
                }
                else
                {
                    var financialComponents = await _financialComponentMasterRepository.GetAllAsync();

                    var commissionSlabs = financialComponents
                        .Where(x => x.Type == (byte)FinancialComponentType.Commission &&
                                    x.ServiceType == (byte)FinancialComponenService.Recharge &&
                                    x.ServiceSubType == subService &&
                                    x.OperatorId == operatorId);

                    var matchingSlab = commissionSlabs.FirstOrDefault(x =>
                        x.Start_Value <= transactionValue && x.End_Value >= transactionValue);

                    if (matchingSlab != null)
                    {
                        if (matchingSlab.CalculationType == (byte)CalculationType.Percentage)
                            userCommission = Math.Round(transactionValue * matchingSlab.Value / 100, 2);
                        else if (matchingSlab.CalculationType == (byte)CalculationType.Flat)
                            userCommission = matchingSlab.Value;
                    }
                }
                return userCommission;
            }
            catch (Exception)
            {

                throw;
            }
        }

        private async Task LogTransactionForDMT(TransactionLog transaction, Wallet adminWallet, Wallet userWallet)
        {
            try
            {
                await UpdateLogStatus(transaction.Id.ToString());

                var response = JsonConvert.DeserializeObject<RemitterTransactionDto>(transaction.Ipay_Response);

                var payableValue = decimal.Parse(response.Data.Pool.Amount);
                var transactionValue = decimal.Parse(response.Data.TxnValue);
                decimal adminSurcharge = payableValue - transactionValue;

                var userSurcharge = await GetSurchargeForDMT(userWallet, decimal.Parse(response.Data.Pool.Amount));

                // Admin wallet
                adminWallet.TotalBalance -= adminSurcharge;
                await _transactionLogRepository.AddAsync(new TransactionLog
                {
                    UserId = adminWallet.UserId,
                    Amount = adminSurcharge,
                    RemainingAmount = adminWallet.TotalBalance,
                    RefUserId = userWallet.UserId,
                    AuditType = (byte)AuditType.Debit,
                    LogType = (byte)LogType.DMTSurcharge,
                    CreatedBy = adminWallet.UserId,
                    Status = (byte)Status.Success,
                    Remark = $"Admin Wallet Debit - DMT Surcharge | Amount: {adminSurcharge} | For UserId: {userWallet.UserId} | Ref Txn: {transaction.Id}",
                    WalletUpdated = true,
                    ReferenceId = transaction.Id.ToString()
                });


                userWallet.TotalBalance -= userSurcharge;
                await _transactionLogRepository.AddAsync(new TransactionLog
                {
                    UserId = userWallet.UserId,
                    RefUserId = adminWallet.UserId,
                    Amount = userSurcharge,
                    RemainingAmount = userWallet.TotalBalance,
                    AuditType = (byte)AuditType.Debit,
                    LogType = (byte)LogType.DMTSurcharge,
                    CreatedBy = userWallet.UserId,
                    Status = (byte)Status.Success,
                    Remark = $"User Wallet Debit - DMT Surcharge | Amount: {userSurcharge} | Ref Txn: {transaction.Id}",
                    WalletUpdated = true,
                    ReferenceId = transaction.Id.ToString()
                });


                await _walletRepository.UpdateAsync(userWallet);

                // Admin wallet user commission deduction
                adminWallet.TotalBalance += userSurcharge;
                await _transactionLogRepository.AddAsync(new TransactionLog
                {
                    UserId = adminWallet.UserId,
                    RefUserId = userWallet.UserId,
                    Amount = userSurcharge,
                    RemainingAmount = adminWallet.TotalBalance,
                    AuditType = (byte)AuditType.Credit,
                    LogType = (byte)LogType.DMTSurcharge,
                    CreatedBy = adminWallet.UserId,
                    Status = (byte)Status.Success,
                    Remark = $"Admin Wallet Credit - DMT Surcharge Received from UserId: {userWallet.UserId} | Amount: {userSurcharge} | Ref Txn: {transaction.Id}",
                    WalletUpdated = true,
                    ReferenceId = transaction.Id.ToString()
                });

                await _walletRepository.UpdateAsync(adminWallet);
            }
            catch (Exception)
            {

                throw;
            }
        }

        private async Task<decimal> GetSurchargeForDMT(Wallet userWallet, decimal transactionValue)
        {
            try
            {
                decimal userSurcharge = 0;

                // Get user commission slab
                var userFinancialComponents = await _userFinancialComponentRepository.GetByUserIdAsync(userWallet.UserId);
                if (userFinancialComponents != null && userFinancialComponents.Any())
                {
                    var surchargeSlabs = userFinancialComponents
                        .Where(x => x.Type == (byte)FinancialComponentType.Surcharge &&
                                    x.ServiceType == (byte)FinancialComponenService.DMT);

                    var matchingSlab = surchargeSlabs.FirstOrDefault(x =>
                        x.Start_Value <= transactionValue && x.End_Value >= transactionValue);

                    if (matchingSlab != null)
                    {
                        if (matchingSlab.CalculationType == (byte)CalculationType.Percentage)
                            userSurcharge = Math.Round(transactionValue * matchingSlab.Value / 100, 2);
                        else if (matchingSlab.CalculationType == (byte)CalculationType.Flat)
                            userSurcharge = matchingSlab.Value;
                    }
                }
                else
                {
                    var financialComponents = await _financialComponentMasterRepository.GetAllAsync();

                    var surchargeSlabs = financialComponents
                        .Where(x => x.Type == (byte)FinancialComponentType.Surcharge &&
                                    x.ServiceType == (byte)FinancialComponenService.DMT);

                    var matchingSlab = surchargeSlabs.FirstOrDefault(x =>
                        x.Start_Value <= transactionValue && x.End_Value >= transactionValue);

                    if (matchingSlab != null)
                    {
                        if (matchingSlab.CalculationType == (byte)CalculationType.Percentage)
                            userSurcharge = Math.Round(transactionValue * matchingSlab.Value / 100, 2);
                        else if (matchingSlab.CalculationType == (byte)CalculationType.Flat)
                            userSurcharge = matchingSlab.Value;
                    }
                }
                return userSurcharge;

            }
            catch (Exception)
            {

                throw;
            }
        }

        private async Task LogTransactionForCW(TransactionLog transaction, Wallet adminWallet, Wallet userWallet)
        {
            try
            {
                var response = JsonConvert.DeserializeObject<CashWithdrawalDto>(transaction.Ipay_Response);

                var transactionValue = decimal.Parse(response.Data.TransactionValue);
                var payableValue = decimal.Parse(response.Data.PayableValue);
                decimal adminCommission = payableValue - transactionValue;

                decimal userCommission = await GetComissionForCW(userWallet, transactionValue);

                // Admin commission
                adminWallet.TotalBalance += adminCommission;
                await _transactionLogRepository.AddAsync(new TransactionLog
                {
                    UserId = adminWallet.UserId,
                    Amount = adminCommission,
                    RemainingAmount = adminWallet.TotalBalance,
                    RefUserId = userWallet.UserId,
                    AuditType = (byte)AuditType.Credit,
                    LogType = (byte)LogType.AEPSCommission,
                    CreatedBy = adminWallet.UserId,
                    Status = (byte)Status.Success,
                    Remark = $"Admin Commission Credit for AEPS Withdrawal | Amount: {adminCommission} | Ref User: {userWallet.UserId} | Ref Txn: {transaction.Id}",
                    WalletUpdated = true,
                    ReferenceId = transaction.Id.ToString()
                });

                // Admin user commission debit
                adminWallet.TotalBalance -= userCommission;
                await _transactionLogRepository.AddAsync(new TransactionLog
                {
                    UserId = adminWallet.UserId,
                    RefUserId = userWallet.UserId,
                    Amount = userCommission,
                    RemainingAmount = adminWallet.TotalBalance,
                    AuditType = (byte)AuditType.Debit,
                    LogType = (byte)LogType.AEPSCommission,
                    CreatedBy = adminWallet.UserId,
                    Status = (byte)Status.Success,
                    Remark = $"Admin Wallet Debit for User AEPS Commission Payout | Amount: {userCommission} | Ref User: {userWallet.UserId} | Ref Txn: {transaction.Id}",
                    WalletUpdated = true,
                    ReferenceId = transaction.Id.ToString()
                });
                await _walletRepository.UpdateAsync(adminWallet);

                // User's wallet
                userWallet.TotalBalance += transactionValue;
                await _transactionLogRepository.AddAsync(new TransactionLog
                {
                    UserId = userWallet.UserId,
                    RefUserId = adminWallet.UserId,
                    Amount = transactionValue,
                    RemainingAmount = userWallet.TotalBalance,
                    AuditType = (byte)AuditType.Credit,
                    LogType = (byte)LogType.AEPSWithdrawal,
                    CreatedBy = userWallet.UserId,
                    Status = (byte)Status.Success,
                    Remark = $"User Wallet Credit for AEPS Cash Withdrawal | Amount: {transactionValue} | Ref Txn: {transaction.Id}",
                    WalletUpdated = true,
                    ReferenceId = transaction.Id.ToString()
                });

                userWallet.TotalBalance += userCommission;
                await _transactionLogRepository.AddAsync(new TransactionLog
                {
                    UserId = userWallet.UserId,
                    RefUserId = adminWallet.UserId,
                    Amount = userCommission,
                    RemainingAmount = userWallet.TotalBalance,
                    AuditType = (byte)AuditType.Credit,
                    LogType = (byte)LogType.AEPSCommission,
                    CreatedBy = userWallet.UserId,
                    Status = (byte)Status.Success,
                    Remark = $"User AEPS Commission Credit for Cash Withdrawal | Amount: {userCommission} | Ref Txn: {transaction.Id}",
                    WalletUpdated = true,
                    ReferenceId = transaction.Id.ToString()
                });

                await _walletRepository.UpdateAsync(userWallet);
            }
            catch (Exception)
            {

                throw;
            }
        }

        private async Task<decimal> GetComissionForCW(Wallet userWallet, decimal transactionValue)
        {
            try
            {
                decimal userCommission = 0;

                // Get user commission slab
                var userFinancialComponents = await _userFinancialComponentRepository.GetByUserIdAsync(userWallet.UserId);
                if (userFinancialComponents != null && userFinancialComponents.Any())
                {
                    var commissionSlabs = userFinancialComponents
                        .Where(x => x.Type == (byte)FinancialComponentType.Commission &&
                                    x.ServiceType == (byte)FinancialComponenService.AEPS);

                    var matchingSlab = commissionSlabs.FirstOrDefault(x =>
                        x.Start_Value <= transactionValue && x.End_Value >= transactionValue);

                    if (matchingSlab != null)
                    {
                        if (matchingSlab.CalculationType == (byte)CalculationType.Percentage)
                            userCommission = Math.Round(transactionValue * matchingSlab.Value / 100, 2);
                        else if (matchingSlab.CalculationType == (byte)CalculationType.Flat)
                            userCommission = matchingSlab.Value;
                    }
                }
                else
                {
                    var financialComponents = await _financialComponentMasterRepository.GetAllAsync();

                    var commissionSlabs = financialComponents
                        .Where(x => x.Type == (byte)FinancialComponentType.Commission &&
                                    x.ServiceType == (byte)FinancialComponenService.AEPS);

                    var matchingSlab = commissionSlabs.FirstOrDefault(x =>
                        x.Start_Value <= transactionValue && x.End_Value >= transactionValue);

                    if (matchingSlab != null)
                    {
                        if (matchingSlab.CalculationType == (byte)CalculationType.Percentage)
                            userCommission = Math.Round(transactionValue * matchingSlab.Value / 100, 2);
                        else if (matchingSlab.CalculationType == (byte)CalculationType.Flat)
                            userCommission = matchingSlab.Value;
                    }
                }
                return userCommission;
            }
            catch (Exception)
            {

                throw;
            }
        }

        private async Task LogTransactionForCD(TransactionLog transaction, Wallet adminWallet, Wallet userWallet)
        {
            try
            {
                await UpdateLogStatus(transaction.Id.ToString());

                var depositDto = JsonConvert.DeserializeObject<CashDepositDto>(transaction.Ipay_Response);

                var transactionValue = decimal.Parse(depositDto.Data.TransactionValue);
                var payableValue = decimal.Parse(depositDto.Data.PayableValue);
                decimal adminCommission = transactionValue - payableValue;

                var userCommission = await GetComissionForCD(userWallet, transactionValue);

                // Admin wallet commission credit
                // Admin commission credit
                adminWallet.TotalBalance += adminCommission;
                await _transactionLogRepository.AddAsync(new TransactionLog
                {
                    UserId = adminWallet.UserId,
                    Amount = adminCommission,
                    RemainingAmount = adminWallet.TotalBalance,
                    RefUserId = userWallet.UserId,
                    AuditType = (byte)AuditType.Credit,
                    LogType = (byte)LogType.AEPSCommission,
                    CreatedBy = adminWallet.UserId,
                    Status = (byte)Status.Success,
                    WalletUpdated = true,
                    Remark = $"Admin Wallet Credit - AEPS Deposit Commission | Amount: {adminCommission} | For UserId: {userWallet.UserId} | Ref Txn: {transaction.Id}",
                    ReferenceId = transaction.Id.ToString()
                });

                // Admin commission debit (payout to user)
                adminWallet.TotalBalance -= userCommission;
                await _transactionLogRepository.AddAsync(new TransactionLog
                {
                    UserId = adminWallet.UserId,
                    RefUserId = userWallet.UserId,
                    Amount = userCommission,
                    RemainingAmount = adminWallet.TotalBalance,
                    AuditType = (byte)AuditType.Debit,
                    LogType = (byte)LogType.AEPSCommission,
                    CreatedBy = adminWallet.UserId,
                    Status = (byte)Status.Success,
                    WalletUpdated = true,
                    Remark = $"Admin Wallet Debit - AEPS Deposit User Commission Payout | Amount: {userCommission} | To UserId: {userWallet.UserId} | Ref Txn: {transaction.Id}",
                    ReferenceId = transaction.Id.ToString()
                });
                await _walletRepository.UpdateAsync(adminWallet);

                // User commission credit
                userWallet.TotalBalance += userCommission;
                await _transactionLogRepository.AddAsync(new TransactionLog
                {
                    UserId = userWallet.UserId,
                    RefUserId = adminWallet.UserId,
                    Amount = userCommission,
                    RemainingAmount = userWallet.TotalBalance,
                    AuditType = (byte)AuditType.Credit,
                    LogType = (byte)LogType.AEPSCommission,
                    CreatedBy = userWallet.UserId,
                    Status = (byte)Status.Success,
                    WalletUpdated = true,
                    Remark = $"User Wallet Credit - AEPS Deposit Commission | Amount: {userCommission} | Ref Txn: {transaction.Id}",
                    ReferenceId = transaction.Id.ToString()
                });

                await _walletRepository.UpdateAsync(userWallet);
            }
            catch (Exception)
            {

                throw;
            }
        }

        private async Task<decimal> GetComissionForCD(Wallet userWallet, decimal transactionValue)
        {
            try
            {
                decimal userCommission = 0;

                // Get user commission slab
                var userFinancialComponents = await _userFinancialComponentRepository.GetByUserIdAsync(userWallet.UserId);
                if (userFinancialComponents != null && userFinancialComponents.Any())
                {
                    var commissionSlabs = userFinancialComponents
                        .Where(x => x.Type == (byte)FinancialComponentType.Commission &&
                                    x.ServiceType == (byte)FinancialComponenService.AEPS);

                    var matchingSlab = commissionSlabs.FirstOrDefault(x =>
                        x.Start_Value <= transactionValue && x.End_Value >= transactionValue);

                    if (matchingSlab != null)
                    {
                        if (matchingSlab.CalculationType == (byte)CalculationType.Percentage)
                            userCommission = Math.Round(transactionValue * matchingSlab.Value / 100, 2);
                        else if (matchingSlab.CalculationType == (byte)CalculationType.Flat)
                            userCommission = matchingSlab.Value;
                    }
                }
                else
                {
                    var financialComponents = await _financialComponentMasterRepository.GetAllAsync();

                    var commissionSlabs = financialComponents
                        .Where(x => x.Type == (byte)FinancialComponentType.Commission &&
                                    x.ServiceType == (byte)FinancialComponenService.AEPS);

                    var matchingSlab = commissionSlabs.FirstOrDefault(x =>
                        x.Start_Value <= transactionValue && x.End_Value >= transactionValue);

                    if (matchingSlab != null)
                    {
                        if (matchingSlab.CalculationType == (byte)CalculationType.Percentage)
                            userCommission = Math.Round(transactionValue * matchingSlab.Value / 100, 2);
                        else if (matchingSlab.CalculationType == (byte)CalculationType.Flat)
                            userCommission = matchingSlab.Value;
                    }
                }
                return userCommission;
            }
            catch (Exception)
            {

                throw;
            }
        }

        private async Task UpdateLogStatus(string logId)
        {
            try
            {
                var transactionLogs = await _transactionLogRepository.GetByReferenceIdAsync(logId);

                if (!transactionLogs.Any())
                    throw new Exception("Transaction log not found.");

                var transactionLog = transactionLogs.Where(x => x.Status == (byte)Status.Pending).SingleOrDefault();

                if (transactionLog == null)
                    throw new Exception("No pending transaction log found to update.");

                transactionLog.Status = (byte)Status.Success;
                transactionLog.Remark = "Transaction marked success.";
                await _transactionLogRepository.UpdateStatusAsync(transactionLog);
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
