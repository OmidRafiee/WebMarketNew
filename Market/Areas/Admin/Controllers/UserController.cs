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
using WebMarket.DomainClasses.Enums;
using WebMarket.ViewModel.Admin.User;
using System.Net;


namespace Market.Areas.Admin.Controllers
{
    
    public partial class UserController : Controller
    {
        // GET: Admin/User

        #region Fields

        private readonly IApplicationUserManager _userService;
        private readonly IApplicationRoleManager _roleService;


        #endregion

        #region Constructor

        public UserController(IApplicationUserManager userService,
                                IApplicationRoleManager roleService)
        {
            _userService = userService;
            _roleService = roleService;
        }

        #endregion

        #region Index,List

        [HttpGet]
        public virtual ActionResult Index()
        {
            ViewBag.UserSearchByList = DropDown.GetUserSearchByList(UserSearchBy.UserName);
            //_roleService.FindUserRoles ( 1 )
            return View();
        }

        [OutputCache(Location = OutputCacheLocation.None, NoStore = true)]
        public virtual ActionResult List(string term = "",
                                           int pageNumber = 1,
                                           int pageCount = 10,
                                           Order order = Order.Descending,
                                           UserOrderBy userOrderBy = UserOrderBy.UserName,
                                           UserSearchBy userSearchBy = UserSearchBy.UserName)
        {
            #region Retrive Data

            int totalUsers;
            var users = _userService.GetDataTable(out totalUsers,
                                                    term,
                                                    pageNumber,
                                                    pageCount,
                                                    order,
                                                    userOrderBy,
                                                  userSearchBy);
            var model = new UsersListViewModel
                        {
                            UserOrderBy = userOrderBy,
                            Term = term,
                            PageNumber = pageNumber,
                            Order = order,
                            UsersList = users,
                            TotalUsers = totalUsers,
                            PageCount = pageCount
                        };

            #endregion

            ViewBag.UserSearchByList = DropDown.GetUserSearchByList(userSearchBy);
            ViewBag.UserOrderByList = DropDown.GetUserOrderByList(userOrderBy);
            ViewBag.CountList = DropDown.GetCountList(pageCount);
            ViewBag.OrderList = DropDown.GetOrderList(order);
            ViewBag.UserSearchBy = userSearchBy;
            return PartialView(MVC.Admin.User.Views._ListUser,model);
        }


        #endregion

        #region Details

        [HttpGet]
        //[Route("Details/{id:long}")]
        public virtual ActionResult Details(int? id)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            var user = _userService.GetUserDetail(id.Value);
            if (user == null)
                return HttpNotFound();


            return View(user);
        }

        #endregion

        #region Edit User

        //[Route ( "Edit/{id}" )]
        [HttpGet]
        public virtual async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            var user = await _userService.FindByIdAsync(id.Value);
            if (user == null)
            {
                return HttpNotFound();
            }

            //var user1 = AutoMapperConfig.Configuration.Mapper.Map<ApplicationUser>(user);

            //    ViewBag.Roles = new SelectList(_userService.GetAllRoles(), "Id", "Description", user.RoleId);
            //ProjectTo < DetailsUserViewModel > ( Market.AutoMapperConfig.Configuration.MapperConfiguration )  .FirstOrDefault ();

            return View(new WebMarket.ViewModel.Admin.User.UserDataEntry
                        {
                            Id = user.Id,
                            Name = user.Name,
                            Family = user.Family,
                            BirthDate = user.BirthDate,
                            UserName = user.UserName,
                            PhoneNumber = user.PhoneNumber,
                            Email = user.PhoneNumber
                        });
        }



        #endregion

        #region Delete User

        public virtual async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var role = await _userService.FindByIdAsync(id.Value);
            if (role == null)
            {
                return HttpNotFound();
            }

            return View(new DetailsUserViewModel
            {
                Name = role.Name,
                Family = role.Family,
                UserName = role.UserName,
                PhoneNumber = role.PhoneNumber,
                Email = role.PhoneNumber
            });
        }


        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public virtual async Task<ActionResult> DeleteConfirmed(int? id)
        {
            if (ModelState.IsValid)
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }

                var user = await _userService.FindByIdAsync(id.Value);
                if (user == null)
                {
                    return HttpNotFound();
                }

                var result = await _userService.DeleteAsync(user);
                if (!result.Succeeded)
                {
                    return MessageBox.Show("در عملیات حذف کاربر مشکلی رخ داد", MessageType.Error);
                }

                return RedirectToAction(MVC.Admin.User.List());
            }
            return View();
        }
        #endregion

        #region Add User

        [HttpGet]
        public virtual async Task<ActionResult> Create()
        {
            ViewBag.RoleId = new SelectList(await _roleService.GetAllCustomRolesAsync(), "Name", "Name");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public virtual async Task<ActionResult> Create(WebMarket.ViewModel.Admin.User.UserDataEntry model, params string[] selectedRoles)
        {
            if (selectedRoles != null)
            {
                if (ModelState.IsValid)
                {

                    model.BirthDate = DateTime.Now;
                    var user = AutoMapperConfig.Configuration.Mapper.Map<ApplicationUser>(model);

                    var adminresult = await _userService.CreateAsync(user, model.Password);

                    //Add User to the selected Roles
                    if (adminresult.Succeeded)
                    {
                        await _userService.AddToRolesAsync(user.Id, selectedRoles);
                        return MessageBox.Show("اطلاعات با موفقیت ثبت شد", MessageType.Success);
                    }
                }
                return MessageBox.Show(ModelState.GetErrors(), MessageType.Warning);

            }
            return MessageBox.Show("ابتدا نقش کاربر را مشخص کنید", MessageType.Warning);

        }



        #endregion

        #region RemoteValidation

        [HttpPost]
        [OutputCache(Location = OutputCacheLocation.None, NoStore = true)]
        public virtual ActionResult CheckUserName(string userName)
        {
            var colection = _userService.GetAllUserName();
            foreach (var item in colection)
            {
                if (item.ToLowerInvariant() == userName.ToLowerInvariant())
                {
                    return Json(false);
                }
            }
            return Json(true);
        }

        [HttpPost]
        [OutputCache(Location = OutputCacheLocation.None, NoStore = true)]
        public virtual ActionResult CheckEmail(string email)
        {
            var colection = _userService.GetAllEmail();
            foreach (var item in colection)
            {
                if (item.ToLowerInvariant() == email.ToLowerInvariant())
                {
                    return Json(false);
                }
            }
            return Json(true);
        }

        #endregion

    }
}
