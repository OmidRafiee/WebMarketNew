using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebMarket.ViewModel.Admin.Setting
{
    public class SlideShowDataEntry
    {
        public int Id { get; set; }

        [Display(Name=" عنوان بزرگ")]
        [MaxLength(30, ErrorMessage = "تعداد حروف عنوان غیر مجاز است")]
        [Required(ErrorMessage = "عنوان نباید خالی باشد")]
        public string BigTitle { get; set; }

        [Display(Name = "عنوان کوچک")]
        [MaxLength(80, ErrorMessage = "تعداد حروف عنوان غیر مجاز است")]
        public string SmallTitle { get; set; }

        [Display(Name = "توضیحات")]
        [MaxLength(300, ErrorMessage = "تعداد حروف توضیحات غیر مجاز است")]
        [Required(ErrorMessage = "توضیحات نباید خالی باشد")]
        [DataType(DataType.MultilineText)]
        public string Description { get; set; }

        [Display(Name = "لینک")]
        [Required(ErrorMessage = "لینک نباید خالی باشد")]
        public string Url { get; set; }


        [Display(Name = "آدرس تصویر")]
        //[Required(ErrorMessage = "آدرس تصویر نباید خالی باشد")]
        public string Image { get; set; }

       
       

    }
}
