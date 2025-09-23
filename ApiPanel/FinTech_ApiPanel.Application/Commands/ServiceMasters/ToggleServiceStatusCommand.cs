using FinTech_ApiPanel.Application.Common;
using MediatR;

namespace FinTech_ApiPanel.Application.Commands.ServiceMasters
{
    public class ToggleServiceStatusCommand : IRequest<ApiResponse>
    {
        public long UserId { get; set; }
        public long ServiceId { get; set; }
    };
}
