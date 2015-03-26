using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace ViewModel
{
    public class LoginViewModel
    {
        [DisplayName("شماره همراه")]
        [Required(ErrorMessage = "لطفا  شماره همراه خود را وارد کنید")]
        [RegularExpression("09(1[0-9]|3[1-9]|2[1-9])-?[0-9]{3}-?[0-9]{4}", ErrorMessage = "لطفا شماره همراه خود را به شکل صحیح وارد کنید")]
        public string PhoneNumber { get; set; }
        [DisplayName("کد ورود")]
        [Required(ErrorMessage = "لطفا کد ورود خود را وارد کنید")]
        public string Password { get; set; }

        [DisplayName("مرا به خاطر بسپار")]
        public bool RememberMe { get; set; }
    }
}
