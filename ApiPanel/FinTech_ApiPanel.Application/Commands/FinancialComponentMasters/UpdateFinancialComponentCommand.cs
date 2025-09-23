using FinTech_ApiPanel.Application.Common;
using MediatR;

namespace FinTech_ApiPanel.Application.Commands.FinancialComponentMasters
{
    public class UpdateFinancialComponentCommand : IRequest<ApiResponse>
    {
        public long Id { get; set; }
        public decimal Start_Value { get; set; }
        public decimal End_Value { get; set; }
        public decimal Value { get; set; }
        public byte CalculationType { get; set; }
    };
}
