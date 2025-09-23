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
    public class PanOnboardHistoryHandler : IRequestHandler<PanOnboardHistoryQuery, ApiResponse<PagedResponse<TransactionLogDto>>>
    {
        private readonly ITransactionLogRepository _transactionLogRepository;
        private readonly IMapper _mapper;
        private readonly ITokenService _tokenService;
        private readonly IUserRepository _userRepository;

        public PanOnboardHistoryHandler(IMapper mapper,
            ITransactionLogRepository transactionLogRepository,
            ITokenService tokenService,
            IUserRepository userRepository)
        {
            _mapper = mapper;
            _transactionLogRepository = transactionLogRepository;
            _tokenService = tokenService;
            _userRepository = userRepository;
        }

        public async Task<ApiResponse<PagedResponse<TransactionLogDto>>> Handle(PanOnboardHistoryQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var loggedInUser = _tokenService.GetLoggedInUserinfo();
                if (loggedInUser == null)
                    return ApiResponse<PagedResponse<TransactionLogDto>>.UnauthorizedResponse("User not authenticated");

                var walletTransactions = await _transactionLogRepository.GetByUserIdAsync(loggedInUser.UserId);

                walletTransactions = walletTransactions
                    .Where(x => x.LogType == (byte)LogType.PANOnboard)
                    .ToList();

                if (request.From != DateTime.MinValue && request.To != DateTime.MinValue)
                    walletTransactions = walletTransactions
                        .Where(x => x.CreatedAt >= request.From && x.CreatedAt <= request.To)
                        .ToList();

                var userList = await _userRepository.GetAllAsync();

                var dtoList = walletTransactions.Select(tx =>
                {
                    var dto = TransactionLogFactory.GetTransactionLogDto(tx, userList.ToList());
                    return dto;
                }).ToList();

                var sorted = SortHelper.ApplySort<TransactionLogDto>(dtoList.AsQueryable(), request.OrderBy);
                var paginated = PagedResponse<TransactionLogDto>.Create(sorted, request.PageNumber, request.PageSize);

                return ApiResponse<PagedResponse<TransactionLogDto>>.SuccessResponse(paginated);
            }
            catch (Exception ex)
            {
                return ApiResponse<PagedResponse<TransactionLogDto>>.BadRequestResponse(ex.Message);
            }
        }
    }

}
