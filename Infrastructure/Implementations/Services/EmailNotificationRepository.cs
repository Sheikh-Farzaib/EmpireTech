using Application.Interfaces.IServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Infrastructure.AppSections;


namespace Infrastructure.Implementations.Services
{
    public class EmailNotificationRepository : IEmailNotificationRepository
    {
        private readonly SmtpSettings _smtpSettings;

        public EmailNotificationRepository(SmtpSettings smtpSettings)
        {
            _smtpSettings = smtpSettings;
        }

        public async Task SendEmailAsync(string to, string subject, string body)
        {
            try
            {
                using (var mailMessage = new MailMessage())
                {
                    mailMessage.From = new MailAddress(_smtpSettings.FromAddress);
                    mailMessage.To.Add(to);
                    mailMessage.Subject = subject;
                    mailMessage.Body = $"<p>Thank you for registering. your verification token is: <b>{body}</b> </p>";
                    mailMessage.IsBodyHtml = true;

                    using (var smtpClient = new System.Net.Mail.SmtpClient(_smtpSettings.Host, _smtpSettings.Port))
                    {
                        smtpClient.Credentials = new NetworkCredential(_smtpSettings.Username, _smtpSettings.Password);
                        smtpClient.EnableSsl = _smtpSettings.EnableSsl;

                        await smtpClient.SendMailAsync(mailMessage);
                    }
                }
            }
            catch (Exception ex)
            {

                throw;
            }

        }
    }
}
