using FinTech_ApiPanel.Application.Common;
using MediatR;

namespace FinTech_ApiPanel.Application.Queries.UserMaster
{
    public record ValidateIpinQuery(long Ipin) : IRequest<ApiResponse>;
}
