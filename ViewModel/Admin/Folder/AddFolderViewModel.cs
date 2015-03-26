using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace ViewModel.Admin.Folder
{
    public class AddFolderViewModel
    {
        [Required(ErrorMessage = "نام فولدر را وارد کنید")]
        [MaxLength(50,ErrorMessage = "تعداد حروف نام فولدر غیر مجاز است")]
        [Remote("CheckFolderNameExistForAdd", "Folder", "Admin", ErrorMessage = "این نام قبلا ثبت شده است", HttpMethod = "POST")]
        [DisplayName("نام فولدر")]
        public string Name { get; set; }
    }
}
