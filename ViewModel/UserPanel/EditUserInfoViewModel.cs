using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Web;

namespace ViewModel.UserPanel
{
    public class EditUserInfoViewModel
    {
       

        [DisplayName("نام")]
        [Required(ErrorMessage = "لطفا  نام خود را وارد کنید")]
        [MaxLength(50, ErrorMessage = "تعداد حروف نام  غیر مجاز است")]
        public string FirstName { get; set; }

        [DisplayName("نام خانوادگی")]
        [MaxLength(50, ErrorMessage = "تعداد حروف نام خانوادگی  غیر مجاز است")]
        [Required(ErrorMessage = "لطفا  نام خانوادگی خود را وارد کنید")]
        public string LastName { get; set; }

        [DisplayName("کد پستی")]
        [Required(ErrorMessage = "لطفا  کد پستی خود را وارد کنید")]
        [MaxLength(12, ErrorMessage = "تعداد حروف کد پستی  غیر مجاز است")]
        public string PostalCode { get; set; }
        [DisplayName("تلفن ثابت")]
        [MaxLength(8, ErrorMessage = "تعداد حروف تلفن ثابت  غیر مجاز است")]
        public string Tel { get; set; }

        [DisplayName("آدرس دقیق")]
        [Required(ErrorMessage = "لطفا  آدرس دقیق خود را وارد کنید")]
        [DataType(DataType.MultilineText)]
        [MaxLength(2000, ErrorMessage = "تعداد حروف آدرس غیر مجاز است")]
        public string Address { get; set; }
    }
}
