using AutoMapper;
using FinTech_ApiPanel.Application.Abstraction.ITokenServices;
using FinTech_ApiPanel.Application.Common;
using FinTech_ApiPanel.Application.Queries.FundManagements;
using FinTech_ApiPanel.Domain.DTOs.FundRequests;
using FinTech_ApiPanel.Domain.Interfaces.IBankMasters;
using FinTech_ApiPanel.Domain.Interfaces.IFundRequests;
using FinTech_ApiPanel.Domain.Interfaces.IUserMasters;
using MediatR;

namespace FinTech_ApiPanel.Application.Handlers.FundManagements
{
    public class GetFundRequestByIdHandler : IRequestHandler<GetFundRequestByIdQuery, ApiResponse<FundRequestDto>>
    {
        public readonly IFundRequestRepository _fundRequestRepository;
        public readonly IUserRepository _userRepository;
        public readonly IMapper _mapper;
        public readonly ITokenService _tokenService;
        public readonly IBankMasterRepository _bankMasterRepository;

        public GetFundRequestByIdHandler(IFundRequestRepository fundRequestRepository,
            IUserRepository userRepository,
            ITokenService tokenService,
            IMapper mapper,
            IBankMasterRepository bankMasterRepository)
        {
            _fundRequestRepository = fundRequestRepository;
            _userRepository = userRepository;
            _mapper = mapper;
            _tokenService = tokenService;
            _bankMasterRepository = bankMasterRepository;
        }

        public async Task<ApiResponse<FundRequestDto>> Handle(GetFundRequestByIdQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var fundRequest = await _fundRequestRepository.GetByIdAsync(request.Id);
                if (fundRequest == null)
                    return ApiResponse<FundRequestDto>.NotFoundResponse("Fund request not found.");

                var loggedInUser = _tokenService.GetLoggedInUserinfo();

                if (loggedInUser == null)
                    return ApiResponse<FundRequestDto>.UnauthorizedResponse("Unauthorized access.");
                if (!loggedInUser.IsAdmin && fundRequest.UserId != loggedInUser.UserId)
                    return ApiResponse<FundRequestDto>.ValidationErrorResponse("Not allowed to access this fund request.");

                var fundRequestDto = _mapper.Map<FundRequestDto>(fundRequest);

                var user = await _userRepository.GetByIdAsync(fundRequestDto.UserId);

                fundRequestDto.FullName = user.FullName;

                // bank details
                var bank = await _bankMasterRepository.GetByIdAsync(fundRequest.BankId);
                if (bank != null)
                {
                    fundRequestDto.BankInfo.BankName = bank.BankName;
                    fundRequestDto.BankInfo.AccountNumber = bank.AccountNumber;
                    fundRequestDto.BankInfo.AccountHolderName = bank.AccountHolderName;
                    fundRequestDto.BankInfo.IFSCCode = bank.IFSCCode;
                    fundRequestDto.BankInfo.BranchName = bank.BranchName;
                    fundRequestDto.BankInfo.Id = bank.Id;
                }

                return ApiResponse<FundRequestDto>.SuccessResponse(fundRequestDto, "Fund request retrieved successfully.");
            }
            catch (Exception)
            {
                return ApiResponse<FundRequestDto>.BadRequestResponse("Error retrieving fund request. Please try again later or contact support if the issue persists.");
            }
        }
    }
}
