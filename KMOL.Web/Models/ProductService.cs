using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using KMOL.Web.Data;

namespace KMOL.Web.Models
{
    public class ProductService : IProductService
    {
        KMOLContextFactory factoryContext = new KMOLContextFactory();
        public ProductInfo GetProductById(bool isHomeLinks, int id)
        {
            KMOLContext db = factoryContext.Create(isHomeLinks);
            return db.Products.Find(id);
        }

        public IEnumerable<ProductInfo> GetProducts(bool isHomeLinks,int skip,int take)
        {
            KMOLContext db = factoryContext.Create(isHomeLinks);
            return db.Products.Skip(skip).Take(take);
        }
    }
}