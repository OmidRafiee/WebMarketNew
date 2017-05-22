using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebMarket.ServiceLayer.Interfaces;
using System.Threading.Tasks;
using WebMarket.Utilities;
using WebMarket.ViewModel.User.User;
using WebMarket.ViewModel.User.Product;
using WebMarket.ViewModel.Admin.Setting;

namespace Market.Controllers
{
    public partial class HomeController : Controller
    {
        private readonly IProductService _productService;
        private readonly IGroupService _groupService;
        private readonly ISettingService _settingService;

        private readonly ISlideShowService _slideShowService;


        public HomeController(IProductService productService, IGroupService groupService,ISettingService settingService,ISlideShowService slideShowService)
        {
            _productService = productService;
            _groupService = groupService;
            _settingService = settingService;
            _slideShowService = slideShowService;
        }

        [HttpGet]
        // GET: Home
        public virtual ActionResult Index()
        {
           
           var model = new HomeIndexViewModel();
           //model.Groups = _groupService.GetAll();

           //model.SlideShows = _slideShowService.List();
           //model.NewProducts = _productService.GetMoreSellProduct(6);
           //model.BestSellerProducts = _productService.GetMoreSellProduct(6);
           //model.PopularProducts = _productService.GetMoreSellProduct(6);
            
          
            return View(model);
        }

        [ChildActionOnly]
        //[OutputCache(Duration = oneDay, VaryByParam = "none")]
        public virtual ActionResult SlideShow()
        {
            var slides = _slideShowService.List();
            return PartialView(MVC.Home.Views._SlideShow, slides);
        }

        [ChildActionOnly]
        //[OutputCache(Duration = 24, VaryByParam = "none")]
        public virtual ActionResult Footer()
        {
            var model = _settingService.GetOptionsForShowOnFooter();
            return PartialView(MVC.Home.Views._Footer, model);
           
        }
        
        //[OutputCache(Duration = min15, VaryByParam = "none")]
        [ChildActionOnly]
        public virtual ActionResult MoreSellProducts()
        {
            var products = _productService.GetMoreSellProduct(6);
            return PartialView(MVC.Home.Views._MoreSellProducts, products);
        }

        //[OutputCache(Duration = min15, VaryByParam = "none")]
        [ChildActionOnly]
        public virtual ActionResult NewProducts()
        {
            var products = _productService.GetMoreSellProduct(8);
            return PartialView(MVC.Home.Views._NewProducts, products);
        }

        //[OutputCache(Duration = min15, VaryByParam = "none")]
        [ChildActionOnly]
        public virtual ActionResult PopularProducts()
        {
            var products = _productService.GetMoreSellProduct(4);
            return PartialView(MVC.Home.Views._PopularProducts, products);
        }

       

    }
}