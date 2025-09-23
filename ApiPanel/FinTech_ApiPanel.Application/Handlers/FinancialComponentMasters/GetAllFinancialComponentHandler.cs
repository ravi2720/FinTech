using AutoMapper;
using FinTech_ApiPanel.Application.Abstraction.ITokenServices;
using FinTech_ApiPanel.Application.Common;
using FinTech_ApiPanel.Application.Queries.FinancialComponentMasters;
using FinTech_ApiPanel.Domain.DTOs.FinancialComponents;
using FinTech_ApiPanel.Domain.Interfaces.IFinancialComponentMasters;
using FinTech_ApiPanel.Domain.Interfaces.IOperatorMasters;
using FinTech_ApiPanel.Domain.Interfaces.IUserFinancialComponents;
using MediatR;

namespace FinTech_ApiPanel.Application.Handlers.FinancialComponentMasters
{
    public class GetAllFinancialComponentHandler : IRequestHandler<GetAllFinancialComponentQuery, ApiResponse<List<FinancialComponentDto>>>
    {
        private readonly IFinancialComponentMasterRepository _financialComponentMasterRepository;
        private readonly IUserFinancialComponentRepository _userFinancialComponentRepository;
        private readonly IMapper _mapper;
        private readonly ITokenService _tokenService;
        private readonly IOperatorRepository _operatorRepository;

        public GetAllFinancialComponentHandler(IFinancialComponentMasterRepository financialComponentMasterRepository,
            IMapper mapper,
            ITokenService tokenService,
            IUserFinancialComponentRepository userFinancialComponentRepository,
            IOperatorRepository operatorRepository)
        {
            _financialComponentMasterRepository = financialComponentMasterRepository;
            _mapper = mapper;
            _tokenService = tokenService;
            _userFinancialComponentRepository = userFinancialComponentRepository;
            _operatorRepository = operatorRepository;
        }

        public async Task<ApiResponse<List<FinancialComponentDto>>> Handle(GetAllFinancialComponentQuery request, CancellationToken cancellationToken)
        {
            try
            {
                if (request.Type == null && request.ServiceType == null)
                    return ApiResponse<List<FinancialComponentDto>>.BadRequestResponse("Type and ServiceType cannot be null.");

                var loggedInUser = _tokenService.GetLoggedInUserinfo();
                if (loggedInUser == null)
                    return ApiResponse<List<FinancialComponentDto>>.UnauthorizedResponse("User not authorized.");

                List<FinancialComponentDto> financialComponentDtos = new List<FinancialComponentDto>();

                var operatorList = _operatorRepository.GetAll();

                if (loggedInUser.IsAdmin)
                {
                    var financialComponentList = await _financialComponentMasterRepository.GetAllAsync();
                    financialComponentList = financialComponentList
                        .Where(x => x.ServiceType == request.ServiceType && x.Type == request.Type)
                        .ToList();

                    foreach (var item in financialComponentList)
                    {
                        var financialComponent = _mapper.Map<FinancialComponentDto>(item);

                        if (financialComponent.OperatorId.HasValue)
                        {
                            var operatorInfo = operatorList.Where(x=>x.Id==(long)financialComponent.OperatorId).FirstOrDefault();

                            financialComponent.OperatorName = operatorInfo.Name;
                        }

                        financialComponentDtos.Add(_mapper.Map<FinancialComponentDto>(financialComponent));
                    }
                }
                else
                {
                    var userFinancialComponentList = await _userFinancialComponentRepository.GetAllAsync();

                    userFinancialComponentList = userFinancialComponentList
                    .Where(x => x.Type == request.Type && x.ServiceType == request.ServiceType && x.UserId == loggedInUser.UserId)
                    .ToList();

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
                            var financialComponent = _mapper.Map<FinancialComponentDto>(item);

                            if (financialComponent.OperatorId.HasValue)
                            {
                                var operatorInfo = operatorList.Where(x => x.Id == (long)financialComponent.OperatorId).FirstOrDefault();

                                financialComponent.OperatorName = operatorInfo.Name;
                            }
                            financialComponentDtos.Add(financialComponent);
                        }
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
