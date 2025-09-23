namespace FinTech_ApiPanel.Domain.DTOs.Mails
{
    public class MailDataDto
    {
        public MailDataDto()
        {
            MailAttachments = new List<MailAttachmentDto>();
        }

        public string FullName { get; set; }
        public string Email { get; set; }

        public long? OTP { get; set; }
        public string? Password { get; set; }

        public List<MailAttachmentDto> MailAttachments { get; set; }
    }
}
