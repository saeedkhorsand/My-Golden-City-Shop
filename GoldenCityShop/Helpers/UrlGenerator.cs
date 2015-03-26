using System.Web;
using System.Web.Mvc;

namespace GoldenCityShop.Helpers
{
    public static class UrlGenerator
    {
        public static MvcHtmlString ReturnUrl(this HtmlHelper htmlHelper, HttpContextBase contextBase,
             UrlHelper urlHelper)
        {
            string currentUrl = contextBase.Request.RawUrl;
            if (currentUrl == "/")
            {
                currentUrl = urlHelper.Action(MVC.Home.ActionNames.Index, MVC.Home.Name);
            }
            return MvcHtmlString.Create(currentUrl);
        }
    }
}