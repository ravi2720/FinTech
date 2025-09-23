using AutoMapper;
using FinTech_ApiPanel.Application.Abstraction.ITokenServices;
using FinTech_ApiPanel.Application.Commands.UserFinancialComponents;
using FinTech_ApiPanel.Application.Common;
using FinTech_ApiPanel.Domain.Entities.UserFinancialComponents;
using FinTech_ApiPanel.Domain.Interfaces.IUserFinancialComponents;
using FinTech_ApiPanel.Domain.Shared.Enums;
using MediatR;

namespace FinTech_ApiPanel.Application.Handlers.UserFinancialComponents
{
    public class UpdateUserFinancialComponentHandler : IRequestHandler<UpdateUserFinancialComponentCommand, ApiResponse>
    {
        private readonly IUserFinancialComponentRepository _userFinancialComponentRepository;
        private readonly IMapper _mapper;
        private readonly ITokenService _tokenService;

        public UpdateUserFinancialComponentHandler(IUserFinancialComponentRepository userFinancialComponentRepository,
            IMapper mapper,
            ITokenService tokenService)
        {
            _mapper = mapper;
            _tokenService = tokenService;
            _userFinancialComponentRepository = userFinancialComponentRepository;
        }

        public async Task<ApiResponse> Handle(UpdateUserFinancialComponentCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var loggedInUser = _tokenService.GetLoggedInUserinfo();
                if (loggedInUser == null)
                    return ApiResponse.UnauthorizedResponse("User not authorized.");

                if (!loggedInUser.IsAdmin)
                    return ApiResponse.UnauthorizedResponse("Access denied.");

                var financialComponentList = await _userFinancialComponentRepository.GetAllAsync();

                var exist = financialComponentList.FirstOrDefault(x => x.Id == request.Id);

                if (exist == null)
                    return ApiResponse.NotFoundResponse("Not found");

                if (exist.ServiceSubType != (byte)FinancialComponenService.Other_Commission && exist.ServiceSubType != (byte)FinancialComponenService.Other_Surcharge)
                {
                    bool overlapExists;

                    if (exist.ServiceType == (byte)FinancialComponenService.Recharge)
                    {
                        // Recharge => check overlaps within same OperatorId + ServiceSubType
                        overlapExists = financialComponentList.Any(x =>
                            x.Id != request.Id &&
                            x.OperatorId == exist.OperatorId &&
                            x.ServiceSubType == exist.ServiceSubType &&
                            x.Start_Value <= request.End_Value &&
                            x.End_Value >= request.Start_Value);
                    }
                    else
                    {
                        // Other services => check overlaps globally
                        overlapExists = financialComponentList.Any(x =>
                            x.Id != request.Id &&
                            x.Start_Value <= request.End_Value &&
                            x.End_Value >= request.Start_Value);
                    }

                    if (overlapExists)
                        throw new InvalidOperationException("Range overlaps with an existing record.");
                }
                _mapper.Map(request, exist);

                await _userFinancialComponentRepository.UpdateAsync(exist);

                return ApiResponse.SuccessResponse();
            }
            catch (Exception ex)
            {
                return ApiResponse.BadRequestResponse($"An error occurred while updating: {ex.Message}");
            }
        }
    }
}
