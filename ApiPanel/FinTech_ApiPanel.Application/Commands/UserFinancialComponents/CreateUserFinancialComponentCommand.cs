using FinTech_ApiPanel.Application.Common;
using FinTech_ApiPanel.Domain.DTOs.FinancialComponents;
using MediatR;

namespace FinTech_ApiPanel.Application.Commands.UserFinancialComponents
{
    public class CreateUserFinancialComponentCommand : IRequest<ApiResponse>
    {
        public long UserId { get; set; }
        public byte Type { get; set; }
        public byte ServiceType { get; set; }
        public List<UserFinancialComponentDto> Components { get; set; }
    }
}
