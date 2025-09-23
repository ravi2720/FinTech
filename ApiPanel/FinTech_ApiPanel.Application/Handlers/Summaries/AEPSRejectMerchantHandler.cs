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
    internal class AEPSRejectMerchantHandler : IRequestHandler<AEPSRejectMerchantQuery, ApiResponse<PagedResponse<TransactionLogDto>>>
    {
        private readonly ITransactionLogRepository _transactionLogRepository;
        private readonly ITokenService _tokenService;
        private readonly IMapper _mapper;
        private readonly IUserRepository _userRepository;

        public AEPSRejectMerchantHandler(ITransactionLogRepository transactionLogRepository,
            ITokenService tokenService,
            IMapper mapper,
            IUserRepository userRepository)
        {
            _transactionLogRepository = transactionLogRepository;
            _tokenService = tokenService;
            _mapper = mapper;
            _userRepository = userRepository;
        }

        public async Task<ApiResponse<PagedResponse<TransactionLogDto>>> Handle(AEPSRejectMerchantQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var loggedInUser = _tokenService.GetLoggedInUserinfo();
                if (loggedInUser == null)
                    return ApiResponse<PagedResponse<TransactionLogDto>>.UnauthorizedResponse("User not authenticated");

                var walletTransactions = new List<TransactionLog>();
                if (loggedInUser.IsAdmin)
                    walletTransactions = _transactionLogRepository.GetAllAsync().Result.ToList();
                else
                    walletTransactions = _transactionLogRepository.GetByUserIdAsync(loggedInUser.UserId).Result.ToList();

                walletTransactions = walletTransactions.Where(x => x.LogType.HasValue && GetIncludedLogTypes().Contains((byte)x.LogType) && x.Status == (byte)Status.Failed).ToList();

                if (request.From != DateTime.MinValue && request.To != DateTime.MinValue)
                    walletTransactions = walletTransactions.Where(x => x.CreatedAt >= request.From && x.CreatedAt <= request.To).ToList();

                var userList = await _userRepository.GetAllAsync();

                var mapped = walletTransactions.Select(log =>
                {
                    var dto = TransactionLogFactory.GetTransactionLogDto(log, userList.ToList());
                    return dto;
                });

                var sorted = SortHelper.ApplySort(mapped.AsQueryable(), request.OrderBy);
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
                (byte)LogType.AEPSOnboardValidate_IPay,
            };
        }
    }
}
