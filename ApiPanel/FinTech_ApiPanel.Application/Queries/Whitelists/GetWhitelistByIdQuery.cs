using FinTech_ApiPanel.Application.Common;
using FinTech_ApiPanel.Domain.DTOs.Whitelists;
using MediatR;

namespace FinTech_ApiPanel.Application.Queries.Whitelists
{
    public record GetWhitelistByIdQuery(long Id) : IRequest<ApiResponse<WhitelistDto>>;
}
