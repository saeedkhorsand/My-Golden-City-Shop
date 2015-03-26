using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using GoldenCityShop.Helpers;
using ServiceLayer.Interfaces;

namespace GoldenCityShop.Controllers
{
    public partial class MetaTagController : Controller
    {
        private readonly ISettingService _settingService;

        public MetaTagController(ISettingService settingService)
        {
            _settingService = settingService;
        }

        public virtual ActionResult Index( string title, string keywords, string description)
        {
            var setting = _settingService.GetOptionsForShowOnFooter();

            ViewBag.Title = !string.IsNullOrEmpty(title)
                ? SeoExtentions.GeneratePageTitle(setting.StorName, title)
                : SeoExtentions.GeneratePageTitle(setting.StorName);
            ViewBag.Keywords =!string.IsNullOrEmpty(keywords)?keywords: setting.StoreKeyWords;
            ViewBag.Description = !string.IsNullOrEmpty(description) ? description : setting.StoreKeyWords;

            return PartialView(MVC.MetaTag.Views.Index);
        }
    }
}