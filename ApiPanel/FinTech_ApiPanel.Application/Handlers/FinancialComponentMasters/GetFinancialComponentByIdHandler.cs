using AutoMapper;
using FinTech_ApiPanel.Application.Abstraction.ITokenServices;
using FinTech_ApiPanel.Application.Common;
using FinTech_ApiPanel.Application.Queries.FinancialComponentMasters;
using FinTech_ApiPanel.Domain.DTOs.FinancialComponents;
using FinTech_ApiPanel.Domain.Interfaces.IFinancialComponentMasters;
using FinTech_ApiPanel.Domain.Interfaces.IOperatorMasters;
using MediatR;

namespace FinTech_ApiPanel.Application.Handlers.FinancialComponentMasters
{
    public class GetFinancialComponentByIdHandler : IRequestHandler<GetFinancialComponentByIdQuery, ApiResponse<FinancialComponentDto>>
    {
        private readonly IFinancialComponentMasterRepository _financialComponentMasterRepository;
        private readonly IMapper _mapper;
        private readonly ITokenService _tokenService;
        private readonly IOperatorRepository _operatorRepository;

        public GetFinancialComponentByIdHandler(IFinancialComponentMasterRepository financialComponentMasterRepository,
            IMapper mapper,
            ITokenService tokenService,
            IOperatorRepository operatorRepository)
        {
            _financialComponentMasterRepository = financialComponentMasterRepository;
            _mapper = mapper;
            _tokenService = tokenService;
            _operatorRepository = operatorRepository;
        }

        public async Task<ApiResponse<FinancialComponentDto>> Handle(GetFinancialComponentByIdQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var loggedInUser = _tokenService.GetLoggedInUserinfo();
                if (loggedInUser == null)
                    return ApiResponse<FinancialComponentDto>.UnauthorizedResponse("User not authorized.");

                if (!loggedInUser.IsAdmin)
                    return ApiResponse<FinancialComponentDto>.UnauthorizedResponse("Access denied.");

                var exist = await _financialComponentMasterRepository.GetByIdAsync(request.Id);

                if (exist == null)
                    return ApiResponse<FinancialComponentDto>.NotFoundResponse("Not found");

                var financialComponent = _mapper.Map<FinancialComponentDto>(exist);

                if (financialComponent.OperatorId.HasValue)
                {
                    var operatorInfo = await _operatorRepository.GetByIdAsync((long)financialComponent.OperatorId);

                    financialComponent.OperatorName = operatorInfo.Name;
                }

                return ApiResponse<FinancialComponentDto>.SuccessResponse(financialComponent);
            }
            catch (Exception ex)
            {
                return ApiResponse<FinancialComponentDto>.BadRequestResponse($"An error occurred while updating: {ex.Message}");
            }
        }
    }
}
