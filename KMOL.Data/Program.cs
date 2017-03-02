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
using System.IO;
using Lucene.Net.Index;
using Lucene.Net.Store;
using Lucene.Net.Documents;

namespace KMOL.Data
{
    class Program
    {
        static List<Products> list = new List<Products>();
        static KMOLContext db = new KMOLContext(false);
        static KMOLContext dbHome = new KMOLContext(true);
        static List<LinkDownload> linkDownloads = new List<LinkDownload>();
        static List<LinkDownload> homeLinks = new List<LinkDownload>();
        static void Main(string[] args)
        {
            //#region main
            //Console.WriteLine($"Starting at {DateTime.Now.ToString()} ...");
            //Log.Debug($"Starting at {DateTime.Now.ToString()} ...", false);
            //object[] sites = {
            //    new Lazada(),
            //    new Tiki(),
            //    new Adayroi()
            //};
            //foreach (var site in sites)
            //{
            //    ISite iSite = (ISite)site;
            //    db.Websites.Add(new WebsiteInfo() { Name = iSite.SiteName, Url = iSite.SiteUrl });
            //    dbHome.Websites.Add(new WebsiteInfo() { Name = iSite.SiteName, Url = iSite.SiteUrl });
            //}
            //db.SaveChanges();
            //dbHome.SaveChanges();

            //foreach (var site in sites)
            //{
            //    try
            //    {
            //        GetDownloadLinks((ISite)site);
            //    }
            //    catch (Exception e)
            //    {
            //        Log.Err("Main run fail", e);
            //        Console.WriteLine("Fail!!!");
            //    }

            //}
            //Console.WriteLine($"Link downloads count: {linkDownloads.Count}");
            //Console.WriteLine($"Finished download links count at {DateTime.Now.ToString()} ...");
            //Log.Debug($"Links count: {linkDownloads.Count}", false);
            //Log.Debug($"Download home links... Count: {homeLinks.Count}", false);
            //SplitDownloadLinks(true);
            //SplitDownloadLinks(false);
            //#endregion

            //if index not working, will start it by hand
            Index("28-02-2017".ToDate());

            Console.Read();
        }
        private static async void SplitDownloadLinks(bool isHomeLinks)
        {
            int start = 0;
            int s = 5;
            int j = 1;
            var linkShouldDowns = isHomeLinks ? homeLinks : linkDownloads;
            while (linkShouldDowns.Count >= s)
            {
                List<LinkDownload> links = new List<LinkDownload>();
                links = linkShouldDowns.Skip(start).Take(s).ToList();
                linkShouldDowns.RemoveRange(start, s);
                Console.WriteLine($"Downloading links from: {s * (j - 1)} to {s * j} ...");
                await Download(links, isHomeLinks);
                j++;
            }
            //execute remain link <s
            await Download(linkShouldDowns, isHomeLinks);
            Console.WriteLine("Finished " + (isHomeLinks ? "home links" : "all links") + $" at {DateTime.Now.ToString()} ...");
            Log.Debug("Finished " + (isHomeLinks ? "home links" : "all links") + $" at {DateTime.Now.ToString()} ...", false);
            //check if all sites have enough products
            KMOLContext dbIsHome = new KMOLContext(isHomeLinks);
            bool shouldDelete = false;
            foreach (var site in dbIsHome.Websites)
            {
                if (dbIsHome.Products.Where(i => i.WebsiteId == site.WebsiteId).Count() < 1000)
                {
                    shouldDelete = true;
                    break;
                }
            }
            if (shouldDelete)
            {
                string path = isHomeLinks ? dbIsHome.DatabaseHomePath : dbIsHome.DatabaseAllPath;
                System.IO.File.Delete(path);
            }
            else
            {
                if (!isHomeLinks)
                {
                    Index(DateTime.Now);
                }

            }
        }
        private static void Index(DateTime usedDate)
        {
            //Begin index
            Console.WriteLine($"Index start at: {DateTime.Now.ToString()}");
            Log.Debug($"Index start at: {DateTime.Now.ToString()}", false);
            KMOLContext context = new KMOLContext(false,usedDate);
            string indexPath = new DirectoryInfo(Environment.CurrentDirectory).Parent.FullName + "\\SearchDatas" + "\\data_" + usedDate.ToString("dd-MM-yyyy") + ".ikmol";
            IndexWriter writer = new IndexWriter(FSDirectory.Open(indexPath), new Lucene.Net.Analysis.Standard.StandardAnalyzer(Lucene.Net.Util.Version.LUCENE_30), true, IndexWriter.MaxFieldLength.UNLIMITED);
            int j = 0;
            foreach (var product in context.Products)
            {
                Field id = new Field("id", product.ProductId.ToString(), Field.Store.YES, Field.Index.NO);
                Field name = new Field("sname", product.Name.ToLower(), Field.Store.YES, Field.Index.ANALYZED);
                Field usignName = new Field("uname", product.Name.ToLower().ToUnsignUnicode(), Field.Store.YES, Field.Index.ANALYZED);
                Document doc = new Document();
                doc.Add(id);
                doc.Add(name);
                doc.Add(usignName);
                writer.AddDocument(doc);
                j++;
                if (j % 1000 == 0) Console.WriteLine($"Index item: {j} ...");
            }
            writer.Optimize();
            writer.Dispose();

            Console.WriteLine($"Index counts: {j}");
            Log.Debug($"Index counts: {j}", true);
            Console.WriteLine($"Index end at: {DateTime.Now.ToString()}");
            Log.Debug($"Index end at: {DateTime.Now.ToString()}", false);
        }
        private static async Task Download(List<LinkDownload> links, bool isHomeLinks)
        {
            for (int i = 1; i <= links.Count; i++)
            {
                links[i - 1].Url = links[i - 1].Url.Replace("{i}", i.ToString());
            }
            var tasks = (from link in links select Utility.GetDataAsync(link, isHomeLinks)).ToList();
            while (tasks.Count > 0)
            {
                try
                {
                    var finishedTask = await Task.WhenAny(tasks);
                    if (finishedTask != null)
                    {
                        tasks.Remove(finishedTask);
                        var response = await finishedTask;
                        SaveData(response.Response, response.Regex, response.WebsiteId, response.Url, response.IsHomeLinks);
                        Console.WriteLine($"Finished download url: {response.Url}");
                    }
                    else
                    {
                        break;
                    }
                }
                catch (Exception e)
                {
                    Log.Err("Download fail", e);
                    Console.WriteLine($"Download fail");
                    break;
                }

            }
        }
        private static void GetDownloadLinks(ISite site)
        {
            Console.WriteLine($"Adding link download of site: {site.SiteName} ...");
            int websiteId = db.Websites.Where(c => c.Name == site.SiteName).FirstOrDefault().WebsiteId;
            int j = 0;
            int w = 200;
            foreach (var link in site.LinkInfos)
            {
                try
                {
                    Console.WriteLine($"Adding link download: {link.Url} ...");
                    string regex = link.Regex;
                    var pageCount = Utility.GetPageCount(link.Url.Replace("{i}", "1"), link.RegexPageCount, link.Pagesize,link.IsHasPageCount);
                    Console.WriteLine($"Page count: {pageCount}");
                    if (pageCount > 0)
                    {
                        if (link.Pagesize >= w) homeLinks.Add(new LinkDownload() { Url = link.Url.Replace("{i}", "1"), Regex = regex, WebsiteId = websiteId });
                        else
                        {
                            var pages = w / link.Pagesize + 1;
                            for (int i = 1; i <= pages; i++)
                            {
                                homeLinks.Add(new LinkDownload() { Url = link.Url.Replace("{i}", i.ToString()), Regex = regex, WebsiteId = websiteId });
                            }
                        }
                    }
                    for (int i = 1; i <= pageCount; i++)
                    {
                        linkDownloads.Add(new LinkDownload() { Url = link.Url.Replace("{i}", i.ToString()), Regex = regex, WebsiteId = websiteId });
                        j++;
                    }
                }
                catch (Exception ex)
                {
                    Log.Debug($"Fail download pagecount for link {link.Url}", false);
                    Log.Err($"Fail download pagecount for link {link.Url}", ex);
                }


            }
            Console.WriteLine($"Link downloads for site: {site.SiteName} count: {j}");
        }


