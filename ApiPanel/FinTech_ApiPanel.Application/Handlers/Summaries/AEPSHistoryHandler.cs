using AutoMapper;
using FinTech_ApiPanel.Application.Abstraction.ITokenServices;
using FinTech_ApiPanel.Application.Common;
using FinTech_ApiPanel.Application.Queries.Summaries;
using FinTech_ApiPanel.Domain.DTOs.TransactionLogs;
using FinTech_ApiPanel.Domain.Entities.TransactionLogs;
using FinTech_ApiPanel.Domain.Interfaces.ITransactionLogs;
using FinTech_ApiPanel.Domain.Interfaces.IUserMasters;
using FinTech_ApiPanel.Domain.Shared.Enums;
using FinTech_ApiPanel.Domain.Shared.Pagination;
using FinTech_ApiPanel.Domain.Shared.SortHelper;
using FinTech_ApiPanel.Infrastructure.Utilities.TransactionLogs;
using MediatR;

namespace FinTech_ApiPanel.Application.Handlers.Summaries
{
    public class AEPSHistoryHandler : IRequestHandler<AEPSHistoryQuery, ApiResponse<PagedResponse<TransactionLogDto>>>
    {
        private readonly ITransactionLogRepository _transactionLogRepository;
        private readonly IMapper _mapper;
        private readonly ITokenService _tokenService;
        private readonly IUserRepository _userRepository;

        public AEPSHistoryHandler(IMapper mapper,
            ITransactionLogRepository transactionLogRepository,
            ITokenService tokenService,
            IUserRepository userRepository)
        {
            _mapper = mapper;
            _transactionLogRepository = transactionLogRepository;
            _tokenService = tokenService;
            _userRepository = userRepository;
        }

        public async Task<ApiResponse<PagedResponse<TransactionLogDto>>> Handle(AEPSHistoryQuery request, CancellationToken cancellationToken)
        {
            try
            {
                List<TransactionLogDto> TransactionLogDtos = new List<TransactionLogDto>();

                var loggedInUser = _tokenService.GetLoggedInUserinfo();
                if (loggedInUser == null)
                    return ApiResponse<PagedResponse<TransactionLogDto>>.UnauthorizedResponse("User not authenticated");

                var walletTransactionsList = new List<TransactionLog>();
                if (loggedInUser.IsAdmin)
                    walletTransactionsList = _transactionLogRepository.GetAllAsync().Result.ToList();
                else
                    walletTransactionsList = _transactionLogRepository.GetByUserIdAsync(loggedInUser.UserId).Result.ToList();

                var walletTransactions = walletTransactionsList
                    .Where(x => x.LogType.HasValue && GetIncludedLogTypes().Contains((byte)x.LogType)).ToList();

                if (request.AuditType.HasValue)
                    walletTransactions = walletTransactions.Where(x => x.AuditType == request.AuditType.Value).ToList();

                if (request.From != DateTime.MinValue && request.To != DateTime.MinValue)
                    walletTransactions = walletTransactions.Where(x => x.CreatedAt >= request.From && x.CreatedAt <= request.To).ToList();

                var userList = await _userRepository.GetAllAsync();

                foreach (var aepsHistory in walletTransactions)
                {
                    var transactionLogDto = TransactionLogFactory.GetTransactionLogDto(aepsHistory, userList.ToList());
                    if (!loggedInUser.IsAdmin)
                    {
                        if (aepsHistory.LogType == (byte)LogType.AEPSMiniStatement_IPay)
                        {
                            var userTrans = walletTransactionsList.FirstOrDefault(x => x.ReferenceId == aepsHistory.Id.ToString() &&
                            x.AuditType == (byte)AuditType.Credit &&
                            x.LogType == (byte)LogType.AEPS_MiniStatement_Commission);

                            if (userTrans != null)
                                transactionLogDto.Amount = userTrans.Amount;
                            else
                                transactionLogDto.Amount = 0;
                        }
                    }

                    transactionLogDto.AEPSType = aepsHistory.LogType switch
                    {
                        (byte)LogType.AEPSDeposit_IPay => "CD",
                        (byte)LogType.AEPSWithdrawal_IPay => "CW",
                        (byte)LogType.AEPSBalanceEnquiry_IPay => "BE",
                        (byte)LogType.AEPSMiniStatement_IPay => "MS",
                        _ => null
                    };
                    TransactionLogDtos.Add(transactionLogDto);
                }

                var sortedResult = SortHelper.ApplySort<TransactionLogDto>(TransactionLogDtos.AsQueryable(), request.OrderBy);

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
                (byte)LogType.AEPSDeposit_IPay,
                (byte)LogType.AEPSWithdrawal_IPay,
                (byte)LogType.AEPSBalanceEnquiry_IPay,
                (byte)LogType.AEPSMiniStatement_IPay,
            };
        }
    }
}
