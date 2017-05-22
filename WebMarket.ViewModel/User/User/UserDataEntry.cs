using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebMarket.ViewModel.User.User
{
    public class UserDataEntry
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "نام را وارد کنید", AllowEmptyStrings = false)]
        [Display(Name = "نام ")]
        [StringLength(50, ErrorMessage = "تعداد کاراکتر ها می بایست حداکثر 50 کاراکتر باشد")]
        public string Name { get; set; }

        [Required(ErrorMessage = " نام خانوادگی را وارد کنید", AllowEmptyStrings = false)]
        [Display(Name = " نام خانوادگی")]
        [StringLength(50, ErrorMessage = "تعداد کاراکتر ها می بایست حداکثر 50 کاراکتر باشد")]
        public string Family { get; set; }

         [System.Web.Mvc.Remote(action: "CheckUserName", controller: "User", HttpMethod = "POST",
                               ErrorMessage = "(جهت لاگین) نام کاربری وارد شده هم اکنون توسط یکی از کاربران مورد استفاده است ")]
        [Display(Name = "نام کاربری")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "نام کاربری را وارد کنید")]
        [StringLength(50, ErrorMessage = "تعداد کاراکتر ها می بایست حداکثر 50 کاراکتر باشد")]
        [RegularExpression("^[a-zA-Z0-9_]*$", ErrorMessage = "لطفا فقط از حروف انگلیسی و اعدد استفاده کنید")]
        public string UserName { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "کلمه عبور خود را وارد کنید")]
        [StringLength(100, ErrorMessage = "کلمه عبور باید حداقل 6 حرف باشد", MinimumLength = 6)]
        [Display(Name = "کلمه عبور")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "کلمه عبور خود را وارد کنید")]
        [Display(Name = "تکرار کلمه عبور")]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "تکرار کلمه عبور، با کلمه عبور یکسان نیست")]
        public string ConfirmPassword { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "پست الکترونیکی خود را وارد کنید")]
        [System.Web.Mvc.Remote(action: "CheckEmail", controller: "User", HttpMethod = "POST",
                               ErrorMessage = "پست الکترونیکی وارد شده هم اکنون توسط یکی از کاربران مورد استفاده است ")]
        [Display(Name = "ایمیل")]
        [StringLength(50, ErrorMessage = "تعداد کاراکتر ها می بایست حداکثر 50 کاراکتر باشد")]
        [RegularExpression(@"^[_A-Za-z0-9-\+]+(\.[_A-Za-z0-9-]+)*@[A-Za-z0-9-]+(\.[A-Za-z0-9-]+)*(\.[A-Za-z]{2,4})$", ErrorMessage = " پست الکترونیکی وارد شده مجاز نمیباشد")]
        public string Email { get; set; }

        [UIHint("PersianDatePicker")]
        [Display(Name = "تاریخ تولد")]
        public DateTime? BirthDate { get; set; }

        [Display(Name = "شماره موبایل")]
        [RegularExpression(@"^0?9[123]\d{8}$", ErrorMessage = "شماره موبایل را بدرستی وارد کنید")]
        [StringLength(11, ErrorMessage = "این فیلد باید حداکثر 11 کاراکتر باشد")]
        public string PhoneNumber { get; set; }
        
        [Display(Name = "جنسیت")]
        public bool Gender { get; set; }

        [Display(Name = "حاصل جمع")]
        [Required(ErrorMessage = "لطفا حاصل جمع را وارد کنید")]
        public string Captcha { get; set; }



    }
}
