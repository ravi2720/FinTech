using AutoMapper;
using FinTech_ApiPanel.Application.Abstraction.ITokenServices;
using FinTech_ApiPanel.Application.Commands.FinancialComponentMasters;
using FinTech_ApiPanel.Application.Common;
using FinTech_ApiPanel.Domain.Entities.FinancialComponentMasters;
using FinTech_ApiPanel.Domain.Interfaces.IFinancialComponentMasters;
using FinTech_ApiPanel.Domain.Shared.Enums;
using MediatR;

namespace FinTech_ApiPanel.Application.Handlers.FinancialComponentMasters
{
    public class CreateFinancialComponentHandler : IRequestHandler<CreateFinancialComponentCommand, ApiResponse>
    {
        private readonly IFinancialComponentMasterRepository _financialComponentMasterRepository;
        private readonly IMapper _mapper;
        private readonly ITokenService _tokenService;

        public CreateFinancialComponentHandler(IFinancialComponentMasterRepository financialComponentMasterRepository,
            IMapper mapper,
            ITokenService tokenService)
        {
            _financialComponentMasterRepository = financialComponentMasterRepository;
            _mapper = mapper;
            _tokenService = tokenService;
        }

        public async Task<ApiResponse> Handle(CreateFinancialComponentCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var loggedInUser = _tokenService.GetLoggedInUserinfo();
                if (loggedInUser == null)
                    return ApiResponse.UnauthorizedResponse("User not authorized.");

                if (!loggedInUser.IsAdmin)
                    return ApiResponse.UnauthorizedResponse("Access denied.");

                var financialComponentList = await _financialComponentMasterRepository.GetAllAsync();

                if (request.ServiceType != (byte)FinancialComponenService.Recharge && request.ServiceSubType.HasValue)
                {
                    var ifExist = financialComponentList.FirstOrDefault(x => x.Type == request.Type && x.ServiceType == request.ServiceType && x.ServiceSubType == request.ServiceSubType);
                    if (ifExist == null)
                    {
                        FinancialComponentMaster componentMaster = new FinancialComponentMaster();

                        componentMaster.Type = request.Type;
                        componentMaster.ServiceType = request.ServiceType;
                        componentMaster.ServiceSubType = request.ServiceSubType;
                        componentMaster.Value = request.Value;

                        await _financialComponentMasterRepository.AddAsync(componentMaster);
                    }
                    else
                    {
                        ifExist.Value = request.Value;
                        await _financialComponentMasterRepository.UpdateAsync(ifExist);
                    }

                    return ApiResponse.SuccessResponse();
                }

                bool exist = false;

                if (request.ServiceType == (byte)FinancialComponenService.Recharge)
                    exist = financialComponentList.Any(x => x.OperatorId == request.OperatorId && x.ServiceType == request.ServiceType && x.ServiceSubType == request.ServiceSubType && x.Start_Value <= request.End_Value && x.End_Value >= request.Start_Value);
                else
                    exist = financialComponentList.Any(x => x.ServiceType == request.ServiceType && x.ServiceSubType == request.ServiceSubType && x.Start_Value <= request.End_Value && x.End_Value >= request.Start_Value);

                if (exist)
                    throw new InvalidOperationException("Range overlaps with an existing record.");

                var financialComponent = _mapper.Map<FinancialComponentMaster>(request);
                await _financialComponentMasterRepository.AddAsync(financialComponent);

                return ApiResponse.SuccessResponse();
            }
            catch (Exception ex)
            {
                return ApiResponse.BadRequestResponse($"An error occurred while creating: {ex.Message}");
            }
        }
    }
}