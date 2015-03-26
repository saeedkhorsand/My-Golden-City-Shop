using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace GoldenCityShop.Controllers
{
    public partial class WishListController : Controller
    {
        // GET: WishList
        public virtual ActionResult Index()
        {
            return View();
        }
    }
}