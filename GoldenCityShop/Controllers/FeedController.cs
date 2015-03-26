
using System.Web.Mvc;
using GoldenCityShop.RSS;

namespace GoldenCityShop.Controllers
{
    public partial class FeedController : Controller
    {
        // GET: Feed
        public virtual ActionResult Product()
        {
            return new FeedResult("",null);
        }
    }
}