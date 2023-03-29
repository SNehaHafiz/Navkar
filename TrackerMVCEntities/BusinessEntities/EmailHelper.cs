using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Security;
using Microsoft.VisualBasic;
using System.Net;
using System.Net.Mail;
//using System.Net.Mail.MailMessage;
using System.Net.Mime;
using System.Threading;
using System.ComponentModel;
using System.Data;

namespace TrackerMVCEntities.BusinessEntities
{
    public class EmailHelper
    {
        public bool SendEMail(int portid, string DomanID,string strFrom, string Password, string strToIDs,string strSubject, string strBodyText, string strCcIDs, string strBccIDs, string strAttachement)
        {
            bool isMessageSent = false;
            MailMessage MailMessage = new MailMessage();

            MailAddress mailAddress = new MailAddress(strFrom);

            MailMessage.From = mailAddress;

            MailMessage.To.Add((strToIDs));
            if(strCcIDs != "")
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
            smtpClient.UseDefaultCredentials = false;
            smtpClient.Credentials = credentials;

            if (strAttachement != "") { MailMessage.Attachments.Add(new Attachment(strAttachement)); }
          
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
