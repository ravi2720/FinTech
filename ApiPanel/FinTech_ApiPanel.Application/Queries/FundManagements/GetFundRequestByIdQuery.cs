using FinTech_ApiPanel.Application.Common;
using FinTech_ApiPanel.Domain.DTOs.FundRequests;
using MediatR;

namespace FinTech_ApiPanel.Application.Queries.FundManagements
{
    public record GetFundRequestByIdQuery(long Id) : IRequest<ApiResponse<FundRequestDto>>;
}
