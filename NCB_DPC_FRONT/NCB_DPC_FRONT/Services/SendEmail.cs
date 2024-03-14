using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using NCB_DPC_FRONT.Models;

namespace NCB_DPC_FRONT.Services
{
    public class SendEmail
    {

        public void sendEmail(Application application)
        {
            StringBuilder sb = new StringBuilder();
            string emailReceiveraddress = string.Empty;


            sb.AppendLine("Dear " + application.applicantTitle + " " + application.applicantName + ",");
            sb.AppendLine("<br><br><b>Digital Proficiency Course</b>");
            sb.AppendLine("<br><br>Application ID: " + application.ApplicationID);
            sb.AppendLine("<p>Batch Applied:" + application.batchApplied + " -- " + "</p>");
            sb.AppendLine("<p>Start Date of course: " + application.startDate + "</p>");
            sb.AppendLine("<p>End Date of course: " + application.endDate + "</p>");
            sb.AppendLine("<p>Location/Training centre: " + application.location + "</p>");
            sb.AppendLine("<p>Date and Time of Application: " + application.dateOfApplication + "</p>");

            try
            {
                using (SmtpClient SmtpServer = new SmtpClient(
                         "202.123.27.104",
                         25
                     )
                 )
                {
                    MailMessage mail = new MailMessage();
                    mail.IsBodyHtml = true;

                    mail.From = new MailAddress("gov_portal@goc.govmu.org");
                    //mail.To.Add(new MailAddress(System.Configuration.ConfigurationManager.AppSettings["PhotocontestMailReceiveAddress"]));

                    mail.To.Add(application.applicantEmail);
                    mail.Subject = "Application for Digital Proficiency Course";

                    mail.Body = sb.ToString();
                    SmtpServer.Send(mail);
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.InnerException.ToString());

            }
        }
    }



}
