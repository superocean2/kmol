using KMOL.Data.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace KMOL.Data
{
    public static class Utility
    {
        public static async Task<TaskDownloadInfo> GetDataAsync(LinkDownload link,bool isHomeLinks)
        {
            using (var httpClient = new HttpClient())
            {
                httpClient.Timeout = TimeSpan.FromMilliseconds(8000);
                var request = new HttpRequestMessage()
                {
                    RequestUri = new Uri(link.Url),
                    
                };
                try
                {
                    var task = await httpClient.SendAsync(request);
                    if (task.IsSuccessStatusCode)
                    {
                        var response = await task.Content.ReadAsStringAsync();
                        Console.WriteLine($"Process downloading url: {link.Url}");
                        return new TaskDownloadInfo() { Response = response.TripHtml(), Url = link.Url, Regex = link.Regex, WebsiteId = link.WebsiteId,IsHomeLinks=isHomeLinks};
                    }
                }
                catch (Exception ex)
                {
                    Log.Err($"Fail download asyn link: {link.Url}", ex);
                }
            }
            return new TaskDownloadInfo();
        }
        public static string GetData(string url)
        {
            string response = string.Empty;
            using (var httpClient = new HttpClient())
            {
                httpClient.Timeout = TimeSpan.FromMilliseconds(8000);
                var request = new HttpRequestMessage()
                {
                    RequestUri = new Uri(url)
                };
                var task = httpClient.SendAsync(request)
                     .ContinueWith(async (c)  => {
                         response = await c.Result.Content.ReadAsStringAsync();
                     });
                task.Wait();
                return response.TripHtml();
                
            }
        }
        public static string TripHtml(this string html)
        {
            Regex regex = new Regex(@"[\s]+[<]");
            string s = regex.Replace(html, "<");
            Regex regex2 = new Regex(@"[\n]");
            return regex2.Replace(s, string.Empty);
        }

        /// <summary>
        /// 
        /// <param name="url"></param>
        /// <param name="regexPattern"></param>
        /// <param name="pagesize"></param>
        /// <returns></returns>
        /// </summary>
        public static int GetPageCount(string url, string regexPattern, int pagesize,bool isHasPageCount)
        {
            int pagecount = 0;
            string response = Utility.GetData(url);
            Regex regex = new Regex(regexPattern);
            if (regex.IsMatch(response))
            {
                int.TryParse(regex.Match(response).Groups["pagecount"].Value.Trim(), out pagecount);
            }
            if (isHasPageCount) return pagecount;
            else
            {
                if (pagecount < pagesize) pagecount = 1;
                else
                {
                    pagecount = pagecount / pagesize;
                    if (pagecount % pagesize != 0) pagecount++;
                }
                return pagecount;
            }
        }
    }
}
