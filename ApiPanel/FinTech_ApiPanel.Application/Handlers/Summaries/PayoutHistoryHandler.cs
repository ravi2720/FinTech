using AutoMapper;
using FinTech_ApiPanel.Application.Abstraction.ITokenServices;
using FinTech_ApiPanel.Application.Common;
using FinTech_ApiPanel.Application.Queries.Summaries;
using FinTech_ApiPanel.Domain.DTOs.TransactionLogs;
using FinTech_ApiPanel.Domain.Interfaces.ITransactionLogs;
using FinTech_ApiPanel.Domain.Interfaces.IUserMasters;
using FinTech_ApiPanel.Domain.Shared.Enums;
using FinTech_ApiPanel.Domain.Shared.Pagination;
using FinTech_ApiPanel.Domain.Shared.SortHelper;
using FinTech_ApiPanel.Infrastructure.Utilities.TransactionLogs;
using MediatR;

namespace FinTech_ApiPanel.Application.Handlers.Summaries
{
    public class PayoutHistoryHandler : IRequestHandler<PayoutHistoryQuery, ApiResponse<PagedResponse<TransactionLogDto>>>
    {
        private readonly ITransactionLogRepository _transactionLogRepository;
        private readonly IMapper _mapper;
        private readonly ITokenService _tokenService;
        private readonly IUserRepository _userRepository;

        public PayoutHistoryHandler(IMapper mapper,
            ITransactionLogRepository transactionLogRepository,
            ITokenService tokenService,
            IUserRepository userRepository)
        {
            _mapper = mapper;
            _transactionLogRepository = transactionLogRepository;
            _tokenService = tokenService;
            _userRepository = userRepository;
        }

        public async Task<ApiResponse<PagedResponse<TransactionLogDto>>> Handle(PayoutHistoryQuery request, CancellationToken cancellationToken)
        {
            try
            {
                List<TransactionLogDto> TransactionLogDtos = new List<TransactionLogDto>();

                var loggedInUser = _tokenService.GetLoggedInUserinfo();
                if (loggedInUser == null)
                    return ApiResponse<PagedResponse<TransactionLogDto>>.UnauthorizedResponse("User not authenticated");

                var walletTransactions = await _transactionLogRepository.GetByUserIdAsync(loggedInUser.UserId);

                walletTransactions = walletTransactions.Where(x => x.LogType == (byte)LogType.Payout).ToList();

                if (request.From != DateTime.MinValue && request.To != DateTime.MinValue)
                    walletTransactions = walletTransactions.Where(x => x.CreatedAt >= request.From && x.CreatedAt <= request.To).ToList();

                var userList = await _userRepository.GetAllAsync();

                foreach (var payout in walletTransactions)
                {
                    var TransactionLogDto = TransactionLogFactory.GetTransactionLogDto(payout, userList.ToList());

                    TransactionLogDtos.Add(TransactionLogDto);
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
    }
}
