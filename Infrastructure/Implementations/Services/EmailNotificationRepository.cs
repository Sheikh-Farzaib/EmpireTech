using Application.Interfaces.IServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Infrastructure.AppSections;
using MailKit.Net.Smtp;
using MimeKit;

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
            //var message = new MimeMessage();
            //message.From.Add(new MailboxAddress("Your Name", _smtpSettings.Username));
            //message.To.Add(new MailboxAddress("", to));
            //message.Subject = subject;
            //message.Body = new TextPart("html") { Text = body };

            //using (var client = new MailKit.Net.Smtp.SmtpClient())
            //{
            //    try
            //    {
            //        await client.ConnectAsync("smtp.office365.com", 587, MailKit.Security.SecureSocketOptions.StartTls);
            //        await client.AuthenticateAsync(_smtpSettings.Username, _smtpSettings.Password); // Use app password here
            //        await client.SendAsync(message);
            //    }
            //    catch (Exception ex)
            //    {
            //        // Handle exception
            //        throw;
            //    }
            //    finally
            //    {
            //        await client.DisconnectAsync(true);
            //    }
            //}
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