        private static void SaveData(string response, string regex, int websiteId, string url, bool isHomeLinks)
        {
            try
            {
                var products = new List<ProductInfo>();
                if (Regex.IsMatch(response, regex))
                {
                    foreach (Match match in Regex.Matches(response, regex))
                    {
                        decimal price, oldprice, percentsale = 0;
                        decimal.TryParse(Regex.Replace(match.Groups["price"].Value.Trim(), "[.,]", string.Empty), out price);
                        decimal.TryParse(Regex.Replace(match.Groups["oldprice"].Value.Trim(), "[.,]", string.Empty), out oldprice);
                        //decimal.TryParse(Regex.Replace(match.Groups["discount"].Value.Trim(), "[.,-]", string.Empty), out percentsale);
                        if (oldprice>price)
                        {
                            percentsale = Math.Round(100 - (price * 100 / oldprice),0);
                        }

                        products.Add(new ProductInfo()
                        {
                            Name = match.Groups["title"].Value,
                            Url = match.Groups["url"].Value,
                            ImageUrl = match.Groups["imageurl"].Value,
                            Price = price,
                            OldPrice = oldprice,
                            PercentSale = percentsale,
                            WebsiteId = websiteId
                        });
                    }
                    StringBuilder sqlCommand = new StringBuilder();
                    sqlCommand.Append($"INSERT INTO Products (Name,Url,ImageUrl,Price,OldPrice,PercentSale,WebsiteId) VALUES ");

                    foreach (var product in products)
                    {
                        sqlCommand.Append($"('{product.Name}','{product.Url}','{product.ImageUrl}',{product.Price},{product.OldPrice},{product.PercentSale},{product.WebsiteId}),");
                    }
                    if (isHomeLinks)
                        dbHome.ExecuteCommandNonQuery(sqlCommand.ToString().TrimEnd(','));
                    else
                        db.ExecuteCommandNonQuery(sqlCommand.ToString().TrimEnd(','));
                    Console.WriteLine($"Saved data url: {url}");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error in save file");
                Log.Err("Error in save file", ex);
            }

        }
    }
}
