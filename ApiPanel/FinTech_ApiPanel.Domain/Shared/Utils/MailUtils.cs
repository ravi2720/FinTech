using FinTech_ApiPanel.Domain.DTOs.Mails;
using FinTech_ApiPanel.Domain.Shared.Enums;
using MailKit.Security;
using MimeKit;

namespace FinTech_ApiPanel.Domain.Shared.Utils
{
    public class MailUtils
    {
        public static async Task<string> SendEmail(MailSettingDto mailSetting, MailDataDto data, EmailType type)
        {
            try
            {
                string senderEmail = mailSetting.SenderEmail;
                string displayName = mailSetting.SenderName;
                string senderPassword = mailSetting.Password;

                string recieverEmail = data.Email;

                string smtpHost = mailSetting.SmtpHost;
                int smtpPort = mailSetting.SmtpPort;

                var email = new MimeMessage();
                email.Sender = new MailboxAddress(displayName, senderEmail);

                email.To.Add(MailboxAddress.Parse(recieverEmail));

                var builder = new BodyBuilder();

                string logoPath = "";

                MailContentDto content = new MailContentDto();

                GetEmailTemplate(type, data, content);
                email.Subject = content.Subject;
                builder.HtmlBody = content.Body;

                //Add PDF file as an attachment
                foreach (var attach in data.MailAttachments)
                {
                    if (attach.File.Length > 0)
                    {
                        var attachment = new MimePart("application", Path.GetExtension(attach.FileName))
                        {
                            Content = new MimeContent(new MemoryStream(attach.File), ContentEncoding.Default),
                            ContentDisposition = new ContentDisposition(ContentDisposition.Attachment),
                            ContentTransferEncoding = ContentEncoding.Base64,
                            FileName = attach.FileName
                        };
                        builder.Attachments.Add(attachment);
                    }
                }

                email.Body = builder.ToMessageBody();
                using var smtp = new MailKit.Net.Smtp.SmtpClient();
                await smtp.ConnectAsync(smtpHost, smtpPort, SecureSocketOptions.StartTls);
                await smtp.AuthenticateAsync(senderEmail, senderPassword);
                await smtp.SendAsync(email);
                await smtp.DisconnectAsync(true);

                return "Email send successfully";
            }
            catch (Exception ex)
            {
                // Log the exception or handle it as needed
                throw new Exception("Error sending email: " + ex.Message, ex);
            }
        }

        private static string GetEmailTemplate(EmailType type, MailDataDto data, MailContentDto content)
        {
            string emailTemplate = null;

            if (type == EmailType.PasswordReset)
            {
                PasswordResetTemplate(data, content);
            }
            else if (type == EmailType.WelcomeCustomer)
            {
                WelcomeCustomerTemplate(data, content);
            }
            else if (type == EmailType.VarifyEmail)
            {
                VerifyEmailTemplate(data, content);
            }
            else if (type == EmailType.ChangeIPin)
            {
                ChangeIpinTemplate(data, content);
            }

            return emailTemplate;
        }

        static void PasswordResetTemplate(MailDataDto data, MailContentDto content)
        {
            content.Subject = "Password Reset OTP";

            content.Body = $@"<!DOCTYPE html>
                    <html>
                       <head>
                          <meta charset=""UTF-8"">
                          <title>Password Reset OTP</title>
                       </head>
                       <body>
                          <div>
                             <h2>Password Reset Request</h2>
                             <p>Dear {data.FullName},</p>
                             <p>You have requested to reset your password. Please use the following OTP to reset your password:</p>
                             <p><strong>OTP: {data.OTP}</strong></p>
                             <p>This OTP is valid for 10 minuts. If you did not request a password reset, please ignore this email.</p>
                             <p>Thank you,</p>
                             <p>NWS Global</p>
                          </div>
                       </body>
                    </html>";
        }

        static void ChangeIpinTemplate(MailDataDto data, MailContentDto content)
        {
            content.Subject = "Change Ipin OTP";

            content.Body = $@"<!DOCTYPE html>
                    <html>
                       <head>
                          <meta charset=""UTF-8"">
                          <title>Ipin change OTP</title>
                       </head>
                       <body>
                          <div>
                             <h2>Change Ipin Request</h2>
                             <p>Dear {data.FullName},</p>
                             <p>You have requested to change Ipin. Please use the following OTP to chnage Ipin:</p>
                             <p><strong>OTP: {data.OTP}</strong></p>
                             <p>This OTP is valid for 10 minuts. If you did not request a Ipin reset, please ignore this email.</p>
                             <p>Thank you,</p>
                             <p>NWS Global</p>
                          </div>
                       </body>
                    </html>";
        }
        static void VerifyEmailTemplate(MailDataDto data, MailContentDto content)
        {
            content.Subject = "Verify Your Email";

            content.Body = $@"<!DOCTYPE html>
                    <html>
                       <head>
                          <meta charset=""UTF-8"">
                          <title>Verify Your Email</title>
                       </head>
                       <body>
                          <div>
                             <h2>Password Reset Request</h2>
                             <p>Dear {data.FullName},</p>
                             <p>You have requested to verify your email. Please use the following OTP to verify your email:</p>
                             <p><strong>OTP: {data.OTP}</strong></p>
                             <p>This OTP is valid for 10 minutes. If you did not request an email verification, please ignore this email.</p>
                             <p>Thank you,</p>
                             <p>NWS Global</p>
                          </div>
                       </body>
                    </html>";
        }
        static void WelcomeCustomerTemplate(MailDataDto data, MailContentDto content)
        {
            content.Subject = "Welcome to SolarERP";

            content.Body = $@"<!DOCTYPE html>
                        <html>
                        <head>
                            <meta charset=""UTF-8"">
                            <title>Welcome to SolarERP</title>
                        </head>
                        <body>
                            <div>
                                <h2>Welcome to SolarERP!</h2>
                                <p>Dear {data.FullName},</p>
                                <p>We are delighted to welcome you to SolarERP.</p>
                                <p>Thank you for joining us. We look forward to serving you and providing you with a great experience.</p>
                                <p>If you have any questions or need assistance, feel free to contact our support team.</p>
                                <p>Best regards,</p>
                                <p>NWS Global</p>
                            </div>
                        </body>
                        </html>
                        ";
        }
    }
}
