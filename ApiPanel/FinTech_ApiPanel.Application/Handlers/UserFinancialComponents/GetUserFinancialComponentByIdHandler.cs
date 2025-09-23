using AutoMapper;
using FinTech_ApiPanel.Application.Abstraction.ITokenServices;
using FinTech_ApiPanel.Application.Common;
using FinTech_ApiPanel.Application.Queries.UserFinancialComponents;
using FinTech_ApiPanel.Domain.DTOs.FinancialComponents;
using FinTech_ApiPanel.Domain.Interfaces.IUserFinancialComponents;
using MediatR;

namespace FinTech_ApiPanel.Application.Handlers.UserFinancialComponents
{
    public class GetUserFinancialComponentByIdHandler : IRequestHandler<GetUserFinancialComponentByIdQuery, ApiResponse<FinancialComponentDto>>
    {
        private readonly IUserFinancialComponentRepository _userFinancialComponentRepository;
        private readonly IMapper _mapper;
        private readonly ITokenService _tokenService;

        public GetUserFinancialComponentByIdHandler(IUserFinancialComponentRepository userFinancialComponentRepository,
            IMapper mapper,
            ITokenService tokenService)
        {
            _mapper = mapper;
            _tokenService = tokenService;
            _userFinancialComponentRepository = userFinancialComponentRepository;
        }

        public async Task<ApiResponse<FinancialComponentDto>> Handle(GetUserFinancialComponentByIdQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var loggedInUser = _tokenService.GetLoggedInUserinfo();
                if (loggedInUser == null)
                    return ApiResponse<FinancialComponentDto>.UnauthorizedResponse("User not authorized.");

                if (!loggedInUser.IsAdmin)
                    return ApiResponse<FinancialComponentDto>.UnauthorizedResponse("Access denied.");

                var exist = await _userFinancialComponentRepository.GetByIdAsync(request.Id);

                if (exist == null)
                    return ApiResponse<FinancialComponentDto>.NotFoundResponse("Not found");

                var financialComponent = _mapper.Map<FinancialComponentDto>(exist);

                return ApiResponse<FinancialComponentDto>.SuccessResponse(financialComponent);
            }
            catch (Exception ex)
            {
                return ApiResponse<FinancialComponentDto>.BadRequestResponse($"An error occurred while updating: {ex.Message}");
            }
        }
    }
}
