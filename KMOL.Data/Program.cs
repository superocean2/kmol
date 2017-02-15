using KMOL.Data.Data;
using KMOL.Data.Mail;
using KMOL.Data.Sites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace KMOL.Data
{
    class Program
    {
        static List<Products> list = new List<Products>();
        static KMOLContext db = new KMOLContext();
        static List<LinkDownload> linkDownloads = new List<LinkDownload>();
        static void Main(string[] args)
        {
            Console.WriteLine($"Starting at {DateTime.Now.ToString()} ...");
            object[] sites = {
                //new Lazada(),
                new Tiki(),
                //new Adayroi()
            };
            foreach (var site in sites)
            {
                ISite iSite = (ISite)site;
                db.Websites.Add(new WebsiteInfo() { Name = iSite.SiteName, Url = iSite.SiteUrl });
            }
            db.SaveChanges();

            foreach (var site in sites)
            {
                try
                {
                    GetDownloadLinks((ISite)site);
                }
                catch (Exception e)
                {
                    Log.Err("Main run fail", e);
                    Console.WriteLine("Fail!!!");
                }

            }
            Console.WriteLine($"Link downloads count: {linkDownloads.Count}");
            Console.WriteLine($"Finished download links count at {DateTime.Now.ToString()} ...");
            SplitDownloadLinks();



            //var st = JsonConvert.SerializeObject(list);
            //string filePath = Environment.CurrentDirectory + "\\file.kmol";
            //try
            //{
            //    System.IO.File.WriteAllText(filePath, st);
            //    EmailSender.GetMailer().SendToMe("Well done!");
            //}
            //catch (Exception ex)
            //{
            //    Log.Err("Error write file", ex);
            //}

            //EmailSender.GetMailer().SendToMe(Log.ReadAllDebug());
            //EmailSender.GetMailer().SendToMe(Log.ReadAllError());
            //Console.WriteLine("Well done!");

            Console.WriteLine($"Finished at {DateTime.Now.ToString()} ...");
            Console.Read();
        }

        private static async void SplitDownloadLinks()
        {
            int start = 0;
            int s = 20;
            int j = 1;
            while (linkDownloads.Count >= s)
            {
                List<LinkDownload> links = new List<LinkDownload>();
                links = linkDownloads.Skip(start).Take(s).ToList();
                linkDownloads.RemoveRange(start, s);
                Console.WriteLine($"Downloading links from: {s*(j-1)} to {s*j} ...");
                await Download(links);
                j++;
            }
        }
        private static async Task Download(List<LinkDownload> links)
        {
            for (int i = 1; i <= links.Count; i++)
            {
               links[i-1].Url= links[i-1].Url.Replace("{i}", i.ToString());
            }
            var tasks = (from link in links select Utility.GetDataAsync(link)).ToList();
            while (tasks.Count > 0)
            {
                try
                {
                    var finishedTask = await Task.WhenAny(tasks);
                    tasks.Remove(finishedTask);
                    var response = await finishedTask;
                    SaveData(response.Response, response.Regex, response.WebsiteId, response.Url);
                    Console.WriteLine($"Finished download url: {response.Url}");
                }
                catch (Exception e)
                {
                    Log.Err("Download fail", e);
                    Console.WriteLine($"Download fail");
                }

            }
        }
        private static void GetDownloadLinks(ISite site)
        {
            Console.WriteLine($"Adding link download of site: {site.SiteName} ...");
            int websiteId = db.Websites.Where(c => c.Name == site.SiteName).FirstOrDefault().WebsiteId;
            int j = 0;
            foreach (var link in site.LinkInfos)
            {
                Console.WriteLine($"Adding link download: {link.Url} ...");
                string regex = link.Regex;
                var pageCount = Utility.GetPageCount(link.Url.Replace("{i}", "1"), link.RegexPageCount,link.Pagesize);
                Console.WriteLine($"Page count: {pageCount}");
                for (int i = 1; i <= pageCount; i++)
                {
                    linkDownloads.Add(new LinkDownload() { Url = link.Url.Replace("{i}", i.ToString()),Regex=regex,WebsiteId=websiteId });
                    j++;
                }
                
            }
            Console.WriteLine($"Link downloads for site: {site.SiteName} count: {j}");
        }


        private static void SaveData(string response, string regex, int websiteId, string url)
        {
            try
            {
                foreach (Match match in Regex.Matches(response, regex))
                {
                    decimal price, oldprice, percentsale = 0;
                    decimal.TryParse(Regex.Replace(match.Groups["price"].Value.Trim(), "[.,]", string.Empty), out price);
                    decimal.TryParse(Regex.Replace(match.Groups["oldprice"].Value.Trim(), "[.,]", string.Empty), out oldprice);
                    decimal.TryParse(Regex.Replace(match.Groups["discount"].Value.Trim(), "[.,-]", string.Empty), out percentsale);

                    db.Products.Add(new ProductInfo()
                    {
                        Name = match.Groups["title"].Value,
                        Url = match.Groups["url"].Value,
                        ImageUrl = match.Groups["imageurl"].Value,
                        Price = price,
                        OldPrice = oldprice,
                        PercentSale = percentsale,
                        WebsiteId = websiteId
                    });
                    //string sqlCommand = $"INSERT INTO Products (Name,Url,ImageUrl,Price,OldPrice,PercentSale,WebsiteId) VALUES ({product.Name},{product.Url},{product.ImageUrl},{product.Price},{product.OldPrice},{product.PercentSale},{product.WebsiteId})";
                    //db.ExecuteCommandNonQuery(sqlCommand);
                }
                db.SaveChanges();
                Console.WriteLine($"Saved data url: {url}");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error in save file");
                Log.Err("Error in save file", ex);
            }
            
            //try
            //{
            //    await db.SaveChangesAsync();
            //    Console.WriteLine($"Saved data url: {url}");
            //}
            //catch (Exception e)
            //{
            //    Log.Err($"Save fail url: {url}", e);
            //    Console.WriteLine($"Save fail data url: {url}");
            //}

        }
    }
}
