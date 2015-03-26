using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using System.Web.UI;
using DataLayer.Context;
using GoldenCitShop.Filters;
using GoldenCityShop.Extentions;
using GoldenCityShop.Searching;
using ServiceLayer.EFServices;
using ServiceLayer.Interfaces;
using ViewModel.Admin.Product;

namespace GoldenCityShop.Controllers
{
    [RoutePrefix("Product")]
    public partial class ProductController : Controller
    {
        #region Fileds
        private const string TotalInCompareCookieName = "totalInCompare";
        private const string CompareListCookieName = "compareList";
        private readonly IProductService _productService;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IUserService _userService;
        private readonly IValueService _valueService;
        #endregion

        #region Constructor

        public ProductController(IUnitOfWork unitOfWork, IUserService userService, IProductService productService,IValueService valueService)
        {
            _unitOfWork = unitOfWork;
            _productService = productService;
            _userService = userService;
            _valueService = valueService;
        }

        #endregion

        #region Rating
        [HttpPost]
        [AjaxOnly]
        [SiteAuthorize]
        [OutputCache(Location = OutputCacheLocation.None, NoStore = true)]
        public virtual async Task<ActionResult> SaveRatings(long? productId, double? value, string sectionType)
        {
            if (productId == null || value == null || string.IsNullOrWhiteSpace(sectionType))
                return Content(null);

            if (!_productService.CanUserRate(productId.Value, User.Identity.Name))
                return Content("nok");
            switch (sectionType)
            {
                case "Product":
                    _productService.SaveRating(productId.Value, value.Value);
                    var user = _userService.GetUserByUserName(User.Identity.Name);
                    _productService.AddUserToLikedUsers(productId.Value, user);
                    break;

                //... سایر قسمت‌های دیگر سایت

                default:
                    return Content(null);
            }
            await _unitOfWork.SaveAllChangesAsync(false);
            return Content("ok");
        }


        #endregion

        #region WishList(2)

        [HttpPost]
        [AjaxOnly]
        [SiteAuthorize]
        [OutputCache(Location = OutputCacheLocation.None, NoStore = true)]
        public virtual async Task<ActionResult> AddToWishList(long? productId)
        {
            if (productId == null)
                return Content(null);

            if (_userService.LimitAddToWishList(User.Identity.Name))
                return Content("limit");

            if (!_productService.CanUserAddToWishList(productId.Value, User.Identity.Name))
                return Content("nok");

            var user = _userService.GetUserByUserName(User.Identity.Name);
            _productService.AddUserToWishList(productId.Value, user);
            await _unitOfWork.SaveAllChangesAsync(false);

            return Content("ok");
        }

        [HttpPost]
        [AjaxOnly]
        [SiteAuthorize]
        [OutputCache(Location = OutputCacheLocation.None, NoStore = true)]
        public virtual async Task<ActionResult> RemoveFromWishList(long? id)
        {
            if (id == null) return Content(null);
            _productService.RemoveFromWishList(id.Value, _userService.Find(User.Identity.Name));
            await _unitOfWork.SaveAllChangesAsync(false);
            return Content("ok");
        }

        #endregion

        #region CompareList(1)

        [HttpPost]
        [AjaxOnly]
        [SiteAuthorize]
        [OutputCache(Location = OutputCacheLocation.None, NoStore = true)]
        public virtual ActionResult AddToCompareList(long? productId)
        {
            if (productId == null)
                return Content(null);
            var itemsInCookie = new List<ProductCompareViewModel>();
            var cookieValue = HttpContext.GetCookieValue(TotalInCompareCookieName);
            var count = string.IsNullOrEmpty(cookieValue) ? 0 : int.Parse(cookieValue);
            if (count > 5)
                return Content("limit");
            var product = _productService.GetForAddToCompare(productId.Value);

            if (count == 0)
                HttpContext.AddCookie(TotalInCompareCookieName, (++count).ToString(), DateTime.Now.AddDays(1));
            else
            {
                itemsInCookie =
                      new JavaScriptSerializer().Deserialize<List<ProductCompareViewModel>>(
                          HttpContext.GetCookieValue(CompareListCookieName));
                if (itemsInCookie.Any(a => a.ProductId == productId.Value))
                    return Content("nok");
                if (itemsInCookie.Count > 4)
                    return Content("limit");
                HttpContext.UpdateCookie(TotalInCompareCookieName, (++count).ToString());
            }
            itemsInCookie.Add(product);
            HttpContext.UpdateCookie(CompareListCookieName, itemsInCookie.ToJson());
            return Content("ok");
        }

        public virtual ActionResult ShowCompareList()
        {
            return View();
        }
        #endregion

        #region ProductDetails
          public virtual ActionResult GetSelections(long? categoryId)
        {
            if (categoryId == null) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            ViewBag.CategoryId = categoryId.Value;
            var products = _productService.GetSelecionProductOfCategory(categoryId.Value);
            return PartialView(MVC.Product.Views._SelactionProductsPartial,products);
        }
        
        public virtual ActionResult GetProperties(long? id)
        {
            if (id == null) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            var properties = _valueService.GetProductProperties(id.Value);
            return PartialView(MVC.Product.Views._AttributesPartial, properties);
        }
        public virtual ActionResult GetRelatedProducts(long? id)
        {
            if (id == null) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            var products = LuceneProducts.ShowMoreLikeThisPostItems(id.Value);
            if (products != null) products = products.Where(a => a.Id != id.Value).ToList();
            return PartialView(MVC.Product.Views._RelatedProductsPartial, products);
        }
        [Route("{id}/{name}")]
        public virtual ActionResult Index(long? id, string name)
        {
            if(id==null)return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            var product = _productService.GetForShowDetails(id.Value);
            if (product == null) return HttpNotFound();
            _unitOfWork.SaveAllChangesAsync(false);
            return View(product);
        }

        #endregion
      

    }
}