using KMOL.Data.Data;
using KMOL.Data.Mail;
using KMOL.Data.Sites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace KMOL.Data
{
    class Program
    {
        static void Main(string[] args)
        {
            object[] sites = {
                new Lazada()
            };

            foreach (var s in sites)
            {
                Run((ISite)s);
            }
            Console.Read();
        }

        private static void Run(ISite site)
        {
            var website = new WebsiteInfo()
            {

            };
            foreach (var link in site.LinkInfos)
            {
                var pageCount = link.PageCount.Invoke();
                for (int i = 1; i <= pageCount; i++)
                {
                    var url = link.Url.Replace("{i}", i.ToString());
                    string response = Utility.GetData(url);
                    foreach (var match in Regex.Matches(response, link.Regex))
                    {
                        
                    }
                }
            }
            
        }
    }
}
