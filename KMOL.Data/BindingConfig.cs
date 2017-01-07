using KMOL.Data.Mail;
using Ninject.Modules;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KMOL.Data
{
    public class BindingConfig:NinjectModule
    {
        public override void Load()
        {
            Bind<IMailSender>().To<MailerGmail>();
        }
    }
}
