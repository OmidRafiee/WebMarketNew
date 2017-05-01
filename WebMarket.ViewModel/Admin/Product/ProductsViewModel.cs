using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace WebMarket.ViewModel.Admin.Product
{
    public class ProductsViewModel
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "نام محصول را وارد کنید")]
        [Display(Name = "نام محصول")]
        [StringLength(50, ErrorMessage = "تعداد کاراکتر ها می بایست حداکثر 50 کاراکتر باشد")]
        public string Name { get; set; }

        [Required(ErrorMessage = "قیمت محصول را وارد کنید", AllowEmptyStrings = false)]
        [Display(Name = "قیمت محصول")]
        [Range(0, int.MaxValue, ErrorMessage = "مقدار وارد شده نامعتبر است")]
        public int Price { get; set; }

        [Required(ErrorMessage = "قیمت محصول را وارد کنید", AllowEmptyStrings = false)]
        [Display(Name = "قیمت تخفیفی محصول")]
        [Range(0, int.MaxValue, ErrorMessage = "مقدار وارد شده نامعتبر است")]
        public int OffPrice { get; set; }

        [Required(ErrorMessage = "آدرس محصول را وارد کنید")]
        [Display(Name = "آدرس محصول")]
        //[System.Web.Mvc.Remote(action: "CheckUrl", controller: "Admin", HttpMethod = "POST", ErrorMessage = "(جهت ایجاد صفحه اختصاصی) آدرس وارد شده را بیشتر برای محصولی دیگر استفاده کرده اید  ")]
        //[DataType(DataType.Url,ErrorMessage = "آدرس محصول نامعتبر است")]
        [StringLength(100, ErrorMessage = "این فیلد باید حداکثر 100 کاراکتر باشد")]
        public string Url { get; set; }

        [Display(Name = "کلمات کلیدی")]
        [StringLength(300, ErrorMessage = "تعداد کاراکتر ها می بایست حداکثر 300 کاراکتر باشد")]
        public string KeyWord { get; set; }

        [Display(Name = "توضیحات")]
        [StringLength(500, ErrorMessage = "تعداد کاراکتر ها می بایست حداکثر 500 کاراکتر باشد")]
        public string Description { get; set; }

        [AllowHtml]
        [Required(ErrorMessage = "توضیحات محصول را وارد کنید", AllowEmptyStrings = false)]

        [Display(Name = "توضیحات محصول")]
        [DataType(DataType.Html, ErrorMessage = "فرمت متن نا معتبر است")]
        public string Summery { get; set; }

        [Display(Name = "برچسپ ها")]
        [StringLength(200, ErrorMessage = "تعداد کاراکتر ها می بایست حداکثر 200 کاراکتر باشد")]
        public string Tag { get; set; }

        [Display(Name = "محصول فعال است")]
        public bool Enable { get; set; }

        [Display(Name = "گروه محصول")]
        public int GroupId { get; set; }

        [Display(Name = "تصویر محصول")]
        [DataType(DataType.ImageUrl)]
        public string Image { get; set; }

        public string GroupName { get; set; }
    }
}
