using FinTech_ApiPanel.Application.Common;
using FinTech_ApiPanel.Application.Queries.OperatorMasters;
using FinTech_ApiPanel.Domain.DTOs.OperatorMasters;
using FinTech_ApiPanel.Domain.Interfaces.IOperatorMasters;
using MediatR;

namespace FinTech_ApiPanel.Application.Handlers.OperatorMasters
{
    public class GetAllOperatorMasterHandler : IRequestHandler<GetAllOperatorMasterQuery, ApiResponse<List<OperatorMasterDto>>>
    {
        private readonly IOperatorRepository _operatorRepository;

        public GetAllOperatorMasterHandler(IOperatorRepository operatorRepository)
        {
            _operatorRepository = operatorRepository;
        }

        public async Task<ApiResponse<List<OperatorMasterDto>>> Handle(GetAllOperatorMasterQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var operatorMaster = _operatorRepository.GetAll().ToList();
                if (operatorMaster == null || !operatorMaster.Any())
                    return ApiResponse<List<OperatorMasterDto>>.SuccessResponse();

                if (request.Type.HasValue)
                    operatorMaster = operatorMaster.Where(b => b.Type == request.Type).ToList();

                List<OperatorMasterDto> result = new List<OperatorMasterDto>();

                foreach (var om in operatorMaster)
                {
                    OperatorMasterDto dto = new OperatorMasterDto();
                    dto.Id = om.Id;
                    dto.Name = om.Name;
                    dto.Type = om.Type;

                    result.Add(dto);
                }

                return ApiResponse<List<OperatorMasterDto>>.SuccessResponse(result);
            }
            catch (Exception ex)
            {
                return ApiResponse<List<OperatorMasterDto>>.BadRequestResponse($"An error occurred while retrieving the banks: {ex.Message}");
            }
        }

    }
}
