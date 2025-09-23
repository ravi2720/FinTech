using FinTech_ApiPanel.Domain.Shared.Audits;

namespace FinTech_ApiPanel.Domain.Entities.FinancialComponentMasters
{
    public class FinancialComponentMaster : AuditedEntity
    {
        public long Id { get; set; }
        public long? OperatorId { get; set; }
        public byte Type { get; set; }
        public decimal? Start_Value { get; set; }
        public decimal? End_Value { get; set; }
        public decimal Value { get; set; }
        public byte ServiceType { get; set; }
        public byte? ServiceSubType { get; set; }
        public byte? CalculationType { get; set; }
    }
}
