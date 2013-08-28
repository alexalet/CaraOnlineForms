using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.Mail;

namespace Utility
{
    public class Email
    {
        /// <summary>
        /// Send an email message.
        /// Throws InvalidOperationException on any error.
        /// </summary>
        /// <param name="subject"></param>
        /// <param name="body"></param>
        /// <param name="from"></param>
        /// <param name="fromName"></param>
        /// <param name="recipients"></param>
        /// <param name="isHTML"></param>
        public static void SendAnEmail(string subject, string body, string from, string fromName, IList<string> recipients, bool isHTML)
        {
            string emailServer = EmailSettings.Default.emailServer; //config driven
            int port = EmailSettings.Default.emailServerPort;
            string userName = EmailSettings.Default.userName;
            string password = EmailSettings.Default.password;

            try
            {
                MailMessage MyMessage = new MailMessage();
                MyMessage.From = new MailAddress(from, fromName);
                foreach (string to in recipients) { if (!string.IsNullOrEmpty(to)) { MyMessage.To.Add(to); } }
                SmtpClient smtp = new SmtpClient(emailServer, port);
                smtp.Credentials = new System.Net.NetworkCredential(userName, password);
                MyMessage.Subject = subject;
                MyMessage.Body = body;
                MyMessage.IsBodyHtml = isHTML;

                smtp.Send(MyMessage);
            }
            catch (Exception ex)
            {
                //the error must be logged by the consumer
                string error = ex.Message;
                throw new InvalidOperationException(error, ex.InnerException);
            }
        }
    }
}
