using System.Net;
using System.Net.Mail;
using Microsoft.Extensions.Configuration;

namespace HRM_API.Application.Services
{
    public class MailService
    {
        private readonly TemplateAdapterFactory _factory;
        private readonly string _smtpHost;
        private readonly int _smtpPort;
        private readonly string _smtpUser;
        private readonly string _smtpPass;
        private readonly string _fromAddress;

        public MailService(TemplateAdapterFactory factory, IConfiguration config)
        {
            _factory = factory;
            _smtpHost = config["Mail:SmtpHost"];
            _smtpPort = int.Parse(config["Mail:SmtpPort"]);
            _smtpUser = config["Mail:SmtpUser"];
            _smtpPass = config["Mail:SmtpPass"];
            _fromAddress = config["Mail:FromAddress"];
        }

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