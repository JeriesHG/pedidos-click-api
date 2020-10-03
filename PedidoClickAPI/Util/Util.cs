using PedidoClickAPI.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PedidoClickAPI.Util
{
    public class Util
    {
        public static void SendEmail(string subject, string emailTo, string emailCC, string content)
        {
            //string smtpServer = "smtp-mail.outlook.com";
            string smtpServer = "smtp.office365.com";

            //Send teh High priority Email  
            EmailManager mailMan = new EmailManager(smtpServer);

            EmailSendConfigure myConfig = new EmailSendConfigure();
            // replace with your email userName  
            myConfig.ClientCredentialUserName = "pedidoclick@pedidoclick.com";
            // replace with your email account password
            myConfig.ClientCredentialPassword = "Javier3131";
            myConfig.TOs = new string[] { "javierl.1994@gmail.com" };
            //myConfig.TOs = new string[] { emailTo };
            myConfig.CCs = new string[] { };
            //myConfig.CCs = new string[] { emailCC };
            myConfig.From = "pedidoclick@pedidoclick.com";
            myConfig.FromDisplayName = "Pedido Click";
            myConfig.Priority = System.Net.Mail.MailPriority.Normal;
            //myConfig.Subject = "WebSite was down - please investigate";
            myConfig.Subject = subject;

            EmailContent myContent = new EmailContent();
            //myContent.Content = "The following URLs were down - 1. Foo, 2. bar";
            myContent.Content = content;

            mailMan.SendMail(myConfig, myContent);
        }
    }
}