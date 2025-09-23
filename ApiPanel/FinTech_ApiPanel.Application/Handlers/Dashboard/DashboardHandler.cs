using FinTech_ApiPanel.Application.Abstraction.ITokenServices;
using FinTech_ApiPanel.Application.Common;
using FinTech_ApiPanel.Application.Queries.Dashboard;
using FinTech_ApiPanel.Domain.DTOs.Dashboard;
using FinTech_ApiPanel.Domain.DTOs.UserMasters;
using FinTech_ApiPanel.Domain.Entities.TransactionLogs;
using FinTech_ApiPanel.Domain.Interfaces.ITransactionLogs;
using FinTech_ApiPanel.Domain.Interfaces.IUserWallets;
using FinTech_ApiPanel.Domain.Shared.Enums;
using MediatR;

namespace FinTech_ApiPanel.Application.Handlers.Dashboard
{
    public class DashboardHandler : IRequestHandler<DashboardQuery, ApiResponse<object>>
    {
        public readonly ITokenService tokenService;
        private readonly IWalletRepository _walletRepository;
        private readonly ITransactionLogRepository _transactionLogRepository;

        public DashboardHandler(ITokenService tokenService,
            IWalletRepository walletRepository,
            ITransactionLogRepository transactionLogRepository)
        {
            this.tokenService = tokenService;
            _walletRepository = walletRepository;
            _transactionLogRepository = transactionLogRepository;
        }

        public async Task<ApiResponse<object>> Handle(DashboardQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var loggedInUser = tokenService.GetLoggedInUserinfo();
                if (loggedInUser == null)
                    return ApiResponse<object>.UnauthorizedResponse("User not authorized.");

                if (loggedInUser.IsAdmin)
                {
                    var transactionLogs = await _transactionLogRepository.GetAllAsync();
                    var adminDashboard = await MapAdminDashboard(request, loggedInUser, transactionLogs);
                    return ApiResponse<object>.SuccessResponse(adminDashboard);
                }
                else
                {
                    var transactionLogs = await _transactionLogRepository.GetByUserIdAsync(loggedInUser.UserId);
                    var userDashboard = await MapUserDashboard(request, loggedInUser, transactionLogs);
                    return ApiResponse<object>.SuccessResponse(userDashboard);
                }

            }
            catch (Exception)
            {
                return ApiResponse<object>.BadRequestResponse("An error occurred while processing the request.");
            }

        }

