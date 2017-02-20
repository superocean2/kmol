using KMOL.Web.Mail;
using KMOL.Web.Models;
using Ninject.Modules;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KMOL.Web
{
    public class BindingConfig:NinjectModule
    {
        public override void Load()
        {
            Bind<IMailSender>().To<MailerGmail>();
            Bind<IProductService>().To<ProductService>();
        }
    }
}
