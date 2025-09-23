using FinTech_ApiPanel.Application.Common;
using MediatR;

namespace FinTech_ApiPanel.Application.Queries.UserMaster
{
    public class GetClientSecretQuery : IRequest<ApiResponse<object>>;
}