        async Task<AdminDashboardDto> MapAdminDashboard(DashboardQuery request, LoggedInUserDto loggedInUser, IEnumerable<TransactionLog> transactionLogs)
        {
            try
            {
                AdminDashboardDto adminDashboard = new AdminDashboardDto();

                // Get all wallets
                var wallets = await _walletRepository.GetAllAsync();

                // Tab 1

                // Admin wallet balance
                adminDashboard.WalletBalance = wallets.FirstOrDefault(x => x.UserId == loggedInUser.UserId)?.TotalBalance ?? 0;

                // Total user balance (excluding admin)
                adminDashboard.UserTotalBalance = wallets
                    .Where(x => x.UserId != loggedInUser.UserId)
                    .Sum(x => x.TotalBalance);

                // Total In (Credit)
                adminDashboard.TotalIn = transactionLogs
                    .Where(x => x.AuditType == (byte)AuditType.Credit && x.Status == (byte)Status.Success &&
                    x.WalletUpdated)
                    .Sum(x => (decimal)x.Amount);

                // Total Out (Debit)
                adminDashboard.TotalOut = transactionLogs
                    .Where(x => x.AuditType == (byte)AuditType.Debit && x.Status == (byte)Status.Success &&
                    x.WalletUpdated)
                    .Sum(x => (decimal)x.Amount);

                // Recently used services
                var recentUsed = transactionLogs
                    .Where(x => GetIncludedLogTypes().Contains(x.LogType ?? 0))
                    .OrderByDescending(x => x.CreatedAt)
                    .GroupBy(x => x.LogType)
                    .Select(g => g.First())
                    .Take(4)
                    .ToList();

                adminDashboard.RecentlyUsed = recentUsed.Select(x => new RecentlyUsed
                {
                    Title = Enum.GetName(typeof(LogType), x.LogType) ?? "Unknown",
                    Amount = (decimal)x.Amount
                }).ToList();

                // Tab 2

                // Opening Balance
                var totalDrBefore = transactionLogs
                    .Where(x => x.AuditType == (byte)AuditType.Debit &&
                    x.CreatedAt < request.From &&
                    x.Status == (byte)Status.Success &&
                    x.WalletUpdated)
                    .Sum(x => x.Amount);

                var totalCrBefore = transactionLogs
                    .Where(x => x.AuditType == (byte)AuditType.Credit &&
                    x.CreatedAt < request.From &&
                    x.Status == (byte)Status.Success &&
                    x.WalletUpdated)
                    .Sum(x => x.Amount);

                adminDashboard.OpeningBalance = (decimal)totalCrBefore - (decimal)totalDrBefore;

                // Closing Balance
                var totalDrDay = transactionLogs
                    .Where(x => x.AuditType == (byte)AuditType.Debit &&
                    x.CreatedAt >= request.From &&
                    x.CreatedAt <= request.To &&
                    x.Status == (byte)Status.Success &&
                    x.WalletUpdated)
                    .Sum(x => x.Amount);

                var totalCrDay = transactionLogs
                    .Where(x => x.AuditType == (byte)AuditType.Credit &&
                    x.CreatedAt >= request.From &&
                    x.CreatedAt <= request.To &&
                    x.Status == (byte)Status.Success &&
                    x.WalletUpdated)
                    .Sum(x => x.Amount);

                adminDashboard.Closingbalance = adminDashboard.OpeningBalance + ((decimal)totalCrDay - (decimal)totalDrDay);

                // Fund request
                adminDashboard.FundRequest = transactionLogs
                    .Where(x => x.AuditType == (byte)AuditType.Credit &&
                                (x.LogType == (byte)LogType.FundRequest) &&
                                x.CreatedAt >= request.From &&
                                x.CreatedAt <= request.To &&
                                x.Status == (byte)Status.Pending)
                    .Sum(x => (decimal)x.Amount);

                // Fund Add
                adminDashboard.FundAdd = transactionLogs
                    .Where(x => x.AuditType == (byte)AuditType.Credit &&
                                x.LogType == (byte)LogType.AddFund &&
                                x.CreatedAt >= request.From &&
                                x.CreatedAt <= request.To &&
                                x.Status == (byte)Status.Success &&
                                x.WalletUpdated)
                    .Sum(x => (decimal)x.Amount);

                // Fund Deduct
                adminDashboard.FundDeduct = transactionLogs
                    .Where(x => x.AuditType == (byte)AuditType.Debit &&
                                x.LogType == (byte)LogType.DeductFund &&
                                x.CreatedAt >= request.From &&
                                x.CreatedAt <= request.To &&
                                x.Status == (byte)Status.Success &&
                                x.WalletUpdated)
                    .Sum(x => (decimal)x.Amount);

                // Cash deposit
                adminDashboard.CashDeposit = transactionLogs
                    .Where(x => x.AuditType == (byte)AuditType.Other &&
                                x.LogType == (byte)LogType.AEPSDeposit_IPay &&
                                x.Status == (byte)Status.Success &&
                                x.CreatedAt >= request.From && x.CreatedAt <= request.To)
                    .Sum(x => (decimal)x.Amount);

                // Cash withdrawal
                adminDashboard.TotalAEPS = transactionLogs
                    .Where(x => x.AuditType == (byte)AuditType.Other &&
                                x.LogType == (byte)LogType.AEPSWithdrawal_IPay &&
                                x.Status == (byte)Status.Success &&
                                x.CreatedAt >= request.From &&
                                x.CreatedAt <= request.To)
                    .Sum(x => (decimal)x.Amount);

                // DMT
                adminDashboard.TodaysDMT = transactionLogs
                    .Where(x => x.AuditType == (byte)AuditType.Other &&
                                x.LogType == (byte)LogType.DMTTransaction_IPay &&
                                x.Status == (byte)Status.Success &&
                                x.CreatedAt >= request.From &&
                                x.CreatedAt <= request.To &&
                                x.Status == (byte)Status.Success)
                    .Sum(x => (decimal)x.Amount);

                // Recharge
                adminDashboard.TotalRecharge = transactionLogs
                    .Where(x => x.AuditType == (byte)AuditType.Other &&
                                x.LogType == (byte)LogType.Recharge_Goter &&
                                x.Status == (byte)Status.Success &&
                                x.CreatedAt >= request.From &&
                                x.CreatedAt <= request.To &&
                                x.Status == (byte)Status.Success)
                    .Sum(x => (decimal)x.Amount);

                // Total Payout
                adminDashboard.TotalPayout = transactionLogs
                    .Where(x => x.AuditType == (byte)AuditType.Debit &&
                                x.LogType == (byte)LogType.Payout &&
                                x.Status == (byte)Status.Success &&
                                x.CreatedAt >= request.From &&
                                x.CreatedAt <= request.To &&
                                x.Status == (byte)Status.Success)
                    .Sum(x => (decimal)x.Amount);

                // Today's MATM
                adminDashboard.TodaysMATM = transactionLogs
                    .Where(x => x.AuditType == (byte)AuditType.Credit &&
                                x.LogType == (byte)LogType.MATM_Commission &&
                                x.Status == (byte)Status.Success &&
                                x.CreatedAt >= request.From && x.CreatedAt <= request.To &&
                                x.Status == (byte)Status.Success)
                    .Sum(x => (decimal)x.Amount);

                // Total Aadhar Pay
                adminDashboard.TotalAadharPay = transactionLogs
                    .Where(x => x.AuditType == (byte)AuditType.Debit &&
                                x.LogType == (byte)LogType.AadharPay &&
                                x.Status == (byte)Status.Success &&
                                x.CreatedAt >= request.From && x.CreatedAt <= request.To &&
                                x.Status == (byte)Status.Success)
                    .Sum(x => (decimal)x.Amount);

                // Total UPI
                adminDashboard.TotalUPI = transactionLogs
                    .Where(x => x.LogType == (byte)LogType.UPI &&
                                x.CreatedAt >= request.From && x.CreatedAt <= request.To &&
                                x.Status == (byte)Status.Success)
                    .Sum(x => (decimal)x.Amount);

                // Total PAN
                adminDashboard.TotalPAN = transactionLogs
                    .Where(x => x.LogType == (byte)LogType.PanVerification &&
                                x.CreatedAt >= request.From && x.CreatedAt <= request.To &&
                                x.Status == (byte)Status.Success)
                    .Sum(x => (decimal)x.Amount);

                // UPI Cashout
                adminDashboard.UPICashout = transactionLogs
                    .Where(x => x.LogType == (byte)LogType.UPI &&
                                x.CreatedAt >= request.From && x.CreatedAt <= request.To &&
                                x.Status == (byte)Status.Success)
                    .Sum(x => (decimal)x.Amount);

                // Credit Card
                adminDashboard.CreditCard = transactionLogs
                    .Where(x => x.LogType == (byte)LogType.BillPayment &&
                                x.CreatedAt >= request.From && x.CreatedAt <= request.To &&
                                x.Status == (byte)Status.Success)
                    .Sum(x => (decimal)x.Amount);

                // VAN
                adminDashboard.VAN = transactionLogs
                    .Where(x => x.LogType == (byte)LogType.Other &&
                                x.CreatedAt >= request.From && x.CreatedAt <= request.To &&
                                x.Status == (byte)Status.Success)
                    .Sum(x => (decimal)x.Amount);

                // DMTPPI
                adminDashboard.TotalDMTPPI = transactionLogs
                    .Where(x => x.LogType == (byte)LogType.DMTPPI &&
                                x.CreatedAt >= request.From && x.CreatedAt <= request.To &&
                                x.Status == (byte)Status.Success)
                    .Sum(x => (decimal)x.Amount);

                // Wallet Load
                adminDashboard.TotalWalletLoad = transactionLogs
                    .Where(x => x.AuditType == (byte)AuditType.Credit &&
                                x.LogType == (byte)LogType.AddFund &&
                                x.UserId == loggedInUser.UserId &&
                                x.Status == (byte)Status.Success &&
                                x.CreatedAt >= request.From &&
                                x.CreatedAt <= request.To)
                    .Sum(x => (decimal)x.Amount);

                return adminDashboard;
            }
            catch (Exception ex)
            {
                throw new Exception("Error mapping dashboard data", ex);
            }
        }

