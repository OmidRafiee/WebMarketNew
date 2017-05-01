using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebMarket.ViewModel.Admin.Setting
{
    public class EditSettingDataEntry
    {
        [ScaffoldColumn(false)]
        public int Id { get; set; }

        [Display(Name = "نام فروشگاه")]
        public string MarketName { get; set; }

        [Display(Name = "توضیحات سئو")]
        [DataType(DataType.MultilineText)]
        public string Url { get; set; }

        [Display(Name = "آدرس فروشگاه")]
        public string ContactUs { get; set; }

        [Display(Name = "شماره تماس  1")]
        public string PhoneNumber1 { get; set; }

        [Display(Name = "شماره موبایل 1")]
        public string PhoneNumber2 { get; set; }

        [Display(Name = "ایمیل")]
        public string Email { get; set; }

        [Display(Name = "درباره ما")]
        public string AboutUs { get; set; }

    }
}
