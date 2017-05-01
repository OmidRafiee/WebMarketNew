using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using WebMarket.DomainClasses.Entity;
using WebMarket.ServiceLayer.Interfaces;
using WebMarket.Utilities;
using WebMarket.ViewModel.Admin.Group;
using WebMarket.ViewModel.Admin.Product;

namespace Market.Areas.Admin.Controllers
{
   
    //[RoutePrefix("Product")]
    //[Route("{action}")]
    public partial class ProductController : Controller
    {
        private readonly IGroupService _groupService;
        private readonly IProductService _productService;

        public ProductController(IGroupService groupService, IProductService productService)
        {
            _groupService = groupService;
            _productService = productService;
        }

        // GET: Admin/Product
        public virtual ActionResult Index()
        {
            var model = _productService.ListProducts();
            return View(model);
        }

        public virtual ActionResult ZTree()
        {
            return View();
        }

        #region Create
        [HttpGet]
        public virtual ActionResult Create()
        {
            var model = new ProductDataEntriy();
            model.Groups = _groupService.GetAll().Select( group => new GroupViewModel
                                                                     {
                                                                             Name = group.Name ,
                                                                             Id = group.Id ,
                                                                             ParentId = group.ParentId
                                                                     });
            return View(model);
        }


        [HttpPost]
        public virtual async Task<ActionResult> Create(ProductDataEntriy model, HttpPostedFileBase uploadImage)
        {
            try
            { 
                
                var product = AutoMapperConfig.Configuration.Mapper.Map<Product>(model);
               
                if (uploadImage != null)
                {
                    product.Image = uploadImage.FileName;
                    var path = Server.MapPath("~") + "Files\\ProductImages\\" + uploadImage.FileName;
                    uploadImage.InputStream.ResizeImageByWidth(500, path, Utilty.ImageComperssion.Normal);

                    if (ModelState.IsValid)
                    {
                        if (_productService.IsGroupSelected(product.GroupId))
                        {
                            return MessageBox.Show("گروه کالا را انتخاب کنید", MessageType.Warning);
                        }
                        if (await Task.Run(() => _productService.Insert(product)))
                        {
                            return MessageBox.Show("اطلاعات با موفقیت ثبت شد", MessageType.Success);
                        }
                        System.IO.File.Delete(path);
                        return MessageBox.Show("در ثبت اطلاعات خطا رخ داد !", MessageType.Error);
                    }
                    return MessageBox.Show(ModelState.GetErrors(), MessageType.Warning);
                }

                if (ModelState.IsValid)
                {
                    if (_productService.IsGroupSelected(product.GroupId))
                    {
                        return MessageBox.Show("گروه کالا را انتخاب کنید", MessageType.Warning);
                    }
                    if (await Task.Run(() => _productService.Insert(product)))
                    {
                        return MessageBox.Show("اطلاعات با موفقیت ثبت شد", MessageType.Success);
                    }
                    //System.IO.File.Delete(path);
                    return MessageBox.Show("در ثبت اطلاعات خطا رخ داد !", MessageType.Error);
                }
                return MessageBox.Show(ModelState.GetErrors(), MessageType.Warning);

            }
            catch (Exception)
            {
                return MessageBox.Show(ModelState.GetErrors(), MessageType.Warning);
            }
        }
        #endregion

        #region Read
        //[HttpGet]
        //public virtual ActionResult Products()
        //{
        //    var model = _productService.ListProducts();
        //    return View(model);
        //}
        
        //[HttpPost]
        //public virtual ActionResult SearchProduct(string searchName)
        //{
        //    //var model = new ProductsViewModel { Products = new[] { _productService.FindByName(searchName)}};
        //     //var model = new ProductsViewModel();
        //   var model= _productService.FindByName(searchName);
        //   return View("Products", model);
        //}
        #endregion

        #region Update

        [HttpGet]
        public virtual ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return MessageBox.Show("محصولی با این مشخصات موجود نمی باشد");
            }
            var model = _productService.GetForEdit(id.Value);
            return View(model);

        }

        [HttpPost]
        [AjaxOnly]
        public virtual ActionResult Edit(ProductDataEntriy product, HttpPostedFileBase uploadImage)
        {
            if (ModelState.IsValid)
            {
                string path = null;
                if (uploadImage != null)
                {
                    path = uploadImage.FileName;
                    uploadImage.InputStream.ResizeImageByWidth(500,
                                                                 Server.MapPath("~") + "Files\\UploadImages\\" + path,
                                                                 Utilty.ImageComperssion.Normal);
                }
                if (_productService.Update(product, path) != null)
                {
                    return MessageBox.Show("اطلاعات با موفقیت ویرایش شد", MessageType.Success);
                }
                else
                {
                    if (path != null)
                    {
                        System.IO.File.Delete(Server.MapPath("~") + "Files\\UploadImages\\" + path);

                    }
                    return MessageBox.Show("در ثبت اطلاعات خطا رخ داد !", MessageType.Error);
                }
            }
            else
            {
                return MessageBox.Show(ModelState.GetErrors(), MessageType.Warning);
            }
        }
        #endregion

        #region Delete
        public virtual ActionResult Delete(int id)
        {
            _productService.Delete(id);
            return RedirectToAction("Index");
        }
       #endregion
   }
}