using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MailKit.Net.Smtp;
using MailKit.Security;
using MimeKit;
using MimeKit.Text;
using MimeKit.Utils;

//nclaudiu007@outlook
//Parola1234

namespace ABD_Project
{
    public static class EmailSender
    {
        public static void sendEmail(string to,string subject, string body, int count)
        {
            var email = new MimeMessage();
            email.From.Add(MailboxAddress.Parse("nclaudiu007@outlook.com"));
            email.To.Add(MailboxAddress.Parse(to));
            email.Subject = subject;

            var bodyBuilder = new BodyBuilder();
            bodyBuilder.TextBody = body;
            
            bodyBuilder.Attachments.Add("Invoice0" +count+ ".pdf");
            email.Body = bodyBuilder.ToMessageBody();



            var smtp = new SmtpClient();
            smtp.Connect("smtp-mail.outlook.com", 587, SecureSocketOptions.StartTlsWhenAvailable);
            smtp.Authenticate("nclaudiu007@outlook.com", "Parola1234");
            smtp.Send(email);
            smtp.Disconnect(true);
        }
    }
}
