using KMOL.Web.Data;
using KMOL.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;

namespace KMOL.Web.Controllers
{
    public class DataHomeController:ApiController
    {
        IProductService _service;
        public DataHomeController(IProductService service)
        {
            _service = service;
        }
        public IEnumerable<ProductInfo> HomeProducts(int pageIndex,int pageSize)
        {
            return _service.GetProducts(true, pageIndex * pageSize, pageSize);
        }
        public IEnumerable<ProductInfo> AllProducts(int pageIndex, int pageSize)
        {
            return _service.GetProducts(false, pageIndex * pageSize, pageSize);
        }
    }
}