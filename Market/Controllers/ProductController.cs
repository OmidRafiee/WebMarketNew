using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebMarket.ServiceLayer.Interfaces;
using WebMarket.ViewModel.Admin.Group;
using WebMarket.ViewModel.Admin.Product;

namespace Market.Controllers
{
    public partial class ProductController : Controller
    {
        private readonly IProductService _productService;
        private readonly IGroupService _groupService;

        public ProductController (IProductService productService ,IGroupService groupService )
        {
            _productService = productService;
            _groupService = groupService;
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

        [HttpGet]
        public virtual ActionResult SearchProduct()
        {
            var model = _productService.ListProducts ();
            return View(model);
        }


        [ChildActionOnly]
        public virtual ActionResult GetGroup()
        {
            var model = _groupService.GetFirstLevelGroups();
            return PartialView(MVC.Product.Views._Groups, model);
        }

        [ChildActionOnly]
        public virtual ActionResult GetPrice()
        {
            var model = _productService.GetPrice();
            return PartialView(MVC.Product.Views._Price, model);
        }

        [ChildActionOnly]
        public virtual ActionResult SingelProduct()
        {
            var model = _productService.ListProducts();
            // var product = AutoMapperConfig.Configuration.Mapper.Map<ProductsViewModel>(model);
            return PartialView(MVC.Product.Views._SingelProduct, model);
        }
        
    
    }
}