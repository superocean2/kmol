using KMOL.Web.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace KMOL.Web.Models
{
    public interface IProductService
    {
        IEnumerable<ProductInfo> GetProducts(bool isHomeLinks, int skip, int take);
        ProductInfo GetProductById(bool isHomeLinks, int id);
    }
}