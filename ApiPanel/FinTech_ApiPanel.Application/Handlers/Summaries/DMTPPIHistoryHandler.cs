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
    public class DMTPPIHistoryHandler : IRequestHandler<DMTPPIHistoryQuery, ApiResponse<PagedResponse<TransactionLogDto>>>
    {
        private readonly ITransactionLogRepository _transactionLogRepository;
        private readonly IMapper _mapper;
        private readonly ITokenService _tokenService;
        private readonly IUserRepository _userRepository;

        public DMTPPIHistoryHandler(ITransactionLogRepository transactionLogRepository, IMapper mapper, ITokenService tokenService, IUserRepository userRepository)
        {
            _transactionLogRepository = transactionLogRepository;
            _mapper = mapper;
            _tokenService = tokenService;
            _userRepository = userRepository;
        }

        public async Task<ApiResponse<PagedResponse<TransactionLogDto>>> Handle(DMTPPIHistoryQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var user = _tokenService.GetLoggedInUserinfo();
                if (user == null)
                    return ApiResponse<PagedResponse<TransactionLogDto>>.UnauthorizedResponse("User not authenticated");

                var logs = await _transactionLogRepository.GetByUserIdAsync(user.UserId);
                logs = logs.Where(x => x.LogType == (byte)LogType.DMTPPI).ToList();

                if (request.From != DateTime.MinValue && request.To != DateTime.MinValue)
                    logs = logs.Where(x => x.CreatedAt >= request.From && x.CreatedAt <= request.To).ToList();

                var userList = await _userRepository.GetAllAsync();

                var dtoList = logs.Select(x => TransactionLogFactory.GetTransactionLogDto(x, userList.ToList())).ToList();
                var sorted = SortHelper.ApplySort<TransactionLogDto>(dtoList.AsQueryable(), request.OrderBy);
                var paged = PagedResponse<TransactionLogDto>.Create(sorted, request.PageNumber, request.PageSize);

                return ApiResponse<PagedResponse<TransactionLogDto>>.SuccessResponse(paged);
            }
            catch (Exception ex)
            {
                return ApiResponse<PagedResponse<TransactionLogDto>>.BadRequestResponse(ex.Message);
            }
        }
    }

}
