using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using DataAnnotationsExtensions;

namespace ViewModel.Admin
{
    public class EditCategoryViewModel
    {
        public long Id { get; set; }
        [DisplayName("نام")]
        [Required(ErrorMessage = "وارد کردن نام گروه ضرویست")]
        [MaxLength(25, ErrorMessage = "تعداد حروف نام گروه غیر مجاز است")]
        [Remote("CheckExistCategoryforEdit", "Category", "Admin", ErrorMessage = "این گروه قبلا ثبت شده است", HttpMethod = "POST", AdditionalFields = "Id")]
        public string Name { get; set; }
        [DisplayName("ترتیب نمایش")]
        [Required(ErrorMessage = "ترتیب نمایش گروه را مشخص کنید")]
        public int DisplayOrder { get; set; }
       
        [DisplayName("توضیحات برای سئو")]
        [MaxLength(400, ErrorMessage = "تعداد حروف توضیحات غیر مجاز است")]
        [Required(ErrorMessage = "وارد کردن توضیحات ضروریست")]
        public string Description { get; set; }
        [DisplayName("کلمات کلیدی")]
        [MaxLength(100, ErrorMessage = "تعداد حروف کلمات کلیدی غیر مجاز است")]
        [Required(ErrorMessage = "وارد کردن کلمات کلیدی ضروریست")]
        public string KeyWords { get; set; }
        [DisplayName("درصد تخفیف")]
        [Required(ErrorMessage = "درصد تخفیف را مشخص کنید")]
        [Range(0, 100, ErrorMessage = "درصد تخفیف میبایست 0 تا 100 انتخاب شود")]
        [Integer(ErrorMessage = "فقط ازاعداد صحیح استفاده کنید")]
        public int DiscountPercent { get; set; }
        public int Level { get; set; }
    }
}
