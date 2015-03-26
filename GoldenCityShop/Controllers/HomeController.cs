using System.Web.Mvc;
using DataLayer.Context;
using DomainClasses.Enums;
using ServiceLayer.Interfaces;
using ViewModel.Admin.Category;

namespace GoldenCityShop.Controllers
{  
    public partial class HomeController : Controller
    {
        #region Fields
        private const int oneDay = 86400;
        private const int hour12 = 43200;
        private const int hour1 = 3600;
        private const int min15 = 900;
        private const int min10 = 600;
        private readonly IProductService _productService;
        private readonly ISlideShowService _slideShowService;
        private readonly IPageService _pageService;
        private readonly IUnitOfWork _unitOfWork;
        private readonly ISettingService _settingService;
        private readonly ICategoryService _categoryService;
        #endregion

        #region Constructor

        public HomeController(IUnitOfWork unitOfWork, IProductService productService, ICategoryService categoryService,
            IPageService pageService, ISlideShowService slideShowService, ISettingService settingService)
        {
            _unitOfWork = unitOfWork;
            _categoryService = categoryService;
            _pageService = pageService;
            _slideShowService = slideShowService;
            _productService = productService;
            _settingService = settingService;
        }

        #endregion

        #region Index
        public virtual ActionResult Index()
        {
            return View(); //نمايش صفحه اوليه
        }
        #endregion

        #region Menus
        [ChildActionOnly]
        public virtual ActionResult NavBar()
        {
            return PartialView(MVC.Home.Views._NavPartial);
        }
        [ChildActionOnly]
        //[OutputCache(Duration = oneDay, VaryByParam = "none")]
        public virtual ActionResult SlideShow()
        {
            var slides = _slideShowService.List();
            return PartialView(MVC.Home.Views._SlideShow, slides);
        }

        //[OutputCache(Duration = oneDay, VaryByParam = "none")]
        [ChildActionOnly]
        public virtual ActionResult Menu()
        {
            var categories = _categoryService.GetCategoriesForMenu();
            return PartialView(MVC.Home.Views._MenuPartial, categories);
        }
        //[OutputCache(Duration = oneDay, VaryByParam = "none")]
        [ChildActionOnly]
        public virtual ActionResult ContentPages()
        {
            var pages = _pageService.GetByShowPlace(PageShowPlace.Body);
            return PartialView(MVC.Home.Views._ContentPagesMenuPartial, pages);
        }
        [ChildActionOnly]
        //[OutputCache(Duration = oneDay, VaryByParam = "none")]
        public virtual ActionResult Footer()
        {
            var pages = _pageService.GetByShowPlace(PageShowPlace.Footer);
            var model = new FooterViewModel
            {
                Pages = pages,
                EditSettingViewModel = _settingService.GetOptionsForShowOnFooter()
            };
            return PartialView(MVC.Home.Views._FooterPartial, model);
        }
        #endregion

        #region ProductsShow
      //  [OutputCache(Duration = min15, VaryByParam = "none")]
        [ChildActionOnly]
        public virtual ActionResult BelovedProducts()
        {
            ViewBag.Teaser = true;
            ViewBag.ProductIcon = "glyphicon glyphicon-heart";
            ViewBag.SectionIcon = "glyphicon glyphicon-heart";
            ViewBag.SectionName = "محصولات محبوب";
            ViewBag.SectionFilter = PSFilter.Beloved;
            var products = _productService.GetBelovedProducts();
            return PartialView(MVC.Home.Views._ProductsSections, products);
        }
        //[OutputCache(Duration = min15, VaryByParam = "none")]
        [ChildActionOnly]
        public virtual ActionResult MoreViewedProducts()
        {
            ViewBag.SectionFilter = PSFilter.MoreView;
            ViewBag.Teaser = true;
            ViewBag.ProductIcon = "glyphicon glyphicon-heart-empty";
            ViewBag.SectionIcon = "glyphicon glyphicon-eye-open";
            ViewBag.SectionName = "محصولات پر بازدید";
            var products = _productService.GetMoreViewedProducts();
            return PartialView(MVC.Home.Views._ProductsSections, products);
        }

        //[OutputCache(Duration = min15, VaryByParam = "none")]
        [ChildActionOnly]
        public virtual ActionResult NewProducts()
        {
            ViewBag.SectionFilter = PSFilter.New;
            ViewBag.Teaser = false;
            ViewBag.ProductIcon = "glyphicon glyphicon-heart-empty";
            ViewBag.SectionIcon = "fa fa-bullhorn";
            ViewBag.SectionName = "محصولات جدید";
            var products = _productService.GetNewProducts();
            return PartialView(MVC.Home.Views._ProductsSections, products);
        }

        //[OutputCache(Duration = min15, VaryByParam = "none")]
        [ChildActionOnly]
        public virtual ActionResult MoreSellProducts()
        {
            var products = _productService.GetMoreSellProducts();
            return PartialView(MVC.Home.Views._MoreSellProducts, products);
        }
        #endregion
    }
}