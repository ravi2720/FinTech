namespace FinTech_ApiPanel.Domain.Entities.OperatorMasters
{
    public class OperatorMaster
    {
        public long Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Code { get; set; } = string.Empty;
        public byte Type { get; set; }
        public decimal Commission { get; set; }
        public bool IsActive { get; set; }
    }
}
