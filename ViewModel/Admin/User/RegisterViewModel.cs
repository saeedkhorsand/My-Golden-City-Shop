using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace ViewModel
{
    public class RegisterViewModel
    {
        [DisplayName("نام نمایشی")]
        [MinLength(5,ErrorMessage = "نام نمایشی شما باید بیشتر از 5 حرف باشد")]
        [Required(ErrorMessage = "لطفا نام نمایشی خود را وارد کنید")]
        [MaxLength(30, ErrorMessage = "نام نمایشی نمیتواند بیشتر از 30 کاراکتر باشد")]
        [Remote("CheckUserNameIsExist", "User","", ErrorMessage = "این نام کاربری قبلا توسط اعضا انتخاب شده است",HttpMethod = "POST")]
        public string UserName { get; set; }
        [DisplayName("شماره همراه")]
        [Required(ErrorMessage = "لطفا برای تکمیل ثبت نام شماره همراه خود را وارد کنید")]
        [Remote("CheckPhoneNumberIsExist", "User","", ErrorMessage = "این شماره همراه قبلا ثبت شده است", HttpMethod = "POST")]
        [RegularExpression("09(1[0-9]|3[1-9]|2[1-9])-?[0-9]{3}-?[0-9]{4}", ErrorMessage = "لطفا شماره همراه خود را به شکل صحیح وارد کنید")]
        public string PhoneNumber { get; set; }
    }
}
