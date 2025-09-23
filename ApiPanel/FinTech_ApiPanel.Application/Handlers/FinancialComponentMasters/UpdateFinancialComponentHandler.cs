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
    public class UpdateFinancialComponentHandler : IRequestHandler<UpdateFinancialComponentCommand, ApiResponse>
    {
        private readonly IFinancialComponentMasterRepository _financialComponentMasterRepository;
        private readonly IMapper _mapper;
        private readonly ITokenService _tokenService;

        public UpdateFinancialComponentHandler(IFinancialComponentMasterRepository financialComponentMasterRepository,
            IMapper mapper,
            ITokenService tokenService)
        {
            _financialComponentMasterRepository = financialComponentMasterRepository;
            _mapper = mapper;
            _tokenService = tokenService;
        }

        public async Task<ApiResponse> Handle(UpdateFinancialComponentCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var loggedInUser = _tokenService.GetLoggedInUserinfo();
                if (loggedInUser == null)
                    return ApiResponse.UnauthorizedResponse("User not authorized.");

                if (!loggedInUser.IsAdmin)
                    return ApiResponse.UnauthorizedResponse("Access denied.");

                var financialComponentList = await _financialComponentMasterRepository.GetAllAsync();

                var exist = financialComponentList.FirstOrDefault(x => x.Id == request.Id);

                if (exist == null)
                    return ApiResponse.NotFoundResponse("Not found");

                if (exist.ServiceSubType != (byte)FinancialComponenService.Other_Commission && exist.ServiceSubType != (byte)FinancialComponenService.Other_Surcharge)
                {
                    bool existOverlape = false;

                    if (exist.ServiceType == (byte)FinancialComponenService.Recharge)
                        existOverlape = financialComponentList
                            .Any(x => x.Id != request.Id && x.OperatorId == exist.OperatorId && x.ServiceType == exist.ServiceType && x.ServiceSubType == exist.ServiceSubType && x.Start_Value <= request.End_Value && x.End_Value >= request.Start_Value);
                    else
                        existOverlape = financialComponentList
                        .Any(x => x.Id != request.Id && x.ServiceType == exist.ServiceType && x.Type == exist.Type && x.Start_Value <= request.End_Value && x.End_Value >= request.Start_Value);

                    if (existOverlape)
                        throw new InvalidOperationException("Range overlaps with an existing record.");
                }

                _mapper.Map(request, exist);

                await _financialComponentMasterRepository.UpdateAsync(exist);

                return ApiResponse.SuccessResponse();
            }
            catch (Exception ex)
            {
                return ApiResponse.BadRequestResponse($"An error occurred while updating: {ex.Message}");
            }
        }
    }
}
