using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.UI;
using WebMarket.ServiceLayer.Interfaces;
using WebMarket.Utilities;
using WebMarket.ViewModel.User.ShoppingCart;

namespace Market.Controllers
{
    public partial class ShoppingCartController : Controller
    {
        private readonly IProductService _productService;
        private readonly IFactorItemService _factorItem;

        public ShoppingCartController(IProductService productService,IFactorItemService factorItem)
        {
            _productService = productService;
            _factorItem = factorItem;
        }


        // GET: ShoppingCart
        //public virtual ActionResult Index()
        //{
        //    return View();
        //}


        [HttpPost]
        public virtual ActionResult AddToCart(int id, byte count)
        {
            try
            {
                if (Request.Cookies.AllKeys.Contains("Market_" + id))
                {
                    //edit cookie
                    var httpCookie = Request.Cookies["Market_" + id];
                    if (httpCookie != null)
                    {
                        var cookie = new HttpCookie("Market_" + id, count.ToString());
                        //cookie.Value += ","+id.ToString ();
                        cookie.Expires = DateTime.Now.AddDays(10);
                        cookie.HttpOnly = true;
                        Response.Cookies.Set(cookie);
                    }
                }
                else
                {
                    //Add new cookie
                    var cookie = new HttpCookie("Market_" + id, count.ToString());
                    cookie.Expires = DateTime.Now.AddDays(10);
                    cookie.HttpOnly = true;
                    Response.Cookies.Add(cookie);
                   // TotalPrice();
                }

                if (count == 1)
                {

                    return Json(new JsonData()
                    {
                        Success = true,
                        Script = MessageBox.Show("کالا به سبد خرید اضافه شد",MessageType.Success).Script,
                        Html =""
                    });

                }
                return Json(new JsonData()
                            {
                                    Success = true,
                                    Script = MessageBox.Show("تعداد کالا سبد خرید ویرایش شد",MessageType.Success).Script,
                                    Html = "(" + "لیست خرید (" + CartCount()
                            });
            }
            catch (Exception)
            {
                return Json(new JsonData()
                {
                    Success = false,
                    Script = MessageBox.Show("کالا به سبد خرید اضافه نشد",MessageType.Error).Script,
                    Html = ""
                });
            }
        }

        public string CartCount()
        {
            var lstCookie = new List<HttpCookie>();
            for (var i = 0; i < Request.Cookies.Count; i++)
            {
                if (lstCookie.Any(p => p.Name == Request.Cookies[i].Name) == false)
                    lstCookie.Add(Request.Cookies[i]);
            }
            var cartCount = lstCookie.Count(p => p.Name.StartsWith("Market_"));
            return cartCount.ToString();
        }

        public virtual PartialViewResult TotalPrice()
        {
            var lstBasket = new List<BasketViewModel>();
            var lstCookie = new List<HttpCookie>();
            for (int i = 0; i < Request.Cookies.Count; i++)
            {
                if (lstCookie.Any(p => p.Name == Request.Cookies[i].Name) == false)
                {
                    lstCookie.Add(Request.Cookies[i]);
                }
            }

            foreach (var item in lstCookie.Where(p => p.Name.StartsWith("Market_")))
            {
                lstBasket.Add(new BasketViewModel
                {
                    Product = _productService.FindById(Convert.ToInt32(item.Name.Substring(7))),
                    Count = Convert.ToInt32(item.Value)
                });
            }
            return PartialView(MVC.ShoppingCart.Views._Basket, lstBasket);
        }

        public virtual ActionResult RemoveCart(int id)
        {
            try
            {
                if (Request.Cookies.AllKeys.Contains("Market_" + id))
                {
                    var httpCookie = Response.Cookies["Market_" + id];
                    if (httpCookie != null)
                        httpCookie.Expires = DateTime.Now.AddDays(-1);
                    Request.Cookies.Remove("Cart_" + id);

                    return Json(new JsonData()
                    {
                        Success = true,
                        Script = MessageBox.Show("کالا از سبد خرید حذف شد",
                                                   MessageType.Success).Script,
                        Html = "(" + "لیست خرید (" + CartCount()
                    });
                }
                return Json(new JsonData()
                            {
                                    Success = false,
                                    Script = MessageBox.Show("این کالا در سبد خرید شما وجود ندارد",
                                                             MessageType.Warning).Script,
                                    Html = "(" + "لیست خرید (" + CartCount()
                            });
            }
            catch (Exception)
            {
                return Json(new JsonData()
                {
                    Success = false,
                    Script = MessageBox.Show("کالا از سبد خرید حذف نشد",MessageType.Error).Script,
                    Html = ""
                });
            }

        }


        [HttpGet]
        public virtual ActionResult Basket()
        {
            var lstBasket = new List<BasketViewModel>();
            var lstCookie = new List<HttpCookie>();
            for (int i = 0; i < Request.Cookies.Count; i++)
            {
                if (lstCookie.Any(p => p.Name == Request.Cookies[i].Name) == false)
                {
                    lstCookie.Add(Request.Cookies[i]);
                }
            }

            foreach (var item in lstCookie.Where(p => p.Name.StartsWith("Market_")))
            {
                lstBasket.Add(new BasketViewModel
                {
                    Product = _productService.FindById(Convert.ToInt32(item.Name.Substring(7))),
                    Count = Convert.ToInt32(item.Value)
                });
            }

            return View(lstBasket);
        }


        [HttpGet]
        public virtual ActionResult Buy()
        {
            return View();
        }

    


    }
}