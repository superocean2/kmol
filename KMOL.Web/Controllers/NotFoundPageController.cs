using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace KMOL.Web.Controllers
{
    public class NotFoundPageController : Controller
    {
        // GET: NotFoundPage
        public ActionResult Index()
        {
            return View();
        }
    }
}