using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using KMOL.Web.Data;
using KMOL.Web.Models;
using KMOL.Web.ViewModels;

namespace KMOL.Web.Controllers
{
    public class DetailController : Controller
    {
        ProductService _service;
        public DetailController()
        {
            _service = new ProductService();
        }
        public ActionResult Index(int id,string date,int l)
        {
            //date dd-mm-yyyy
            var product = _service.GetProductById(l==0?true:false,id,date.ToDate());
            if (product == null) Response.Redirect($"{SiteConfig.HostName}/pagenotfound/{System.Web.HttpUtility.UrlEncode(Request.Url.AbsoluteUri)}");
            var website = _service.GetWebsiteByProduct(product);
            ProductDetailViewModel detail = new ProductDetailViewModel()
            {
                Name = product.Name,
                ImageUrl = product.ImageUrl,
                Price = product.Price,
                OldPrice = product.OldPrice,
                PercentSale = product.PercentSale,
                SavedPrice = product.OldPrice - product.Price,
                LinkDetail = SiteConfig.AffUrl + System.Web.HttpUtility.UrlEncode(product.Url.Contains("http://") || product.Url.Contains("https://") ? product.Url : website.Url + product.Url)
            };
            return View(detail);
        }
    }
}
