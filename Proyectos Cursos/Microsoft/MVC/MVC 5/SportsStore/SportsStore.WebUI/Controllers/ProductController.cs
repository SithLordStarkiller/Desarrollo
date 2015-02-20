using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SportsStore.Domain.Abstract;

namespace SportsStore.WebUI.Controllers
{
    public class ProductController : Controller
    {
        private IProductRepository _repository;

        public int PageSize;

        public ProductController(IProductRepository productRepository)
        {
            _repository = productRepository;
        }

        public ViewResult List()
        {
            return View(_repository.Products);
        }

        //public ViewResult List(int page)
        //{
        //    var list = _repository.Products
        //        .OrderBy(p => p.ProductID)
        //        .Skip((page - 1)*PageSize)
        //        .Take(PageSize);
        //    return View(list);
        //}
    }
}