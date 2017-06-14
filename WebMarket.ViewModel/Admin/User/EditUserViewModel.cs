using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebMarket.ViewModel.Admin.User
{
    public class EditUserViewModel
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

       
        [Display(Name = "نام کاربری")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "نام کاربری را وارد کنید")]
        [StringLength(50, ErrorMessage = "تعداد کاراکتر ها می بایست حداکثر 50 کاراکتر باشد")]
        [RegularExpression("^[a-zA-Z0-9_]*$", ErrorMessage = "لطفا فقط از حروف انگلیسی و اعدد استفاده کنید")]
        public string UserName { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "کلمه عبور خود را وارد کنید")]
        [StringLength(100, ErrorMessage = "کلمه عبور باید حداقل 6 حرف باشد", MinimumLength = 6)]
        [Display(Name = "رمز عبور")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "کلمه عبور خود را وارد کنید")]
        [Display(Name = "تکرار کلمه عبور")]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "تکرار کلمه عبور، با کلمه عبور یکسان نیست")]
        public string ConfirmPassword { get; set; }


        [UIHint("PersianDatePicker")]
        [Display(Name = "تاریخ تولد")]
        public DateTime? BirthDate { get; set; }

        [Display(Name = "شماره موبایل")]
        [RegularExpression(@"^0?9[123]\d{8}$", ErrorMessage = "شماره موبایل را بدرستی وارد کنید")]
        [StringLength(11, ErrorMessage = "این فیلد باید حداکثر 11 کاراکتر باشد")]
        public string PhoneNumber { get; set; }
       
        [Display(Name = "جنسیت")]
        public bool Gender { get; set; }
       
        public IEnumerable<System.Web.Mvc.SelectListItem> RolesList { get; set; }
    }
}
