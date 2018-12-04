using System;
using System.Net.Mail;

namespace Calmo.Core.Net.Mail
{
	/// <summary>
	/// Send mails using the default .NET Smtp client, all the network settings must be valid on web.config or app.config
	/// </summary>
    public class MailSender
    {
        private readonly SmtpClient _smtpClient;

        public MailSender()
        {
            this._smtpClient = new SmtpClient();
        }

		/// <summary>
		/// Send e-mail
		/// </summary>
		/// <param name="toEmailAddress">Recipient e-mail</param>
		/// <param name="toDisplayName">Recipient display name</param>
		/// <param name="subject">Subject</param>
		/// <param name="body">Body</param>
		/// <param name="ccEmailAddress">E-mails to send a copy to</param>
		/// <param name="ccDisplayName">Copy To: Display Name</param>
		/// <param name="attachment">Full path of attachment file to send</param>
		public void Send(string toEmailAddress, string toDisplayName, string subject, string body, string ccEmailAddress = null, string ccDisplayName = null, string attachment = null)
        {
            if (String.IsNullOrWhiteSpace(toEmailAddress))
                throw new ArgumentNullException("toEmailAddress");

            if (String.IsNullOrWhiteSpace(toDisplayName))
                throw new ArgumentNullException("toDisplayName");

            if (String.IsNullOrWhiteSpace(subject))
                throw new ArgumentNullException("subject");

            if (String.IsNullOrWhiteSpace(body))
                throw new ArgumentNullException("body");

            var mail = new MailMessage { IsBodyHtml = true };

            mail.To.Add(new MailAddress(toEmailAddress, toDisplayName));

            if (!String.IsNullOrWhiteSpace(ccEmailAddress) && !String.IsNullOrWhiteSpace(ccDisplayName))
            {
                mail.CC.Add(new MailAddress(ccEmailAddress, ccDisplayName));
            }

            mail.Subject = subject;
            mail.Body = body;

            if (!String.IsNullOrWhiteSpace(attachment))
            {
                mail.Attachments.Add(new Attachment(attachment));
            }

            this._smtpClient.Send(mail);
        }
    }
}