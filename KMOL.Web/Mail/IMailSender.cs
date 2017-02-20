using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KMOL.Web.Mail
{
    public interface IMailSender
    {
        bool Send(System.Net.Mail.MailMessage mailMessage);
        bool Send(string from, string recipients, string subject, string body);
    }
}
