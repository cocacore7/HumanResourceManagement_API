using HRM_API.Configuration;
using System.Net;
using System.Net.Mail;

namespace HRM_API.Application.Helpers
{
    public class MailHelper(TemplateAdapterFactory factory, ISettings settings)
    {
        private readonly TemplateAdapterFactory _factory = factory;
        private readonly ISettings _settings = settings;

        public async Task<bool> SendEmailFromTemplateAsync(string toEmail, string subject, string templateName, int id)
        {
            // Obtiene el adaptador correcto
            var adapter = _factory.GetAdapter(templateName);

            // Genera el cuerpo del mensaje
            var htmlBody = await adapter.BuildBodyAsync(id);

            using var smtp = new SmtpClient(_settings.SmtHost, _settings.SmtPort ?? 0)
            {
                EnableSsl = true,
                Credentials = new NetworkCredential(_settings.EmailAddress, _settings.EmailKey)
            };

            var mail = new MailMessage
            {
                From = new MailAddress(_settings.FromAddress, "Forza Delivery Express"),
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