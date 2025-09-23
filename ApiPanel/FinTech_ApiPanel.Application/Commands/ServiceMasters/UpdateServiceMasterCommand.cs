using FinTech_ApiPanel.Application.Common;
using MediatR;

namespace FinTech_ApiPanel.Application.Commands.ServiceMasters
{
    public record UpdateServiceMasterCommand : IRequest<ApiResponse>
    {
        public long Id { get; set; }
        public string Title { get; set; } 
        public byte Type { get; set; }
    }
}
