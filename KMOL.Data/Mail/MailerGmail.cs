using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.IO;
using KMOL.Data.Data;
using System.Net.Mail;
using System.Net;

namespace KMOL.Data.Mail
{
    public class MailerGmail : IMailSender
    {
        string folder = Environment.CurrentDirectory + "\\Config\\";
        MailConfigInfo config = null;
        public MailerGmail()
        {
            string path = folder + "gmail.json";
            if (File.Exists(path))
            {
                config = JsonConvert.DeserializeObject<MailConfigInfo>(File.ReadAllText(path));
            }
            else
            {
                Directory.CreateDirectory(folder);
                File.WriteAllText(path, JsonConvert.SerializeObject(new MailConfigInfo()));
            }
        }
        public bool Send(MailMessage mailMessage)
        {
            if (config != null)
            {
                try
                {
                    SmtpClient smtp = new SmtpClient();
                    smtp.Host = "smtp.gmail.com";
                    smtp.EnableSsl = true;
                    NetworkCredential NetworkCred = new NetworkCredential(config.Email, config.Password);
                    smtp.UseDefaultCredentials = true;
                    smtp.Credentials = NetworkCred;
                    smtp.Port = 587;
                    smtp.SendMailAsync(mailMessage).Wait();
                    return true;
                }
                catch (Exception ex)
                {
                    Log.Err($"send mail to:{mailMessage.To[0].Address} error", ex);
                    return false;
                }
            }
            else return false;
        }
        public bool Send(string from, string recipients, string subject, string body)
        {
            if (config != null)
            {
                try
                {
                    SmtpClient smtp = new SmtpClient();
                    smtp.Host = "smtp.gmail.com";
                    smtp.EnableSsl = true;
                    NetworkCredential NetworkCred = new NetworkCredential(config.Email, config.Password);
                    smtp.UseDefaultCredentials = true;
                    smtp.Credentials = NetworkCred;
                    smtp.Port = 587;
                    smtp.SendMailAsync(from,recipients,subject,body).Wait();
                    return true;
                }
                catch (Exception ex)
                {
                    Log.Err($"send mail to:{recipients} error", ex);
                    return false;
                }
            }
            else return false;
        }
    }
}
