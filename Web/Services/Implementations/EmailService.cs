using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;
using System.Web;
using Web.Services.Interfaces;

namespace Web.Services.Implementations
{
    public class EmailService: IEmailService
    {
        private SmtpClient SmtpClient { get; }
        private SmtpSettings Settings{ get;}

        public EmailService()
        {
            SmtpClient = new SmtpClient();
            var server = ConfigurationManager.AppSettings["SmtpServer"];
            var port = int.Parse(ConfigurationManager.AppSettings["SmtpPort"]);
            var username = ConfigurationManager.AppSettings["SmtpUsername"];
            var password = ConfigurationManager.AppSettings["SmtpPassword"];
            var useSsl = bool.Parse(ConfigurationManager.AppSettings["SmtpSSL"]);

            if (string.IsNullOrEmpty(server) || string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
            {
                throw new ArgumentNullException(server, "SMTP settings not correct");
            }

            Settings = new SmtpSettings() {Server = server, Password = password, UserName = username, Post = port, UseSsl = useSsl};

            SetSettings(Settings);
        }
        public async Task<bool> SendAsync(MailMessage message)
        {
            try
            {
                await SmtpClient.SendMailAsync(message);
                return true;
            }
            catch
            {
                return false;
            }
            
        }

        private void SetSettings(SmtpSettings settings)
        {
            this.SmtpClient.Host = settings.Server;
            this.SmtpClient.Port = settings.Post;
            this.SmtpClient.EnableSsl = settings.UseSsl;
            this.SmtpClient.Credentials = new NetworkCredential(settings.UserName, settings.Password);
        }

        private class SmtpSettings
        {
            public string Server { get; set; }
            public int Post { get; set; }
            public bool UseSsl { get; set; }
            public string UserName { get; set; }
            public string Password { get; set; }
        }
    }
}