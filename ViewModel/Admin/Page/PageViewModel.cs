using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using DomainClasses.Enums;

namespace ViewModel.Admin.Page
{
    public class PageViewModel
    {
        public long Id { get; set; }
        [DisplayName("محتوای صفحه")]
        [AllowHtml]
        public string Content { get; set; }
        [DisplayName("مکان نمایش")]
        public string ShowPlace { get; set; }
        [DisplayName("عنوان صفحه")]
        public string Title { get; set; }
        [DisplayName("تصویر لینک")]
        public string LinkImage { get; set; }
    }
}
