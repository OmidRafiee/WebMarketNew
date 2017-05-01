using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebMarket.ServiceLayer.Interfaces;

namespace Market.Controllers
{
    //[RoutePrefix("Product")]
    public partial class ProductController : Controller
    {
        private readonly IProductService _productService;

        public ProductController (IProductService productService)
        {
            _productService = productService;
        }

        public virtual ActionResult ShowProduct(string id)
        {

            if (string.IsNullOrEmpty(id))
            {
                return RedirectToAction("Index");
            }

            var product = _productService.FindByUrl(id);
            if (product != null)
            {
                return View(product);
            }
            return RedirectToAction("Index");
        }
    }
}