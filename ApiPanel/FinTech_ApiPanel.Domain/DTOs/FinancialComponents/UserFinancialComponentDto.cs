namespace FinTech_ApiPanel.Domain.DTOs.FinancialComponents
{
    public class UserFinancialComponentDto
    {
        public long? OperatorId { get; set; }
        public byte? ServiceSubType { get; set; }
        public decimal? Start_Value { get; set; }
        public decimal? End_Value { get; set; }
        public decimal Value { get; set; }
        public byte? CalculationType { get; set; }
    }
}
