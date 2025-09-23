using AutoMapper;
using FinTech_ApiPanel.Application.Abstraction.ITokenServices;
using FinTech_ApiPanel.Application.Common;
using FinTech_ApiPanel.Application.Queries.FundManagements;
using FinTech_ApiPanel.Domain.DTOs.FundRequests;
using FinTech_ApiPanel.Domain.Interfaces.IBankMasters;
using FinTech_ApiPanel.Domain.Interfaces.IFundRequests;
using FinTech_ApiPanel.Domain.Interfaces.IUserMasters;
using FinTech_ApiPanel.Domain.Shared.Pagination;
using FinTech_ApiPanel.Domain.Shared.SortHelper;
using MediatR;

namespace FinTech_ApiPanel.Application.Handlers.FundManagements
{
    public class GetAllFundRequestHandler : IRequestHandler<GetAllFundRequestsQuery, ApiResponse<PagedResponse<FundRequestDto>>>
    {
        public readonly IFundRequestRepository _fundRequestRepository;
        public readonly IUserRepository _userRepository;
        public readonly IMapper _mapper;
        public readonly ITokenService _tokenService;
        public readonly IBankMasterRepository _bankMasterRepository;

        public GetAllFundRequestHandler(IFundRequestRepository fundRequestRepository,
            IUserRepository userRepository,
            IMapper mapper,
            ITokenService tokenService,
            IBankMasterRepository bankMasterRepository)
        {
            _fundRequestRepository = fundRequestRepository;
            _userRepository = userRepository;
            _mapper = mapper;
            _tokenService = tokenService;
            _bankMasterRepository = bankMasterRepository;
        }

        public async Task<ApiResponse<PagedResponse<FundRequestDto>>> Handle(GetAllFundRequestsQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var loggedInUser = _tokenService.GetLoggedInUserinfo();

                if (loggedInUser == null)
                    return ApiResponse<PagedResponse<FundRequestDto>>.UnauthorizedResponse("Unauthorized access.");

                if (!loggedInUser.IsAdmin && request.UserId.HasValue && request.UserId != loggedInUser.UserId)
                    return ApiResponse<PagedResponse<FundRequestDto>>.ValidationErrorResponse("Not allowed.");

                var fundRequests = await _fundRequestRepository.GetAllAsync();

                if (!loggedInUser.IsAdmin)
                    fundRequests = fundRequests.Where(x => x.UserId == loggedInUser.UserId);

                var users = await _userRepository.GetAllAsync();
                var banks = await _bankMasterRepository.GetAllAsync();

                List<FundRequestDto> fundRequestDtos = new List<FundRequestDto>();

                foreach (var fundRequest in fundRequests)
                {
                    var fundRequestDto = _mapper.Map<FundRequestDto>(fundRequest);
                    fundRequestDto.Timestamp = fundRequest.UpdatedAt ?? fundRequest.CreatedAt;

                    var user = users.FirstOrDefault(x => x.Id == fundRequestDto.UserId);
                    if (user != null)
                        fundRequestDto.FullName = user.FullName;

                    // bank details
                    var bank = banks.FirstOrDefault(x => x.Id == fundRequest.BankId);
                    if (bank != null)
                    {
                        fundRequestDto.BankInfo.BankName = bank.BankName;
                        fundRequestDto.BankInfo.AccountNumber = bank.AccountNumber;
                        fundRequestDto.BankInfo.AccountHolderName = bank.AccountHolderName;
                        fundRequestDto.BankInfo.IFSCCode = bank.IFSCCode;
                        fundRequestDto.BankInfo.BranchName = bank.BranchName;
                        fundRequestDto.BankInfo.Id = bank.Id;
                    }

                    fundRequestDtos.Add(fundRequestDto);
                }

                var sortedresponse = SortHelper.ApplySort(fundRequestDtos.AsQueryable(), request.OrderBy);

                var pagedResponse = PagedResponse<FundRequestDto>.Create(sortedresponse, request.PageNumber, request.PageSize);

                return ApiResponse<PagedResponse<FundRequestDto>>.SuccessResponse(pagedResponse, "Fund requests retrieved successfully.");
            }
            catch (Exception)
            {
                return ApiResponse<PagedResponse<FundRequestDto>>.BadRequestResponse("Error retrieving fund requests. Please try again later or contact support if the issue persists.");
            }
        }
    }
}
