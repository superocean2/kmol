using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using KMOL.Web.Data;
using KMOL.Web.Models;

namespace KMOL.Web.Controllers
{
    public class HomeController : Controller
    {
        ProductService _service;
        public HomeController()
        {
            _service = new ProductService();
        }
        public ActionResult Index()
        {
            ViewBag.Title = "Home Page";
            DateTime realUsedDate = new DateTime();
            var websites = _service.GetWebsites(ref realUsedDate);
            ViewBag.UsedDate = realUsedDate.ToString("dd-MM-yyyy");
            return View(websites);
        }
    }
}
