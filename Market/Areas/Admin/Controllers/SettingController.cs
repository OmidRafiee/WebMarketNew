using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using WebMarket.DataLayer.Context;
using WebMarket.DomainClasses.Entity;
using WebMarket.ServiceLayer.Interfaces;
using WebMarket.ViewModel.Admin.Setting;

namespace Market.Areas.Admin.Controllers
{
    public partial class SettingController : Controller
    {
        private readonly ISlideShowService _slideShowService;
        private readonly ISettingService _settingService;

        public SettingController(ISlideShowService slideShowService, ISettingService settingService)
        {
            _settingService = settingService;
            _slideShowService = slideShowService;
        }

        // GET: Admin/Setting
        //[Route("Edit")]
        [HttpGet]
        public virtual ActionResult EditSetting()
        {
            var model = _settingService.GetOptionsForEdit();
            return View(model);
        }

      
       
        [HttpPost]
        [ValidateAntiForgeryToken]
        public virtual async Task<ActionResult> EditSetting(EditSettingDataEntry viewModel)
        {
            _settingService.Update(viewModel);
            return MessageBox.Show(" مشخصات فروشگاه با موفقیت ثبت شد", MessageType.Success);

        }


        [HttpGet]
        //[Route("Add-Slide-show")]
        public virtual ActionResult AddSlide()
        {
            if (!_slideShowService.AllowAdd())
            {
                ViewBag.AllowAddSlide = "noAllow";
                return View();
            }

            return View();
        }

        [HttpPost]
        //[Route("Add-Slide-show")]
        [ValidateAntiForgeryToken]
        public virtual async Task<ActionResult> AddSlide(SlideShowDataEntry viewModel, HttpPostedFileBase uploadImage)
        {
            try
            {
                var slideShow = AutoMapperConfig.Configuration.Mapper.Map<SlideShow>(viewModel);

                if (uploadImage != null)
                {
                    slideShow.Image = uploadImage.FileName;
                    //var path = Server.MapPath("~") + "Files\\SlideShowImages\\" + uploadImage.FileName;
                    var path = System.IO.Path.Combine(Server.MapPath("~/Files/SlideShowImages"), uploadImage.FileName);
                    uploadImage.InputStream.ResizeImage(1400,377, path, Utilty.ImageComperssion.Minimum);

                    if (ModelState.IsValid)
                    {
                        if (await _slideShowService.Create(slideShow))
                        {
                            return MessageBox.Show("اسلایدرشو با موفقیت ثبت شد", MessageType.Success);
                        }
                        System.IO.File.Delete(path);
                        return MessageBox.Show("در ثبت اطلاعات خطا رخ داد !", MessageType.Error);
                    }
                    return MessageBox.Show(ModelState.GetErrors(), MessageType.Warning);
                }
                return MessageBox.Show("آدرس تصویر نباید خالی باشد", MessageType.Warning);
            }
            catch (Exception)
            {
                return MessageBox.Show(ModelState.GetErrors(), MessageType.Warning);
            }
        }

        [HttpGet]
        //[Route("Slides-List")]
        public virtual ActionResult ListSlide()
        {
            var slides = _slideShowService.List();
            return View(slides);
        }

        [HttpGet]
        public virtual ActionResult EditSlide(int? id)
        {
            if (id == null)
            {
                return MessageBox.Show("اسلایدری با این مشخصات موجود نمی باشد");
            }
            var model = _slideShowService.GetForEdit(id.Value);
            return View(model);
        }

        [HttpPost]
        public virtual ActionResult EditSlide(SlideShowDataEntry viewModel, HttpPostedFileBase uploadImage)
        {
            if (ModelState.IsValid)
            {
                string path = null;
                if (uploadImage != null)
                {
                    path = uploadImage.FileName;
                    uploadImage.InputStream.ResizeImage(1400, 377, Server.MapPath("~") + "Files\\SlideShowImages\\" + path, Utilty.ImageComperssion.Minimum);
                }
                if (_slideShowService.Update(viewModel, path) != null)
                {
                    return MessageBox.Show("اسلایدرشو با موفقیت ویرایش شد", MessageType.Success);
                }
                if (path != null)
                {
                    System.IO.File.Delete(Server.MapPath("~") + "Files\\SlideShowImages\\" + path);

                }
                return MessageBox.Show("در ثبت اطلاعات خطا رخ داد !", MessageType.Error);
            }
            return MessageBox.Show(ModelState.GetErrors(), MessageType.Warning);
        }

        [HttpGet]
        public virtual ActionResult DeleteSlide(int id)
        {
            _slideShowService.Delete(id);
            return RedirectToAction("ListSlide");
        }

       
        [HttpGet]
        public virtual ActionResult DetailsSlide(int? id)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            var slideShow = _slideShowService.GetSlideShowDetail(id.Value);
            if (slideShow == null)
                return HttpNotFound();
            return View(slideShow);
        }
    }
}