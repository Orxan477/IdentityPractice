using System.Net;
using System.Net.Mail;

namespace LoginRegisterPractice.Utilities
{
    public static class Email
    {
        public static void SendEmailAsync(string fromMail,string password,string toMail,string body,string subject)
        {
            using (var client = new SmtpClient("smtp.googlemail.com", 587))
            {
                client.Credentials = new NetworkCredential(fromMail, password);
                client.EnableSsl = true;
                var message = new MailMessage(fromMail, toMail);
                message.Body = body;
                message.Subject = subject;
                message.IsBodyHtml = true;
                client.Send(message);
            }
        }
    }
}
