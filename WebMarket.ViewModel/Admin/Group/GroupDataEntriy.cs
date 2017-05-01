using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebMarket.ViewModel.Admin.Group
{
    public class GroupDataEntriy
    {
        //public int Level { get; set; }
        //public long Id { get; set; }

        //[Remote("CheckExistCategoryforAdd", "Category", "Admin", ErrorMessage = "این گروه قبلا ثبت شده است", HttpMethod = "POST")]
        [Display(Name = "نام ")]
        [Required(ErrorMessage = "وارد کردن نام گروه ضرویست")]
        [MaxLength(25, ErrorMessage = "تعداد حروف نام گروه غیر مجاز است")]
        public string Name { get; set; }

        [Display(Name = "گروه پدر ")]
        public long? ParentId { get; set; }

       
    }
}
