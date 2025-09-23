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
    public class MoneyRemitterHistoryHandler : IRequestHandler<MoneyRemitterHistoryQuery, ApiResponse<PagedResponse<TransactionLogDto>>>
    {
        private readonly ITransactionLogRepository _transactionLogRepository;
        private readonly IMapper _mapper;
        private readonly ITokenService _tokenService;
        private readonly IUserRepository _userRepository;

        public MoneyRemitterHistoryHandler(ITransactionLogRepository transactionLogRepository, IMapper mapper, ITokenService tokenService, IUserRepository userRepository)
        {
            _transactionLogRepository = transactionLogRepository;
            _mapper = mapper;
            _tokenService = tokenService;
            _userRepository = userRepository;
        }

        public async Task<ApiResponse<PagedResponse<TransactionLogDto>>> Handle(MoneyRemitterHistoryQuery request, CancellationToken cancellationToken)
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
                    .Where(x => x.LogType.HasValue && GetIncludedLogTypes().Contains((byte)x.LogType) && x.Status == (byte)Status.Success).ToList();

                if (request.From != DateTime.MinValue && request.To != DateTime.MinValue)
                    walletTransactions = walletTransactions.Where(x => x.CreatedAt >= request.From && x.CreatedAt <= request.To).ToList();

                var userList = await _userRepository.GetAllAsync();

                var dtoList = walletTransactions.Select(r =>
                {
                    var dto = TransactionLogFactory.GetTransactionLogDto(r, userList.ToList());
                    if (!loggedInUser.IsAdmin)
                    {
                        if (r.LogType == (byte)LogType.RemitterKyc_IPay)
                        {
                            var userTrans = walletTransactionsList.FirstOrDefault(x => x.ReferenceId == r.Id.ToString() &&
                            x.AuditType == (byte)AuditType.Debit &&
                            x.LogType == (byte)LogType.Remitter_KYC_Surcharge);

                            if (userTrans != null)
                                dto.Amount = userTrans.Amount;
                            else
                                dto.Amount = 0;
                        }
                    }

                    return dto;

                }).ToList();
               
                var sorted = SortHelper.ApplySort<TransactionLogDto>(dtoList.AsQueryable(), request.OrderBy);
                var paged = PagedResponse<TransactionLogDto>.Create(sorted, request.PageNumber, request.PageSize);

                return ApiResponse<PagedResponse<TransactionLogDto>>.SuccessResponse(paged);
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
                (byte)LogType.RemitterKyc_IPay,
            };
        }
    }
}
