using MailKit.Net.Smtp;
using Microsoft.Extensions.Options;
using MimeKit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VoxU_Backend.Core.Application.DTOS.Email;
using VoxU_Backend.Core.Application.Interfaces.Services;
using VoxU_Backend.Core.Domain.Settings;

namespace VoxU_Backend.Persistence.Shared.Service
{
    public class EmailService : IEmailService
    {
        private readonly MailSettings _mailSettings;

        public EmailService(IOptions<MailSettings> mailSettings)
        {
            _mailSettings = mailSettings.Value;
        }

        public async Task SendAsync(EmailRequest emailRequest)
        {
            //Almando el mensaje que es del tipo mimeMessage
            MimeMessage Email = new();
            Email.Sender = MailboxAddress.Parse($"{_mailSettings.DisplayName} <{_mailSettings.EmailFrom}>");
            Email.To.Add(MailboxAddress.Parse(emailRequest.To));
            Email.Subject = emailRequest.Subject;
            BodyBuilder bodyBuilder = new();
            bodyBuilder.HtmlBody = emailRequest.Body;
            Email.Body = bodyBuilder.ToMessageBody();

            try
            {
                using SmtpClient smtpClient = new();
                smtpClient.ServerCertificateValidationCallback = (s, c, h, e) => true;
                await smtpClient.ConnectAsync(_mailSettings.SmtpHost, _mailSettings.SmtpPort, MailKit.Security.SecureSocketOptions.StartTls);
                smtpClient.Authenticate(_mailSettings.SmtpUser, _mailSettings.SmtpPass);
                await smtpClient.SendAsync(Email);
                smtpClient.Disconnect(true);

            }
            catch (Exception ex)
            {

            }


        }
    }
}
