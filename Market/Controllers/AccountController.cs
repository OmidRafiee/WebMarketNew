using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using WebMarket.ServiceLayer.Interfaces;
using WebMarket.ViewModel.User.Account;

namespace Market.Controllers
{
    public partial class AccountController : Controller
    {
        private readonly IEmailService _emailService;


        private readonly IAuthenticationManager _authenticationManager;
        private readonly IApplicationSignInManager _signInManager;
        private readonly IApplicationUserManager _userManager;
        public AccountController(IApplicationUserManager userManager,
                                 IApplicationSignInManager signInManager,
                                 IAuthenticationManager authenticationManager, 
                                IEmailService emailService)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _authenticationManager = authenticationManager;
            _emailService = emailService;
        }

        [AllowAnonymous]
        public virtual ActionResult Login(string returnUrl)
        {
            ViewBag.ReturnUrl = returnUrl;
            return View();
        }

        // POST: /Account/Login
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public virtual async Task<ActionResult> Login(LoginViewModel model, string returnUrl)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            // This doesn't count login failures towards lockout only two factor authentication
            // To enable password failures to trigger lockout, change to shouldLockout: true
            var result = await _signInManager.PasswordSignInAsync(model.UserName , model.Password, model.RememberMe, shouldLockout: false);

            /*var userName = User.Identity.GetUserName();
            if (string.IsNullOrWhiteSpace(userName))
            {

            }*/

            switch (result)
            {
                case SignInStatus.Success:
                    return redirectToLocal(returnUrl);
                case SignInStatus.LockedOut:
                    return View(MVC.Shared.Views.Error);
                case SignInStatus.RequiresVerification:
                    return RedirectToAction(MVC.Account.ActionNames.SendCode, new { ReturnUrl = returnUrl });
                default:
                    ModelState.AddModelError("", "نام کاربری / کلمه عبور مورد تایید نیست");
                    return View(model);
            }
        }

        
        //// POST: /Account/LogOff
       
        public virtual async Task<ActionResult> LogOff()
        {
            var user = await _userManager.FindByNameAsync(User.Identity.Name);
            _authenticationManager.SignOut(DefaultAuthenticationTypes.ApplicationCookie);
            await _userManager.UpdateSecurityStampAsync(user.Id);

            return RedirectToAction(MVC.Home.Index());
        }

        private ActionResult redirectToLocal(string returnUrl)
        {
            if (Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }
            return RedirectToAction(MVC.Home.Index());
        }

        // GET: /Account/ForgotPasswor
        [HttpGet]
        [AllowAnonymous]
        public virtual ActionResult ForgotPassword()
        {
            return View();
        }

        //
        // POST: /Account/ForgotPassword
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public virtual async Task<ActionResult> ForgotPassword(ForgotPasswordViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByEmailAsync(model.Email);
                if (user == null || !(await _userManager.IsEmailConfirmedAsync(user.Id)))
                {
                    // Don't reveal that the user does not exist or is not confirmed
                    ViewBag.MassegeFail = "پست الکترونیکی با ابن مشخصات موجود نمی باشد";
                    return View(model);
                }

                var code = await _userManager.GeneratePasswordResetTokenAsync(user.Id);
                var callbackUrl = Url.Action("ResetPassword", "Account",
                    new { userId = user.Id, code }, protocol: Request.Url.Scheme);
                await _userManager.SendEmailAsync(user.Id, "Reset Password", "Please reset your password by clicking here: <a href=\"" + callbackUrl + "\">link</a>");

                await _emailService.SendMail( "Reset Password" ,
                                               "Please reset your password by clicking here: <a href=\"" + callbackUrl +
                                               "\">link</a>" ,
                                               user.Email );

                ViewBag.MassegeSuccess = "  لینک بازیابی حداکثر تا 5 دقیقه دیگر به پست الکترونیکی شما ارسال می شود. در صورت عدم مشاهده قسمت اسپم پست الکترونیکی خود را چک کنید.";
                return View(model);
            }

            // If we got this far, something failed, redisplay form
            return View(model);
        }

        // GET: /Account/ResetPassword
        [AllowAnonymous]
        public virtual ActionResult ResetPassword(string code)
        {
            return code == null ? View(MVC.Shared.Views.Error) : View();
        }

        
        // POST: /Account/ResetPassword
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public virtual async Task<ActionResult> ResetPassword(ResetPasswordViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            var user = await _userManager.FindByEmailAsync(model.Email);
            if (user == null)
            {
                // Don't reveal that the user does not exist
                ViewBag.MassegeFail = "پست الکترونیکی با ابن مشخصات موجود نمی باشد";
                return View(model);
            }
            var result = await _userManager.ResetPasswordAsync(user.Id, model.Code, model.Password);
            if (result.Succeeded)
            {
                TempData["MassegeSuccess"] = "کلمه عبور با موفقیت تغییر پیدا کرد";
                return RedirectToAction("ResetPasswordConfirmation", "Account");
            }
            addErrors(result);
            return View();
        }

        // GET: /Account/ResetPasswordConfirmation
        [AllowAnonymous]
        public virtual ActionResult ResetPasswordConfirmation()
        {
            return View();
        }

        // POST: /Account/SendCode
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public virtual async Task<ActionResult> SendCode(SendCodeViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            // Generate the token and send it
            if (!await _signInManager.SendTwoFactorCodeAsync(model.SelectedProvider))
            {
                return View(MVC.Shared.Views.Error);
            }
            return RedirectToAction("VerifyCode", new { Provider = model.SelectedProvider, model.ReturnUrl });
        }

        //
        // GET: /Account/VerifyCode
        [AllowAnonymous]
        public virtual async Task<ActionResult> VerifyCode(string provider, string returnUrl)
        {
            // Require that the user has already logged in via username/password or external login
            if (!await _signInManager.HasBeenVerifiedAsync())
            {
                return View("Error");
            }
            var user = await _userManager.FindByIdAsync(await _signInManager.GetVerifiedUserIdAsync());
            if (user != null)
            {
                ViewBag.Status = "For DEMO purposes the current " + provider + " code is: " + await _userManager.GenerateTwoFactorTokenAsync(user.Id, provider);
            }
            return View(new VerifyCodeViewModel { Provider = provider, ReturnUrl = returnUrl });
        }

        //
        // POST: /Account/VerifyCode
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public virtual async Task<ActionResult> VerifyCode(VerifyCodeViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var result = await _signInManager.TwoFactorSignInAsync(model.Provider, model.Code, isPersistent: false, rememberBrowser: model.RememberBrowser);
            switch (result)
            {
                case SignInStatus.Success:
                    return redirectToLocal(model.ReturnUrl);
                case SignInStatus.LockedOut:
                    return View("Error");
                default:
                    ModelState.AddModelError("", "Invalid code.");
                    return View(model);
            }
        }

        private void addErrors(IdentityResult result)
        {
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError("", error);
            }
        }


    }
}