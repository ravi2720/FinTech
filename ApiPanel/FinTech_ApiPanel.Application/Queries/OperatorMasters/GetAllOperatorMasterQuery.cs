using FinTech_ApiPanel.Application.Common;
using FinTech_ApiPanel.Domain.DTOs.OperatorMasters;
using MediatR;

namespace FinTech_ApiPanel.Application.Queries.OperatorMasters
{
    public record GetAllOperatorMasterQuery(byte? Type) : IRequest<ApiResponse<List<OperatorMasterDto>>>;
}
