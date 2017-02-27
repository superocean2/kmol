using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace KMOL.Web.ViewModels
{
    public class ProductDetailViewModel
    {
        public string ImageUrl { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public decimal SavedPrice { get; set; }
        public decimal PercentSale { get; set; }
        public decimal OldPrice { get; set; }
        public string LinkDetail { get; set; }
    }
}