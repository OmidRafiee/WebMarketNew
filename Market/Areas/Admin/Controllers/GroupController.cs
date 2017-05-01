using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using WebMarket.DomainClasses.Entity;
using WebMarket.DomainClasses.Enums;
using WebMarket.ServiceLayer.Interfaces;
using WebMarket.Utilities;
using WebMarket.ViewModel.Admin.Group;

namespace Market.Areas.Admin.Controllers
{
    public partial class GroupController : Controller
    {
        private readonly IGroupService _groupService;

        public GroupController(IGroupService groupService)
        {
            _groupService = groupService;
        }


        public virtual ActionResult Groups()
        {
            return View ();
        }

        [HttpPost]
        public virtual ActionResult GetJsonGroup()
        {
            
            //var list = _groupService.GetAll ().Select ( group => new
            //                                                     {
            //                                                             group.Id ,
            //                                                             group.Name ,
            //                                                             group.ParentId
            //                                                     } );
            var list = _groupService.GetAll ();
            return Json(list, JsonRequestBehavior.AllowGet);
        }

        // GET: Admin/Group
        //[HttpGet]
        //public virtual ActionResult Create()
        //{
        //    PopulateGroupsDropDownList(_groupService.GetFirstLevelGroup());
        //    return View();
        //}

        // POST: Admin/Group/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        [AjaxOnly]
        public virtual async Task<ActionResult> Create(GroupDataEntriy viewModel)
        {
            if (ModelState.IsValid)
            {
                var group = AutoMapperConfig.Configuration.Mapper.Map<Group>(viewModel);
                var status = await Task.Run(() => _groupService.Add(group));
                if (status == AddGroupStatus.GroupNameExist)
                {
                    return Json(new JsonData()
                    {
                        Script = MessageBox.Show("این نام قبلا ثبت شده است !", MessageType.Error).Script,
                        Success = false,
                        Html = ""
                    });
                    //return MessageBox.Show("این نام قبلا ثبت شده است", MessageType.Error);
                }

                 return Json(new JsonData()
                    {
                        Script = MessageBox.Show("اطلاعات با موفقیت ثبت شد", MessageType.Success).Script,
                        Success = true,
                        Html = this.RenderPartialToString("_ListGroup", _groupService.GetAll())

                    });
               
                //return MessageBox.Show("اطلاعات با موفقیت ثبت شد", MessageType.Success);
            }
 
            return View(viewModel);
        }

        private class JsonData
        {
            public string Script { get; set; }
            public string Html { get; set; }
            public bool Success { get; set; }
        }

     
    }
}