using AutoMapper;
using FinTech_ApiPanel.Application.Abstraction.ITokenServices;
using FinTech_ApiPanel.Application.Common;
using FinTech_ApiPanel.Application.Queries.UserFinancialComponents;
using FinTech_ApiPanel.Domain.DTOs.FinancialComponents;
using FinTech_ApiPanel.Domain.Entities.UserFinancialComponents;
using FinTech_ApiPanel.Domain.Interfaces.IFinancialComponentMasters;
using FinTech_ApiPanel.Domain.Interfaces.IOperatorMasters;
using FinTech_ApiPanel.Domain.Interfaces.IUserFinancialComponents;
using MediatR;

namespace FinTech_ApiPanel.Application.Handlers.UserFinancialComponents
{
    public class GetAllUserFinancialComponentHandler : IRequestHandler<GetAllUserFinancialComponentQuery, ApiResponse<List<FinancialComponentDto>>>
    {
        private readonly IUserFinancialComponentRepository _userFinancialComponentRepository;
        private readonly IFinancialComponentMasterRepository _financialComponentMasterRepository;
        private readonly IMapper _mapper;
        private readonly ITokenService _tokenService;
        private readonly IOperatorRepository _operatorRepository;

        public GetAllUserFinancialComponentHandler(IUserFinancialComponentRepository userFinancialComponentRepository,
            IFinancialComponentMasterRepository financialComponentMasterRepository,
            IMapper mapper,
            ITokenService tokenService,
            IOperatorRepository operatorRepository)
        {
            _mapper = mapper;
            _tokenService = tokenService;
            _userFinancialComponentRepository = userFinancialComponentRepository;
            _financialComponentMasterRepository = financialComponentMasterRepository;
            _operatorRepository = operatorRepository;
        }

        public async Task<ApiResponse<List<FinancialComponentDto>>> Handle(GetAllUserFinancialComponentQuery request, CancellationToken cancellationToken)
        {
            try
            {
                if (request.UserId == null || request.UserId == 0)
                    return ApiResponse<List<FinancialComponentDto>>.BadRequestResponse("Please provide userId.");

                var loggedInUser = _tokenService.GetLoggedInUserinfo();
                if (loggedInUser == null)
                    return ApiResponse<List<FinancialComponentDto>>.UnauthorizedResponse("User not authorized.");

                if (!loggedInUser.IsAdmin)
                    return ApiResponse<List<FinancialComponentDto>>.UnauthorizedResponse("Access denied.");

                var userFinancialComponentList = await _userFinancialComponentRepository.GetAllAsync();

                userFinancialComponentList = userFinancialComponentList
                    .Where(x => x.Type == request.Type && x.ServiceType == request.ServiceType && x.UserId == request.UserId)
                    .ToList();

                var operatorList = _operatorRepository.GetAll();

                List<FinancialComponentDto> financialComponentDtos = new List<FinancialComponentDto>();

                if (userFinancialComponentList.Count() > 0)
                {
                    foreach (var item in userFinancialComponentList)
                    {
                        var financialComponent = _mapper.Map<FinancialComponentDto>(item);

                        if (financialComponent.OperatorId.HasValue)
                        {
                            var operatorInfo = operatorList.Where(x => x.Id == (long)financialComponent.OperatorId).FirstOrDefault();

                            financialComponent.OperatorName = operatorInfo.Name;
                        }

                        financialComponentDtos.Add(_mapper.Map<FinancialComponentDto>(financialComponent));
                    }
                }
                else
                {
                    var financialComponentList = await _financialComponentMasterRepository.GetAllAsync();
                    financialComponentList = financialComponentList
                        .Where(x => x.ServiceType == request.ServiceType && x.Type == request.Type)
                        .ToList();

                    foreach (var item in financialComponentList)
                    {
                        var userFinancialComponent = new UserFinancialComponent()
                        {
                            UserId = request.UserId,
                            ServiceType = item.ServiceType,
                            ServiceSubType = item.ServiceSubType,
                            Type = item.Type,
                            Start_Value = item.Start_Value,
                            End_Value = item.End_Value,
                            Value = item.Value,
                            CalculationType = item.CalculationType,
                            OperatorId = item.OperatorId,
                        };

                        var createResponse = await _userFinancialComponentRepository.AddAsync(userFinancialComponent);

                        var financialComponent = _mapper.Map<FinancialComponentDto>(item);
                        financialComponent.Id = createResponse;

                        if (financialComponent.OperatorId.HasValue)
                        {
                            var operatorInfo = operatorList.Where(x => x.Id == (long)financialComponent.OperatorId).FirstOrDefault();

                            financialComponent.OperatorName = operatorInfo.Name;
                        }

                        financialComponentDtos.Add(_mapper.Map<FinancialComponentDto>(financialComponent));
                    }
                }

                return ApiResponse<List<FinancialComponentDto>>.SuccessResponse(financialComponentDtos);
            }
            catch (Exception ex)
            {
                return ApiResponse<List<FinancialComponentDto>>.BadRequestResponse($"An error occurred while updating: {ex.Message}");
            }
        }
    }
}
