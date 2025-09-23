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
    public class AepsOnboardHistoryHandler : IRequestHandler<AepsOnboardHistoryQuery, ApiResponse<PagedResponse<TransactionLogDto>>>
    {
        private readonly ITransactionLogRepository _transactionLogRepository;
        private readonly IMapper _mapper;
        private readonly ITokenService _tokenService;
        private readonly IUserRepository _userRepository;

        public AepsOnboardHistoryHandler(IMapper mapper,
            ITransactionLogRepository transactionLogRepository,
            ITokenService tokenService,
            IUserRepository userRepository)
        {
            _mapper = mapper;
            _transactionLogRepository = transactionLogRepository;
            _tokenService = tokenService;
            _userRepository = userRepository;
        }

        public async Task<ApiResponse<PagedResponse<TransactionLogDto>>> Handle(AepsOnboardHistoryQuery request, CancellationToken cancellationToken)
        {
            try
            {
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

                if (request.From != DateTime.MinValue && request.To != DateTime.MinValue)
                    walletTransactions = walletTransactions
                        .Where(x => x.CreatedAt >= request.From && x.CreatedAt <= request.To)
                        .ToList();

                var userList = await _userRepository.GetAllAsync();

                var dtoList = walletTransactions.Select(tx =>
                {
                    var dto = TransactionLogFactory.GetTransactionLogDto(tx, userList.ToList());

                    if (!loggedInUser.IsAdmin)
                    {
                        if (tx.LogType == (byte)LogType.AEPSOnboardValidate_IPay)
                        {
                            var userTrans = walletTransactionsList.FirstOrDefault(x => x.ReferenceId == tx.Id.ToString() &&
                            x.AuditType == (byte)AuditType.Debit &&
                            x.LogType == (byte)LogType.AEPSOnboarding_Surcharge);

                            if (userTrans != null)
                                dto.Amount = userTrans.Amount;
                            else
                                dto.Amount = 0;
                        }
                    }

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

        byte[] GetIncludedLogTypes()
        {
            return new[]
            {
                (byte)LogType.AEPSOnboardValidate_IPay
            };
        }
    }
}
