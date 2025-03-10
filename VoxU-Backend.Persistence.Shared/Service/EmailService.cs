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

            /*MimeMessage email = new MimeMessage();
    email.Sender = MailboxAddress.Parse($"{_mailSettings.DisplayName} <{_mailSettings.EmailFrom}>");
    email.To.Add(MailboxAddress.Parse(emailRequest.To));
    email.Subject = emailRequest.Subject;

    // Construcción del cuerpo del correo con estilo
    BodyBuilder bodyBuilder = new BodyBuilder();
    
    // Contenido HTML con CSS inline para garantizar compatibilidad
    bodyBuilder.HtmlBody = $@"
        <html>
        <head>
            <style>
                /* Estilos básicos para el cuerpo del correo */
                body {{
                    font-family: Arial, sans-serif;
                    color: #333333;
                    margin: 0;
                    padding: 20px;
                }}
                .email-container {{
                    width: 100%;
                    max-width: 600px;
                    margin: 0 auto;
                    background-color: #f4f4f4;
                    border-radius: 8px;
                    box-shadow: 0 4px 6px rgba(0, 0, 0, 0.1);
                }}
                .email-header {{
                    background-color: #4CAF50;
                    color: white;
                    padding: 20px;
                    border-radius: 8px 8px 0 0;
                    text-align: center;
                }}
                .email-content {{
                    padding: 20px;
                    background-color: #ffffff;
                    border-radius: 0 0 8px 8px;
                }}
                .email-footer {{
                    padding: 10px;
                    background-color: #f4f4f4;
                    text-align: center;
                    font-size: 12px;
                    color: #888888;
                }}
                h1 {{
                    font-size: 24px;
                    margin-bottom: 10px;
                }}
                p {{
                    font-size: 16px;
                    line-height: 1.5;
                }}
                .cta-button {{
                    display: inline-block;
                    padding: 10px 20px;
                    background-color: #4CAF50;
                    color: white;
                    text-decoration: none;
                    border-radius: 5px;
                    font-weight: bold;
                    margin-top: 20px;
                }}
                .cta-button:hover {{
                    background-color: #45a049;
                }}
            </style>
        </head>
        <body>
            <div class='email-container'>
                <div class='email-header'>
                    <h1>Bienvenido a VoxU</h1>
                </div>
                <div class='email-content'>
                    <h2>{emailRequest.Subject}</h2>
                    <p>{emailRequest.Body}</p>
                    <a href='#' class='cta-button'>Acceder a la aplicación</a>
                </div>
                <div class='email-footer'>
                    <p>&copy; {DateTime.Now.Year} VoxU. Todos los derechos reservados.</p>
                    <p>Si tienes alguna pregunta, no dudes en contactarnos.</p>
                </div>
            </div>
        </body>
        </html>";

    email.Body = bodyBuilder.ToMessageBody();

    // Enviar el correo
    try
    {
        using (SmtpClient smtpClient = new SmtpClient())
        {
            smtpClient.ServerCertificateValidationCallback = (s, c, h, e) => true;
            await smtpClient.ConnectAsync(_mailSettings.SmtpHost, _mailSettings.SmtpPort, MailKit.Security.SecureSocketOptions.StartTls);
            smtpClient.Authenticate(_mailSettings.SmtpUser, _mailSettings.SmtpPass);
            await smtpClient.SendAsync(email);
            smtpClient.Disconnect(true);
        }
    }
    catch (Exception ex)
    {
        // Aquí puedes manejar el error si es necesario
        Console.WriteLine("Error al enviar el correo: " + ex.Message);
    }*/


        }
    }
}
