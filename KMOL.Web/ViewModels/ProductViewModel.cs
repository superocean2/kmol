using KMOL.Web.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace KMOL.Web.ViewModels
{
    public class ProductViewModel
    {
        public string date { get; set; }
        public IEnumerable<ProductInfo> products { get; set; }
    }
}