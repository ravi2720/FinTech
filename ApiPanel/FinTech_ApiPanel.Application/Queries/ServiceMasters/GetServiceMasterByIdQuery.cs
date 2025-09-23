using FinTech_ApiPanel.Application.Common;
using FinTech_ApiPanel.Domain.DTOs.Services;
using FinTech_ApiPanel.Domain.Entities.ServiceMasters;
using MediatR;

namespace FinTech_ApiPanel.Application.Queries.ServiceMasters
{
    public record GetServiceMasterByIdQuery(long Id) : IRequest<ApiResponse<ServiceDto>>;

}
