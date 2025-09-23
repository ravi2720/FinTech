using FinTech_ApiPanel.Application.Common;
using MediatR;

namespace FinTech_ApiPanel.Application.Commands.FinancialComponentMasters
{
    public class CreateFinancialComponentCommand : IRequest<ApiResponse>
    {
        public byte Type { get; set; }
        public decimal? Start_Value { get; set; }
        public decimal? End_Value { get; set; }
        public decimal Value { get; set; }
        public byte ServiceType { get; set; }
        public byte? ServiceSubType { get; set; }
        public byte? CalculationType { get; set; }
        public long? OperatorId { get; set; }
    }
}
