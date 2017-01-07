using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KMOL.Data.Mail
{
    public class Mailer
    {
        IMailSender _sender;
        public Mailer(IMailSender sender)
        {
            _sender = sender;
        }
        public bool Send(System.Net.Mail.MailMessage mailMessage)
        {
           return _sender.Send(mailMessage);
        }
        public bool Send(string from, string recipients, string subject, string body)
        {
           return _sender.Send(from, recipients, subject,body);
        }
    }
}