        async Task<UserDashboardDto> MapUserDashboard(DashboardQuery request, LoggedInUserDto loggedInUser, IEnumerable<TransactionLog> transactionLogs)
        {
            try
            {
                UserDashboardDto userDashboard = new UserDashboardDto();

                var wallet = await _walletRepository.GetByUserIdAsync(loggedInUser.UserId);

                // Tab 1
                userDashboard.WalletBalance = wallet.TotalBalance;

                userDashboard.TotalIn = transactionLogs
                    .Where(x => x.AuditType == (byte)AuditType.Credit)
                    .Sum(x => (decimal)x.Amount);

                userDashboard.TotalOut = transactionLogs
                    .Where(x => x.AuditType == (byte)AuditType.Debit)
                    .Sum(x => (decimal)x.Amount);

                var recentUsed = transactionLogs
                    .Where(x => GetIncludedLogTypes().Contains(x.LogType ?? 0))
                    .OrderByDescending(x => x.CreatedAt)
                    .GroupBy(x => x.LogType)
                    .Select(g => g.First())
                    .Take(4)
                    .ToList();

                userDashboard.RecentlyUsed = recentUsed.Select(x => new RecentlyUsed
                {
                    Title = Enum.GetName(typeof(LogType), x.LogType) ?? "Unknown",
                    Amount = (decimal)x.Amount
                }).ToList();

                // Tab 2

                // Opening balance
                var totalDrBefore = transactionLogs
                    .Where(x => x.AuditType == (byte)AuditType.Debit && x.CreatedAt < request.From)
                    .Sum(x => x.Amount);

                var totalCrBefore = transactionLogs
                    .Where(x => x.AuditType == (byte)AuditType.Credit && x.CreatedAt < request.From)
                    .Sum(x => x.Amount);

                userDashboard.OpeningBalance = (decimal)totalCrBefore - (decimal)totalDrBefore;

                // Closing balance
                var totalDrDay = transactionLogs
                    .Where(x => x.AuditType == (byte)AuditType.Debit && x.CreatedAt >= request.From && x.CreatedAt <= request.To)
                    .Sum(x => x.Amount);

                var totalCrDay = transactionLogs
                    .Where(x => x.AuditType == (byte)AuditType.Credit && x.CreatedAt >= request.From && x.CreatedAt <= request.To)
                    .Sum(x => x.Amount);

                userDashboard.Closingbalance = userDashboard.OpeningBalance + ((decimal)totalCrDay - (decimal)totalDrDay);

                // Fund request
                userDashboard.FundRequest = transactionLogs
                    .Where(x => x.AuditType == (byte)AuditType.Credit &&
                                (x.LogType == (byte)LogType.AcceptFundRequest) &&
                                x.Status == (byte)Status.Success &&
                                x.CreatedAt >= request.From &&
                                x.CreatedAt <= request.To &&
                                x.Status == (byte)Status.Success &&
                                x.WalletUpdated)
                    .Sum(x => (decimal)x.Amount);

                // Fund Add
                userDashboard.FundAdd = transactionLogs
                    .Where(x => x.AuditType == (byte)AuditType.Credit &&
                                x.LogType == (byte)LogType.AddFund &&
                                x.Status == (byte)Status.Success &&
                                x.CreatedAt >= request.From &&
                                x.CreatedAt <= request.To &&
                                x.Status == (byte)Status.Success &&
                                x.WalletUpdated)
                    .Sum(x => (decimal)x.Amount);

                // Fund Deduct
                userDashboard.FundDeduct = transactionLogs
                    .Where(x => x.AuditType == (byte)AuditType.Debit &&
                                (x.LogType == (byte)LogType.DeductFund) &&
                                x.Status == (byte)Status.Success &&
                                x.CreatedAt >= request.From &&
                                x.CreatedAt <= request.To &&
                                x.Status == (byte)Status.Success &&
                                x.WalletUpdated)
                    .Sum(x => (decimal)x.Amount);

                // Cash deposit
                userDashboard.CashDeposit = transactionLogs
                    .Where(x => x.AuditType == (byte)AuditType.Other &&
                                x.LogType == (byte)LogType.AEPSDeposit_IPay &&
                                x.Status == (byte)Status.Success &&
                                x.CreatedAt >= request.From &&
                                x.CreatedAt <= request.To)
                    .Sum(x => (decimal)x.Amount);

                // Cash withdrawal
                userDashboard.TotalAEPS = transactionLogs
                    .Where(x => x.AuditType == (byte)AuditType.Other &&
                                x.LogType == (byte)LogType.AEPSWithdrawal_IPay &&
                                x.Status == (byte)Status.Success &&
                                x.CreatedAt >= request.From &&
                                x.CreatedAt <= request.To)
                    .Sum(x => (decimal)x.Amount);

                // DMT
                userDashboard.TodaysDMT = transactionLogs
                    .Where(x => x.AuditType == (byte)AuditType.Other &&
                                x.LogType == (byte)LogType.DMTTransaction_IPay &&
                                x.Status == (byte)Status.Success &&
                                x.CreatedAt >= request.From &&
                                x.CreatedAt <= request.To)
                    .Sum(x => (decimal)x.Amount);

                // Recharge
                userDashboard.TotalRecharge = transactionLogs
                    .Where(x => x.AuditType == (byte)AuditType.Other &&
                                x.LogType == (byte)LogType.Recharge_Goter &&
                                x.Status == (byte)Status.Success &&
                                x.CreatedAt >= request.From &&
                                x.CreatedAt <= request.To)
                    .Sum(x => (decimal)x.Amount);

                // Total Payout
                userDashboard.TotalPayout = transactionLogs
                    .Where(x => x.AuditType == (byte)AuditType.Debit &&
                                x.Status == (byte)Status.Success &&
                                x.LogType == (byte)LogType.Payout &&
                                x.CreatedAt >= request.From && x.CreatedAt <= request.To)
                    .Sum(x => (decimal)x.Amount);

                // Today's MATM
                userDashboard.TodaysMATM = transactionLogs
                    .Where(x => x.AuditType == (byte)AuditType.Credit &&
                                x.Status == (byte)Status.Success &&
                                (x.LogType == (byte)LogType.MATM_Commission) &&
                                x.CreatedAt >= request.From && x.CreatedAt <= request.To)
                    .Sum(x => (decimal)x.Amount);

                // Total Aadhar Pay
                userDashboard.TotalAadharPay = transactionLogs
                    .Where(x => x.AuditType == (byte)AuditType.Debit &&
                                x.Status == (byte)Status.Success &&
                                x.LogType == (byte)LogType.AadharPay &&
                                x.CreatedAt >= request.From && x.CreatedAt <= request.To)
                    .Sum(x => (decimal)x.Amount);

                // Total UPI
                userDashboard.TotalUPI = transactionLogs
                    .Where(x => x.LogType == (byte)LogType.UPI &&
                                x.Status == (byte)Status.Success &&
                                x.CreatedAt >= request.From && x.CreatedAt <= request.To)
                    .Sum(x => (decimal)x.Amount);

                // Total PAN
                userDashboard.TotalPAN = transactionLogs
                    .Where(x => x.LogType == (byte)LogType.PanVerification &&
                                x.Status == (byte)Status.Success &&
                                x.CreatedAt >= request.From && x.CreatedAt <= request.To)
                    .Sum(x => (decimal)x.Amount);

                // UPI Cashout
                userDashboard.UPICashout = transactionLogs
                    .Where(x => x.LogType == (byte)LogType.UPI &&
                                x.Status == (byte)Status.Success &&
                                x.CreatedAt >= request.From && x.CreatedAt <= request.To)
                    .Sum(x => (decimal)x.Amount);

                // Credit Card
                userDashboard.CreditCard = transactionLogs
                    .Where(x => x.LogType == (byte)LogType.BillPayment &&
                                x.Status == (byte)Status.Success &&
                                x.CreatedAt >= request.From && x.CreatedAt <= request.To)
                    .Sum(x => (decimal)x.Amount);

                // VAN
                userDashboard.VAN = transactionLogs
                    .Where(x => x.LogType == (byte)LogType.Other &&
                                x.Status == (byte)Status.Success &&
                                x.CreatedAt >= request.From && x.CreatedAt <= request.To)
                    .Sum(x => (decimal)x.Amount);

                // DMTPPI
                userDashboard.TotalDMTPPI = transactionLogs
                    .Where(x => x.LogType == (byte)LogType.DMTPPI &&
                                x.Status == (byte)Status.Success &&
                                x.CreatedAt >= request.From && x.CreatedAt <= request.To)
                    .Sum(x => (decimal)x.Amount);

                return userDashboard;
            }
            catch (Exception ex)
            {
                throw new Exception("Error mapping dashboard data", ex);
            }
        }

        byte[] GetIncludedLogTypes()
        {
            return new[]
            {
                (byte)LogType.AddFund,
                (byte)LogType.DeductFund,
                (byte)LogType.AEPSWithdrawal,
                (byte)LogType.AEPSDeposit,
                (byte)LogType.AEPSSettlement,
                (byte)LogType.Recharge,
                (byte)LogType.BillPayment,
                (byte)LogType.Payout,
                (byte)LogType.PayIn,
                (byte)LogType.AEPSCommission,
                (byte)LogType.AEPS_MiniStatement_Commission,
                (byte)LogType.DMTTransaction,
                (byte)LogType.DMTSurcharge,
                (byte)LogType.Refund,
                (byte)LogType.UPI,
                (byte)LogType.DMTPPI,
            };
        }
    }
}