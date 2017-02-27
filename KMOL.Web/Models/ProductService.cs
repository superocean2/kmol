using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using KMOL.Web.Data;

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
        public IEnumerable<ProductInfo> GetProducts(int webid, bool isHomeLinks, int skip, int take)
        {
            KMOLContext db = factoryContext.Create(isHomeLinks, DateTime.Now);

            return db.Products.Where(c => c.WebsiteId == webid).OrderByDescending(i => i.PercentSale).Skip(skip).Take(take);
        }
        public IEnumerable<WebsiteInfo> GetWebsites(ref DateTime realUsedDate)
        {
            KMOLContext db = factoryContext.Create(true, DateTime.Now, ref realUsedDate);
            return db.Websites;
        }
        public WebsiteInfo GetWebsiteByProduct(ProductInfo product)
        {
            KMOLContext db = factoryContext.Create(true, DateTime.Now);
            return db.Websites.Where(i => i.WebsiteId == product.WebsiteId).FirstOrDefault();
        }
    }
}