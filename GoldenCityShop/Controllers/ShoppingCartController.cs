using System;
using System.Globalization;
using System.Threading.Tasks;
using System.Web.Mvc;
using System.Web.UI;
using DataLayer.Context;
using DomainClasses.Entities;
using GoldenCitShop.Filters;
using GoldenCityShop.Extentions;
using ServiceLayer.Interfaces;

namespace GoldenCityShop.Controllers
{
    public partial class ShoppingCartController : Controller
    {
        #region Fileds
        private const string TotalInCartCookieName = "totalInCart";
        private readonly IShoppingCartService _shoppingCartService;
        private readonly IProductService _productService;
        private readonly IUnitOfWork _unitOfWork;
        #endregion

        #region Constructor
        public ShoppingCartController(IUnitOfWork unitOfWork, IProductService productService, IShoppingCartService shoppingCartService)
        {
            _unitOfWork = unitOfWork;
            _productService = productService;
            _shoppingCartService = shoppingCartService;
        }

        #endregion

        #region Add
        [HttpPost]
        [AjaxOnly]
        [SiteAuthorize]
        [OutputCache(Location = OutputCacheLocation.None, NoStore = true)]
        public virtual async Task<ActionResult> AddToCart(long? productId, decimal? value)
        {
            if (productId == null)
                return Content(null);
            var product = _productService.GetById(productId.Value);
            if (product == null) return Content(null);
            var count = value ?? product.Ratio;
            var result = decimal.Remainder(count, product.Ratio);
            if (result!= decimal.Zero)
                return Content(null);

            if (!_productService.CanAddToShoppingCart(productId.Value,count))
                return Content("nok");

            var cartItem = _shoppingCartService.GetCartItem(productId.Value, User.Identity.Name);

             product.Reserve += count;

            if (cartItem == null)
            {
                _shoppingCartService.Add(new ShoppingCart
                {
                    CartNumber = User.Identity.Name,
                    Quantity = count,
                    ProductId = productId.Value,
                    CreateDate = DateTime.Now
                });
            }
            else
            {
                cartItem.Quantity += count;
            }

            await _unitOfWork.SaveAllChangesAsync(false);

            var totalValueInCart = _shoppingCartService.TotalValueInCart(User.Identity.Name);
            if (string.IsNullOrEmpty(HttpContext.GetCookieValue(TotalInCartCookieName)))
                HttpContext.AddCookie(TotalInCartCookieName, totalValueInCart.ToString(CultureInfo.InvariantCulture), DateTime.Now.AddDays(1));
            else HttpContext.UpdateCookie(TotalInCartCookieName, totalValueInCart.ToString(CultureInfo.InvariantCulture));

            return Content("ok");
        }
        #endregion

        #region RemoveItem
        [HttpPost]
        [AjaxOnly]
        [SiteAuthorize]
        [OutputCache(Location = OutputCacheLocation.None, NoStore = true)]
        public virtual async Task<ActionResult> RemoveFromCart(long? productId)
        {
            if (productId == null) return Content(null);
            var value = _shoppingCartService.DeleteItem(productId.Value, User.Identity.Name);
            _productService.DecreaseReserve(productId.Value, value);
            await _unitOfWork.SaveAllChangesAsync(false);

            var valueforCookie = HttpContext.GetCookieValue(TotalInCartCookieName);
            var total = string.IsNullOrEmpty(valueforCookie) ? 0 : int.Parse(valueforCookie) - value;
            if (valueforCookie != null) HttpContext.UpdateCookie(TotalInCartCookieName,total.ToString(CultureInfo.InvariantCulture));
            return Content("ok");
        }

        #endregion

        #region UpdateCart
        [HttpPost]
        [AjaxOnly]
        [SiteAuthorize]
        [OutputCache(Location = OutputCacheLocation.None, NoStore = true)]
        public virtual async Task<ActionResult> UpdateCart()
        {
            await _unitOfWork.SaveAllChangesAsync(false);
            return Content("ok");
        }
        #endregion

        #region EmptyCart
        [HttpPost]
        [AjaxOnly]
        [SiteAuthorize]
        [OutputCache(Location = OutputCacheLocation.None, NoStore = true)]
        public virtual async Task<ActionResult> SetEmptyCart()
        {
            await _shoppingCartService.Delete(User.Identity.Name);
            await _unitOfWork.SaveAllChangesAsync(false);
            return Content("ok");
        }
        #endregion

        #region show
        //[HttpPost]
        //[AjaxOnly]
        //[SiteAuthorize]
        //[OutputCache(Location = OutputCacheLocation.None, NoStore = true)]
        //public async Task<ActionResult> PartialShowCart()
        //{
        //    var list= _shoppingCartService.List(User.Identity.Name);
        //    await _unitOfWork.SaveChangesAsync();
        //    return PartialView("",list);
        //}

        #endregion

    }
}