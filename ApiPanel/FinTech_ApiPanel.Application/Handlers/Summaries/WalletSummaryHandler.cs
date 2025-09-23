using AutoMapper;
using FinTech_ApiPanel.Application.Abstraction.ITokenServices;
using FinTech_ApiPanel.Application.Common;
using FinTech_ApiPanel.Application.Queries.Summaries;
using FinTech_ApiPanel.Domain.DTOs.TransactionLogs;
using FinTech_ApiPanel.Domain.Interfaces.ITransactionLogs;
using FinTech_ApiPanel.Domain.Interfaces.IUserMasters;
using FinTech_ApiPanel.Domain.Interfaces.IUserWallets;
using FinTech_ApiPanel.Domain.Shared.Enums;
using FinTech_ApiPanel.Domain.Shared.Pagination;
using FinTech_ApiPanel.Domain.Shared.SortHelper;
using FinTech_ApiPanel.Infrastructure.Utilities.TransactionLogs;
using MediatR;
using System.Linq.Dynamic.Core;

namespace FinTech_ApiPanel.Application.Handlers.Summaries
{
    public class WalletSummaryHandler : IRequestHandler<WalletSummaryQuery, ApiResponse<PagedResponse<TransactionLogDto>>>
    {
        private readonly ITransactionLogRepository _transactionLogRepository;
        private readonly IMapper _mapper;
        private readonly ITokenService _tokenService;
        private readonly IUserRepository _userRepository;
        private readonly IWalletRepository _walletRepository;

        public WalletSummaryHandler(IMapper mapper,
            ITransactionLogRepository transactionLogRepository,
            ITokenService tokenService,
            IUserRepository userRepository,
            IWalletRepository walletRepository)
        {
            _mapper = mapper;
            _transactionLogRepository = transactionLogRepository;
            _tokenService = tokenService;
            _userRepository = userRepository;
            _walletRepository = walletRepository;
        }

        public async Task<ApiResponse<PagedResponse<TransactionLogDto>>> Handle(WalletSummaryQuery request, CancellationToken cancellationToken)
        {
            try
            {
                List<TransactionLogDto> payoutHistoryDtos = new List<TransactionLogDto>();

                var loggedInUser = _tokenService.GetLoggedInUserinfo();
                if (loggedInUser == null)
                    return ApiResponse<PagedResponse<TransactionLogDto>>.UnauthorizedResponse("User not authenticated");

                var walletTransactions = await _transactionLogRepository.GetByUserIdAsync(loggedInUser.UserId);

                walletTransactions = walletTransactions
                    .Where(x => x.LogType.HasValue && GetIncludedLogTypes().Contains((byte)x.LogType));

                if (request.AuditType.HasValue)
                    walletTransactions = walletTransactions.Where(x => x.AuditType == request.AuditType.Value).ToList();

                if (request.From != DateTime.MinValue && request.To != DateTime.MinValue)
                    walletTransactions = walletTransactions.Where(x => x.CreatedAt >= request.From && x.CreatedAt <= request.To).ToList();

                var userList = await _userRepository.GetAllAsync();

                foreach (var walletTransaction in walletTransactions)
                {
                    var payoutHistoryDto = TransactionLogFactory.GetTransactionLogDto(walletTransaction, userList.ToList());

                    payoutHistoryDtos.Add(payoutHistoryDto);
                }

                var sortedResult = SortHelper.ApplySort<TransactionLogDto>(payoutHistoryDtos.AsQueryable(), request.OrderBy);

                var paginatedResult = PagedResponse<TransactionLogDto>.Create(sortedResult, request.PageNumber, request.PageSize);

                return ApiResponse<PagedResponse<TransactionLogDto>>.SuccessResponse(paginatedResult);
            }
            catch (Exception ex)
            {
                return ApiResponse<PagedResponse<TransactionLogDto>>.BadRequestResponse(ex.Message);
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
                (byte)LogType.BBPS_Commission,
                (byte)LogType.MATM_Commission,
                (byte)LogType.PAN_Commission,
                (byte)LogType.Recharge_Commission,
                (byte)LogType.Aadhar_PaySurcharge,
                (byte)LogType.UPI_Surcharge,
                (byte)LogType.DMTSurcharge,
                (byte)LogType.DMTTransaction,
                (byte)LogType.CreditCard_Surcharge,
                (byte)LogType.Remitter_KYC_Surcharge,
                (byte)LogType.Verify_BankAccount_Surcharge,
                (byte)LogType.AEPSOnboarding_Surcharge,
                (byte)LogType.Outlet_Login_Surcharge,
                (byte)LogType.PayOut_Surcharge,
                (byte)LogType.Refund,
            };
        }
    }
}
