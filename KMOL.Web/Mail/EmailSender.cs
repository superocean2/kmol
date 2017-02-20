using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KMOL.Web.Mail
{
    public class EmailSender
    {
        public static Mailer GetMailer()
        {
            GetNinject o = new GetNinject();
            var sender = o.Get<IMailSender>();
            return new Mailer(sender);
        }
    }
}
