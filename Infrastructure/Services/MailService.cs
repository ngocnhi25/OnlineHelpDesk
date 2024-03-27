using Application.DTOs.Auth;
using Application.Services;
using Domain.Entities.Settings;
using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.Extensions.Options;
using MimeKit;
using MimeKit.Text;

namespace DemoSendMail.Services
{
    public class MailService : IMailService
    {
        private readonly MailSetting _mailSettings;

        public MailService(IOptions<MailSetting> mailSettings)
        {
            _mailSettings = mailSettings.Value;
        }
        public async Task SendMailAsync(MailRequest mailRequest)
        {
            var send = new MimeMessage();
            send.From.Add(MailboxAddress.Parse(_mailSettings.Mail));
            send.To.Add(MailboxAddress.Parse(mailRequest.ToEmail));
            send.Subject = mailRequest.Subject;
            //send.Body = new TextPart(TextFormat.Html) { Text = "<a href=\"https://jasonwatmore.com/post/2022/03/11/net-6-send-an-email-via-smtp-with-mailkit\">Example HTML Message Body</h1>" };

            var builder = new BodyBuilder();

            if (mailRequest.Attachments != null)
            {
                byte[] fileBytes;
                foreach (var file in mailRequest.Attachments)
                {
                    if (file.Length > 0)
                    {
                        using (var ms = new MemoryStream())
                        {
                            file.CopyTo(ms);
                            fileBytes = ms.ToArray();
                        }
                        builder.Attachments.Add(file.FileName, fileBytes, ContentType.Parse(file.ContentType));
                    }
                }
            }

            builder.HtmlBody = mailRequest.Body;
            send.Body = new TextPart(TextFormat.Html) { Text = $"<h1>{mailRequest.Body}</h1>" };
            using var smtp = new SmtpClient();
            smtp.Connect(_mailSettings.Host, _mailSettings.Port, SecureSocketOptions.StartTls);
            smtp.Authenticate(_mailSettings.Mail, _mailSettings.Password);
            await smtp.SendAsync(send);
            smtp.Disconnect(true);
        }
    }
}
