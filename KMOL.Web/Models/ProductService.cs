using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using KMOL.Web.Data;
using System.IO;
using Lucene.Net.Index;
using Lucene.Net.Store;
using Lucene.Net.Search;
using Lucene.Net.Documents;
using KMOL.Web.ViewModels;

namespace KMOL.Web.Models
{
    public class ProductService
    {
        KMOLContextFactory factoryContext = new KMOLContextFactory();
        public ProductInfo GetProductById(bool isHomeLinks, int id, DateTime usedDate)
        {
            KMOLContext db = factoryContext.Create(isHomeLinks, usedDate);
            return db.Products.Find(id);
        }
        public ProductViewModel GetProducts(int webid, bool isHomeLinks, int skip, int take)
        {
            DateTime realUsedDate = new DateTime();
            KMOLContext db = factoryContext.Create(isHomeLinks, DateTime.Now, ref realUsedDate);

            return new ProductViewModel()
            {
                date = realUsedDate.ToString("dd-MM-yyyy"),
                products = db.Products.Where(c => c.WebsiteId == webid).OrderByDescending(i => i.PercentSale).Skip(skip).Take(take)
            };
        }
        public IEnumerable<WebsiteInfo> GetWebsites()
        {
            KMOLContext db = factoryContext.Create(true, DateTime.Now);
            return db.Websites;
        }
        public WebsiteInfo GetWebsiteByProduct(ProductInfo product)
        {
            KMOLContext db = factoryContext.Create(true, DateTime.Now);
            return db.Websites.Where(i => i.WebsiteId == product.WebsiteId).FirstOrDefault();
        }
        public ProductViewModel Search(string q, int maxResult)
        {
            DateTime realUsedDate = new DateTime();
            List<ProductInfo> l = new List<ProductInfo>();
            KMOLContext db = factoryContext.Create(false, DateTime.Now, ref realUsedDate);
            string path = GetIndexPathString(realUsedDate);
            if (File.Exists(path))
            {
                IndexReader reader = IndexReader.Open(FSDirectory.Open(path), true);
                IndexSearcher searcher = new IndexSearcher(reader);
                Query query = new TermQuery(new Term("n", q));
                TopScoreDocCollector collector = TopScoreDocCollector.Create(maxResult, true);
                searcher.Search(query, collector);
                ScoreDoc[] hits = collector.TopDocs().ScoreDocs;
                if (hits.Length > 0)
                {
                    foreach (var hit in hits)
                    {
                        Document doc = searcher.Doc(hit.Doc);
                        int id = 0;
                        int.TryParse(doc.Get("id"), out id);
                        ProductInfo product = db.Products.Find(id);
                        if (product != null) l.Add(product);
                    }
                }
            }
            return new ProductViewModel()
            {
                date = realUsedDate.ToString("dd-MM-yyyy"),
                products = l
            };
        }
        private static string GetIndexPathString(DateTime currentDate)
        {
            return new DirectoryInfo(HttpRuntime.AppDomainAppPath).Parent.FullName + "\\KMOL.Data\\SearchDatas" + "\\data_" + currentDate.ToString("dd-MM-yyyy") + ".index";
        }
    }
}