using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Mail;
namespace TrackerMVCBusinessLayer.TrackerMVCBusinessManger.TicketSystem
{
    public class EmailHelper
    {
        public bool SendEMail(int portid, string DomanID, string strFrom, string Password, string strToIDs, string strSubject, string strBodyText, string strCcIDs, string strBccIDs, string strAttachement)
        {
            string strSQL = "";
            bool isMessageSent = false;
            MailMessage MailMessage = new MailMessage();

            MailAddress mailAddress = new MailAddress(strFrom);

            MailMessage.From = mailAddress;

            MailMessage.To.Add((strToIDs));
            //strCcIDs = "raj@digidisruptors.com";
            //strBccIDs = "datta@digidisruptors.com";
            if (strCcIDs != "")
            {
                MailMessage.CC.Add(strCcIDs);
            }
            if (strBccIDs != "")
            {
                MailMessage.Bcc.Add(strBccIDs);
            }

            MailMessage.Subject = strSubject;
            MailMessage.IsBodyHtml = true;
            MailMessage.Body = strBodyText;


            SmtpClient smtpClient = new SmtpClient(DomanID, portid);
            System.Net.NetworkCredential credentials = new System.Net.NetworkCredential(strFrom, Password);
            smtpClient.EnableSsl = true;
            smtpClient.UseDefaultCredentials = true;

            smtpClient.Credentials = credentials;

            if (strAttachement != "")
            {

                // adding multiple attachment
                string[] values = strAttachement.Split(',');

                for (int i = 1; i < values.Length; i++)
                {

                    values[i] = values[i].Trim();
                    string strAttachement1 = values[i];
                    MailMessage.Attachments.Add(new Attachment(strAttachement1));
                    strAttachement1 = "";

                }

            }

            try
            {
                smtpClient.Send(MailMessage);
                isMessageSent = true;
            }
            catch (Exception ex)
            {
                isMessageSent = false;

            }
            return isMessageSent;

        }
    }
}
