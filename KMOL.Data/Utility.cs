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
        public static string GetData(string url)
        {
            string response = string.Empty;
            using (var httpClient = new HttpClient())
            {
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
    }
}
