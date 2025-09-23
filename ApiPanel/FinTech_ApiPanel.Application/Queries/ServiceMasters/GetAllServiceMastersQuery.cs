using FinTech_ApiPanel.Application.Common;
using FinTech_ApiPanel.Domain.DTOs.Services;
using MediatR;

namespace FinTech_ApiPanel.Application.Queries.ServiceMasters
{
    public record GetAllServiceMastersQuery() : IRequest<ApiResponse<IEnumerable<ServiceDto>>>;

}
