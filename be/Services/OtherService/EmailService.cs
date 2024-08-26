using MimeKit;
using MimeKit.Text;
using MailKit.Net.Smtp;
using MailKit.Security;

using be.Helper;
using be.Models;

namespace be.Services.OtherService
{
    public class EmailService
    {
        private static EmailService instance;

        public static EmailService Instance
        {
            get { if (instance == null) instance = new EmailService(); return EmailService.instance; }
            private set { EmailService.instance = value; }
        }

        public async Task<bool> SendMail(string mail, int type, string fullname, string account, string password)
        {
            try
            {
                // With type == 1, create account
                // With type == 2, forgot password
                // With type == 3, update password
                string _text = "";
                if (type == 1)
                {
                    _text = EmailHelper.Instance.BodyRegisterMail(fullname, account, password);
                }
                if (type == 2)
                {
                    _text = EmailHelper.Instance.BodyForgotMail(fullname, account, password);
                }
                //else
                //{
                //    _text = EmailHelper.Instance.BodyUpdate(fullname, account, password);
                //}
                var email = new MimeMessage();
                email.From.Add(MailboxAddress.Parse("jira.service.fpt@gmail.com"));
                email.To.Add(MailboxAddress.Parse(mail));
                email.Subject = "SWM COMFIRMED ACCOUNT";
                email.Body = new TextPart(TextFormat.Html)
                {
                    Text = _text
                };
                var smtp = new SmtpClient();
                await smtp.ConnectAsync("smtp.gmail.com", 587, false);
                await smtp.AuthenticateAsync("jira.service.fpt@gmail.com", "hcjizzxjmyobukzt");
                await smtp.SendAsync(email);
                await smtp.DisconnectAsync(true);
                return true;
            }
            catch (Exception)
            {
                return false;
            }

        }
    }
}
