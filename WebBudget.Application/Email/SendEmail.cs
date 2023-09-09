using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace WebBudget.Application.Email
{
	public class SendEmail
	{

		private SmtpClient _smtp;
		private MailMessage _mail;

		private string _hostSmtp;
		private bool _enableSsl;
		private int _port;
		private string _senderEmail;
		private string _senderEmailPassword;
		private string _senderName;

		public SendEmail(EmailParams emailParams)
		{
			_hostSmtp = emailParams.HostSmtp;
			_enableSsl = emailParams.EnableSsl;
			_port = emailParams.Port;
			_senderEmail = emailParams.SenderEmail;
			_senderEmailPassword = emailParams.SenderEmailPassword;
			_senderName = emailParams.SenderName;
		}

		public async Task Send(string subject, string body, string to)
		{
			_mail = new MailMessage();
			_mail.From = new MailAddress(_senderEmail, _senderName);
			_mail.To.Add(new MailAddress(to));
			_mail.IsBodyHtml = true;
			_mail.Subject = subject;
			_mail.BodyEncoding = Encoding.UTF8;
			_mail.SubjectEncoding = Encoding.UTF8;
			_mail.Body = body;

			_smtp = new SmtpClient
			{
				Host = _hostSmtp,
				EnableSsl = _enableSsl,
				Port = _port,
				DeliveryMethod = SmtpDeliveryMethod.Network,
				UseDefaultCredentials = false,
				Credentials = new NetworkCredential(_senderEmail, _senderEmailPassword)
			};

			_smtp.SendCompleted += OnSendCompleted;

			await _smtp.SendMailAsync(_mail);
		}

		private void OnSendCompleted(object sender, System.ComponentModel.AsyncCompletedEventArgs e)
		{
			_smtp.Dispose();
			_mail.Dispose();
		}
	}
}
