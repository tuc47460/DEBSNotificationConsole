using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace DEBSNotificationConsole
{
    class Email
    {
        public static bool SendEmail(string fromEmail, string displayEmail, string subject, string emailbody, string sendto)
        {
            try
            {
                //local var
                MailMessage mailMessage = new MailMessage();
                MailAddress fromAddress = new MailAddress(fromEmail, displayEmail);
                //SmtpClient smtpClient = new SmtpClient("localhost", 25);
                SmtpClient smtpClient = new SmtpClient("smtp.temple.edu", 25);



                string emailSubject = subject;
                string emailBody = emailbody;
                mailMessage.To.Add(sendto);
                mailMessage.From = fromAddress;
                mailMessage.Subject = emailSubject;
                mailMessage.Body = emailBody;
                mailMessage.IsBodyHtml = true;
                smtpClient.Send(mailMessage);
                return true;
            }//end try
            catch (Exception ex)
            {
                return false;
            }//end Catch
        }//end SendEmail
    }//end Email Class
}
