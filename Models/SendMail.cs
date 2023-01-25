using Microsoft.AspNetCore.Identity.UI.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Threading.Tasks;

namespace UserRegistrationDotNetCore.Models
{
    public class SendMail : IEmailSender
    {
        public async Task SendEmailAsync(string email, string subject, string htmlMessage)
        {
            using (MailMessage mailMessage = new MailMessage()) {
                mailMessage.To.Add(email);
                //mailMessage.To.Add(new MailAddress(email));
                mailMessage.From = new MailAddress("loancompanyproject2021@gmail.com");
                mailMessage.Subject = subject;
                mailMessage.Body = email + htmlMessage;
                mailMessage.IsBodyHtml =  true;
                SmtpClient smtp = new SmtpClient("smtp.gmail.com", 465);
                smtp.UseDefaultCredentials = true;
                smtp.EnableSsl = true;
                smtp.Credentials = new System.Net.NetworkCredential("loancompanyproject2021@gmail.com", "");
                await smtp.SendMailAsync(mailMessage);
            }
        }
    }
}
