using KMOL.Web.Data;
using KMOL.Web.Models;
using KMOL.Web.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;

namespace KMOL.Web.Controllers
{
    public class DatasController:ApiController
    {
        ProductService _service;
        public DatasController()
        {
            _service = new ProductService();
        }
        [HttpGet]
        public IEnumerable<string> Test()
        {
            string[] a = { "a", "b", "c" };
            return a;
        }
        [HttpGet]
        public IEnumerable<ProductInfo> HomeProducts(int webid,int pageIndex,int pageSize)
        {
            return _service.GetProducts(webid,true, pageIndex * pageSize, pageSize);
        }
        [HttpGet]
        public IEnumerable<ProductInfo> AllProducts(int webid,int pageIndex, int pageSize)
        {
            return _service.GetProducts(webid,false, pageIndex * pageSize, pageSize);
        }
        [HttpGet]
        public ProductInfo HomeProducts(int id)
        {
            return _service.GetProductById(true, id,DateTime.Now);
        }
        [HttpGet]
        public ProductInfo AllProducts(int id)
        {
            return _service.GetProductById(false, id,DateTime.Now);
        }
    }
}