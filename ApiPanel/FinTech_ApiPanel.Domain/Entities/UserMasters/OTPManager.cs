namespace FinTech_ApiPanel.Domain.Entities.UserMasters
{
    public class OTPManager
    {
        public long Id { get; set; }
        public byte Type { get; set; }
        public string? Email { get; set; }
        public string? PhoneNumber { get; set; }
        public string? Token { get; set; }
        public DateTime? TokenValidTill { get; set; }
        public long Otp { get; set; }
        public DateTime OtpValidTill { get; set; }
        public bool IsOtpVerified { get; set; }
    }
}
