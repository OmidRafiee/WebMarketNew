using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebMarket.DomainClasses.Entity;
using WebMarket.ServiceLayer.Interfaces;
using System.Threading.Tasks;
using WebMarket.Utilities;
using WebMarket.ViewModel.User.User;
using WebMarket.DomainClasses;
using System.Web.UI;

namespace Market.Controllers
{
    public partial class UserController : Controller
    {
        private readonly IApplicationUserManager _userManager;

        public UserController(IApplicationUserManager userService)
        {
            _userManager = userService;
        }

        [HttpGet]
        public virtual async Task<ActionResult> Add()
        {
            return await Task.Run(() => View()).ConfigureAwait(false);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public virtual async Task<ActionResult> Add(UserDataEntry model)
        {
            if (Session["Captcha"] == null || Session["Captcha"].ToString() != model.Captcha)
            {
                ModelState.AddModelError("Captcha", "مجموع اشتباه است");
            }

            if (!ModelState.IsValid)
            {
                return MessageBox.Show(ModelState.GetErrors(), MessageType.Warning);
            }
            
            var user = AutoMapperConfig.Configuration.Mapper.Map<ApplicationUser>(model);
          
            var adminresult = await _userManager.CreateAsync(user, model.Password);
            if (adminresult.Succeeded)
            {
                var result = await _userManager.AddToRolesAsync(user.Id, "Admin");
                if (!result.Succeeded)
                {
                    return MessageBox.Show(result.Errors.First(), MessageType.Warning);
                }
            }
            else
            {
                return MessageBox.Show(adminresult.Errors.First(), MessageType.Warning);
            }

            return MessageBox.Show("اطلاعات با موفقیت ثبت شد", MessageType.Success);
        }


        [HttpPost]
        [OutputCache(Location = OutputCacheLocation.None, NoStore = true)]
        public virtual ActionResult CheckEmail(string email)
        {
            var colection = _userManager.GetAllEmail ();
            foreach (var item in colection)
            {
                if (item.ToLowerInvariant() == email.ToLowerInvariant())
                {
                    return Json(false);
                }
            }
            return Json(true);
        }


        [HttpPost]
        [OutputCache(Location = OutputCacheLocation.None, NoStore = true)]
        public virtual ActionResult CheckUserName(string userName)
        {
            var colection = _userManager.GetAllUserName();
            foreach (var item in colection)
            {
                if (item.ToLowerInvariant() == userName.ToLowerInvariant())
                {
                    return Json(false);
                }
            }
            return Json(true);
        }


        //public virtual async Task<ActionResult> Add(RegisterViewModel userViewModel, HttpPostedFileBase userImage)
        //{
        //    if (userViewModel.Id.HasValue)
        //    {
        //        ModelState.Remove("Password");
        //        ModelState.Remove("ConfirmPassword");
        //    }

        //    if (!ModelState.IsValid)
        //    {
        //        return View(userViewModel);
        //    }

        //    if (!userViewModel.Id.HasValue)
        //    {
        //        var user = new ApplicationUser
        //        {
        //            UserName = userViewModel.UserName,
        //            Email = userViewModel.Email,
        //            EmailConfirmed = true
        //        };

        //        var adminresult = await _userManager.CreateAsync(user, userViewModel.Password);

        //        if (adminresult.Succeeded)
        //        {
        //            var result = await _userManager.AddToRolesAsync(user.Id, "Admin");
        //            if (!result.Succeeded)
        //            {
        //                ModelState.AddModelError("", result.Errors.First());
        //                return View();
        //            }
        //        }
        //        else
        //        {
        //            ModelState.AddModelError("", adminresult.Errors.First());

        //            return View();
        //        }

        //        TempData["message"] = "کاربر جدید با موفقیت در سیستم ثبت شد";

        //    }
        //    else
        //    {
        //        var user = await _userManager.FindByIdAsync(userViewModel.Id.Value);

        //        if (user == null)
        //        {
        //            return HttpNotFound();
        //        }

        //        user.UserName = userViewModel.UserName;
        //        user.Email = userViewModel.Email;

        //        await _unitOfWork.SaveAllChangesAsync();

        //        TempData["message"] = "کاربر مورد نظر با موفقیت ویرایش شد";

        //    }

        //    if (userImage != null)
        //    {
        //        var img = new WebImage(userImage.InputStream);
        //        img.Resize(161, 161, true, false).Crop(1, 1);

        //        img.Save(Server.MapPath("~/UploadedFiles/Avatars/" + userViewModel.UserName + ".png"));
        //    }


        //    return RedirectToAction(MVC.User.Admin.ActionNames.Index);
        //}
    }
}
