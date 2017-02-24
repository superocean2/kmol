using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using KMOL.Web.Data;
using KMOL.Web.Models;

namespace KMOL.Web.Controllers
{
    public class DetailController : Controller
    {
        ProductService _service;
        public DetailController()
        {
            _service = new ProductService();
        }
        public ActionResult Index(int id,string date,int where)
        {
            var product = _service.GetProductById(where==0?true:false,id);

            return View();
        }
    }
}
