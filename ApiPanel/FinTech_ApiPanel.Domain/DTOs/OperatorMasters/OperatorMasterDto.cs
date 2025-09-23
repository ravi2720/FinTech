namespace FinTech_ApiPanel.Domain.DTOs.OperatorMasters
{
    public class OperatorMasterDto
    {
        public long Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public byte Type { get; set; }
    }
}
