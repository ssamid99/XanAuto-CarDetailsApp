using Microsoft.Extensions.Options;
using System.Net.Mail;
using System.Net;
using System.Threading.Tasks;
using System;

namespace XanAuto.Application.AppCode.Services
{
    public class EmailService
    {
        private readonly EmailServiceOptions options;

        public EmailService(IOptions<EmailServiceOptions> options)
        {
            this.options = options.Value;
        }

        public async Task<bool> SendMailAsync(string toEmail, string subject, string messageText)
        {
            try
            {

                var client = new SmtpClient(options.SmtpServer, Convert.ToInt32(options.SmtpPort));
                client.Credentials = new NetworkCredential(options.UserName, options.Password);
                client.EnableSsl = true;

                var from = new MailAddress(options.UserName, options.DisplayName);
                var to = new MailAddress(toEmail);

                var message = new MailMessage(from, to);
                message.Subject = subject;
                message.Body = messageText;
                message.IsBodyHtml = true;

                await client.SendMailAsync(message);
                return true;
            }
            catch (System.Exception)
            {
                return false;
            }
        }
    }
    public class EmailServiceOptions
    {
        public string DisplayName { get; set; }
        public string SmtpServer { get; set; }
        public int SmtpPort { get; set; }
        public bool EnableSsl { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string Cc { get; set; }


    }
}

