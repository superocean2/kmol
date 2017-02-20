using KMOL.Data.Data;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace TestConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            KMOLContext db = new KMOLContext();
            var products = db.Products.ToList();
            foreach (var product in products)
            {
                Console.WriteLine(product.Name);
            }
            Console.Read();
        }

        static async Task TestTime()
        {
            int[] times = { 1000, 5000, 9000, 7000, 4000, 3000, 6000};
            var tasks = (from time in times select Process(time)).ToList();
            while (tasks.Count>0)
            {
                var completeTask = await Task.WhenAny(tasks);
                tasks.Remove(completeTask);
                var time = await completeTask;
                Console.WriteLine($"Task time finished: {time}");
            }
        }
        static async Task<int> Process(int time)
        {
            Console.WriteLine($"Process task time: {time}");
            await Task.Delay(time);
            return time;
        }
       static async Task AccessTheWebAsync()
        {
            HttpClient client = new HttpClient();

            // Make a list of web addresses.  
            List<string> urlList = SetUpURLList();

            // ***Create a query that, when executed, returns a collection of tasks.  
            IEnumerable<Task<int>> downloadTasksQuery =
                from url in urlList select ProcessURL(url, client);

            // ***Use ToList to execute the query and start the tasks.   
            List<Task<int>> downloadTasks = downloadTasksQuery.ToList();

            // ***Add a loop to process the tasks one at a time until none remain.  
            while (downloadTasks.Count > 0)
            {
                // Identify the first task that completes.  
                Task<int> firstFinishedTask = await Task.WhenAny(downloadTasks);

                // ***Remove the selected task from the list so that you don't  
                // process it more than once.  
                downloadTasks.Remove(firstFinishedTask);

                // Await the completed task.  
                int length = await firstFinishedTask;
                Console.WriteLine(String.Format("\r\nLength of the download:  {0}", length));
            }
        }

        private static List<string> SetUpURLList()
        {
            List<string> urls = new List<string>
            {
                "http://msdn.microsoft.com",
                "http://msdn.microsoft.com/library/windows/apps/br211380.aspx",
                "http://msdn.microsoft.com/en-us/library/hh290136.aspx",
                "http://msdn.microsoft.com/en-us/library/dd470362.aspx",
                "http://msdn.microsoft.com/en-us/library/aa578028.aspx",
                "http://msdn.microsoft.com/en-us/library/ms404677.aspx",
                "http://msdn.microsoft.com/en-us/library/ff730837.aspx"
            };
            return urls;
        }

        static async Task<int> ProcessURL(string url, HttpClient client)
        {
            Console.WriteLine($"Process url: {url}");
            // GetAsync returns a Task<HttpResponseMessage>.   
            HttpResponseMessage response = await client.GetAsync(url);

            // Retrieve the website contents from the HttpResponseMessage.  
            byte[] urlContents = await response.Content.ReadAsByteArrayAsync();

            return urlContents.Length;
        }
    }
}
