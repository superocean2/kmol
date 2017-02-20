using KMOL.Web.Data;
using KMOL.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;

namespace KMOL.Web.Controllers
{
    public class DatasController:ApiController
    {
        IProductService _service;
        public DatasController(IProductService service)
        {
            _service = service;
        }
        [HttpGet]
        public IEnumerable<ProductInfo> HomeProducts(int pageIndex,int pageSize)
        {
            return _service.GetProducts(true, pageIndex * pageSize, pageSize);
        }
        [HttpGet]
        public IEnumerable<ProductInfo> AllProducts(int pageIndex, int pageSize)
        {
            return _service.GetProducts(false, pageIndex * pageSize, pageSize);
        }
        [HttpGet]
        public ProductInfo HomeProducts(int id)
        {
            return _service.GetProductById(true, id);
        }
        [HttpGet]
        public ProductInfo AllProducts(int id)
        {
            return _service.GetProductById(false, id);
        }
    }
}