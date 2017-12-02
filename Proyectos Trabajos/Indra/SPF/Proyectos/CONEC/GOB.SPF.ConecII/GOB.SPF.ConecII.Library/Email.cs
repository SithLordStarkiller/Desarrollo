using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Mail;
using System.Configuration;
using System.IO;

namespace GOB.SPF.ConecII.Library
{
    /// <summary>
    /// 
    /// </summary>
    /// <remarks>
    /// 17/10/2017
    /// Horacio Torres
    /// Creado
    /// </remarks>
    public class Email
    {
        #region Properties 

        public string Subject { get; private set; }
        public string Body { get; private set; }
        public string To { get; private set; }
        public string From { get; private set; }
        public bool IsBodyHrml { get; private set; }

        #endregion

        #region Class Variables
        /// <summary>
        /// Base .NET MailMessage
        /// </summary>
        private MailMessage _message;

        #endregion


        #region Constructors

        public Email(string subject,
                     string body,
                     string to,
                     string from,
                     bool isBodyHtml
                    ) 

        {
            if (String.IsNullOrEmpty(from))
                throw new ArgumentException("Remitente no especificado.");

            if (String.IsNullOrEmpty(to))
                throw new ArgumentException("Destinatario no especificado.");


            Subject = subject;
            Body = body;
            To = to;
            From = from;
            IsBodyHrml = isBodyHtml;

            _message = new MailMessage();

            _message.Subject = Subject;
            _message.Body = Body;
            _message.IsBodyHtml = IsBodyHrml;
            To = to.Replace(',', '|');
            To = to.Replace(';', '|');
            string[] EmailRecipients = To.Split(new char[] { '|' });

            foreach (string Recipient in EmailRecipients)
            {
                if (Recipient != string.Empty)
                {
                    _message.To.Add(new MailAddress(Recipient.Trim()));
                }
            }

            _message.From = new MailAddress(From);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="subject"></param>
        /// <param name="body"></param>
        /// <param name="to"></param>
        /// <remarks>
        /// El From se toma del web.config AppSettings SMTPTUserName
        /// </remarks>
        public Email(string subject,
                     string body,
                     string to
                    ) : this(subject, body, to, GetFrom(), true)

        {          
          
        }

        /// <summary>
        /// Regresa el From del arcvivo de configuracionews
        /// </summary>
        /// <returns></returns>
        private static string GetFrom()
        {
            string from = null;
            var appsettings = ConfigurationManager.AppSettings["SMTPTUserName"];
            if (appsettings != null)
            {
                from = appsettings.To<string>();
            }
            
            return from ;
        }

        #endregion

        public void AddAttachment(Stream file, string fileName)
        {

            _message.Attachments.Add(new Attachment(file, fileName));
        }

        public void AddAttachment(string fileName)
        {
            _message.Attachments.Add(new Attachment(fileName));
        }

       
        /// <summary>
        /// Envia el email
        /// </summary>
        public void Send()
        {                       
            
            SmtpClient smtpClient = new SmtpClient();
            var userName = ConfigurationManager.AppSettings["SMTPTUserName"].To<string>();
            var password = ConfigurationManager.AppSettings["SMTPTPassword"].To<string>();
            smtpClient.Host = ConfigurationManager.AppSettings["SMTPServer"].To<string>();
            smtpClient.Port = ConfigurationManager.AppSettings["SMTPPort"].To<int>();
            smtpClient.UseDefaultCredentials = false;
            smtpClient.Credentials = new System.Net.NetworkCredential(userName, password);
            smtpClient.DeliveryMethod = SmtpDeliveryMethod.Network;
            smtpClient.Send(_message);
        }
        

    }
}

