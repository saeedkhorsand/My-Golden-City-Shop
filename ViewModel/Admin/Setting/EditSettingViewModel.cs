using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViewModel.Admin
{
    public class EditSettingViewModel
    {
        [DisplayName("نام فروشگاه")]
        public string StorName { get; set; }
        [DisplayName("کلمات کلیدی")]
        [DataType(DataType.MultilineText)]
        public string StoreKeyWords { get; set; }
        [DisplayName("توضیحات سئو")]
        [DataType(DataType.MultilineText)]
        public string StoreDescription { get; set; }
        [DisplayName("شماره تماس 1")]
        public string Tel1 { get; set; }
        [DisplayName("شماره تماس 2")]
        public string Tel2 { get; set; }
        [DisplayName("آدرس فروشگاه")]
        public string Address { get; set; }
        [DisplayName("شماره موبایل 1")]
        public string PhoneNumber1 { get; set; }
        [DisplayName("شماره موبایل 1")]
        public string PhoneNumber2 { get; set; }
        [DisplayName("مدیریت نظرات")]
        public bool CommentModeratorStatus { get; set; }

    }
}
