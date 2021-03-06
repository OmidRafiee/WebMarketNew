﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using WebMarket.DomainClasses.Enums;
using WebMarket.ServiceLayer.Interfaces;
using WebMarket.Utilities;

namespace Market.Areas.Admin.Controllers
{
    public partial class FactorController : Controller
    {
        private readonly IFactorService _factorService;
        private readonly IEmailService _emailService;

        public FactorController(IFactorService factorService, IEmailService emailService)
        {
            _factorService = factorService;
            _emailService = emailService;
        }

        [HttpGet]
        public virtual ActionResult Index()
        {
           return View();
        }

        
        [OutputCache(Location = System.Web.UI.OutputCacheLocation.None, NoStore = true)]
        public virtual async Task<ActionResult> List(int pageNumber = 1, int pageCount = 10, Payments paymentBy = Payments.Paid)
        {

            int totalFactors;
            var factor = _factorService.GetFactorData(out totalFactors, pageNumber, pageCount, paymentBy);

            var model = new WebMarket.ViewModel.User.Factor.FactorListViewModel
            {
                FactorList = factor,
                PageNumber = pageNumber,
                TotalFactors = totalFactors,
                PageCount = pageCount,
                PaymentBy = paymentBy
            };

            ViewBag.PaymentListBy = EnumToList(typeof(Payments),null);// DropDown.GetPaymentList(paymentBy);
            ViewBag.PaymentTitle = GetEnumDisplayValue ( paymentBy );
            return  PartialView(MVC.Admin.Factor.Views._ListFactor, model);
        }

       
        [HttpGet]
        public virtual async Task<ActionResult> SendMail(string email)
        {
            return await Task.Run(() => View(model: email)).ConfigureAwait(false);
        }

        [HttpPost]
        [ValidateInput(false)]
        public virtual async Task<ActionResult> SendMail(string recivers, string title, string text, HttpPostedFileBase attachment)
        {
            var result = false;
            if (attachment != null)
            {
                var path = Server.MapPath("~"+"\\Files\\Attachment\\"+System.IO.Path.GetFileName(attachment.FileName));
                attachment.SaveAs(path);
                result = await _emailService.SendMailByAttach(title, text, path, recivers.Split(','));
            }
            else
            {
                result = await _emailService.SendMail(title, text, recivers.Split(','));
            }
            if (result)
            {
                return MessageBox.Show("پیام با موفقیت ارسال شد", MessageType.Success);
            }
            return MessageBox.Show("در ارسال پیام خطا رخ داد", MessageType.Error);
        }



        //ForBase Contorler
        public string GetEnumDisplayValue(Enum enumName)
        {
            var type = (Type)enumName.GetType();
            var field = type.GetField(enumName.ToString());
            if ( field == null )
                return enumName.ToString ();
            var display = ((System.ComponentModel.DataAnnotations.DisplayAttribute[])field.GetCustomAttributes(typeof(System.ComponentModel.DataAnnotations.DisplayAttribute), false)).FirstOrDefault();
            return display != null
                ? @display.GetName()
                : enumName.ToString();
        }

        public List<SelectListItem> EnumToList(Type enumType, Enum selectedItem)
        {
            var items = new List<SelectListItem>();
            if (enumType == null)
                return items;

            var values = Enum.GetValues(enumType);
            items.AddRange(from Enum item in values
                           select new SelectListItem
                           {
                               Value = item.ToString(),
                               Text = GetEnumDisplayValue(item),
                               Selected = selectedItem != null && item.ToString() == selectedItem.ToString()
                           });
            return items.OrderBy(item => item.Text)
                        .ToList();
        }
    }
}