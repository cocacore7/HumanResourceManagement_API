using System.Net;
using System.Net.Mail;
using HRM_API.Configuration;
using Microsoft.Extensions.Configuration;

namespace HRM_API.Application.Services
{
    public class MailService(TemplateAdapterFactory factory, ISettings settings)
    {
        private readonly TemplateAdapterFactory _factory = factory;
        private readonly ISettings _settings = settings;
        private readonly string _smtpHost = _settings.SmtHost;
        private readonly int _smtpPort = _settings.SmtPort;
        private readonly string _smtpUser = _settings.EmailAddress;
        private readonly string _smtpPass = _settings.EmailKey;
        private readonly string _fromAddress = _settings.FromAddress;

        public async Task<bool> SendEmailFromTemplateAsync(string toEmail, string subject, string templateName, int id)
        {
            // Obtiene el adaptador correcto
            var adapter = _factory.GetAdapter(templateName);

            // Genera el cuerpo del mensaje
            var htmlBody = await adapter.BuildBodyAsync(id);

            using var smtp = new SmtpClient(_smtpHost, _smtpPort)
            {
                EnableSsl = true,
                Credentials = new NetworkCredential(_smtpUser, _smtpPass)
            };

            var mail = new MailMessage
            {
                From = new MailAddress(_fromAddress, "Forza Delivery Express"),
                Subject = subject,
                Body = htmlBody,
                IsBodyHtml = true
            };

            mail.To.Add(toEmail);
            await smtp.SendMailAsync(mail);

            return true;
        }   
    }
}