using FinTech_ApiPanel.Application.Common;
using MediatR;
using System.ComponentModel.DataAnnotations;

namespace FinTech_ApiPanel.Application.Commands.ServiceMasters
{
    public class CreateServiceMasterCommand : IRequest<ApiResponse>
    {
        [Required]
        public string Title { get; set; }
        [Required]
        public byte Type { get; set; }
    };
}
