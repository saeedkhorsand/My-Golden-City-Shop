using System.Text;
using System.Web.Mvc;
using DataLayer.Context;
using DomainClasses.Enums;
using GoldenCityShop.Helpers;
using GoldenCityShop.Searching;
using ServiceLayer.Interfaces;
using ViewModel.Admin.Product;

namespace GoldenCityShop.Controllers
{
    [RoutePrefix("Search-Products")]
    public partial class SearchController : Controller
    {
        #region Fields

        private readonly IUnitOfWork _unitOfWork;
        private readonly IProductService _productService;

        #endregion

        #region Constructor

        public SearchController(IUnitOfWork unitOfWork, IProductService productService)
        {
            _unitOfWork = unitOfWork;
            _productService = productService;
        }
        #endregion

        #region Search
        [OutputCache( Duration = 1, VaryByParam = "none")]
        [Route("category/{categoryId}/page/{page=1}/count/{count=12}/filter/{filter=All}/keyword/{keyword?}")]
        public virtual ActionResult Index(long categoryId, int page, int count, PSFilter filter,
            string keyword
            )
        {
            int total;
            var products = _productService.DataListSearch(out total, keyword.RemoveHtmlTags(), page, count, filter, categoryId);
            ViewBag.Counts = DropDown.GetSearchPageCount(count);
            ViewBag.Filters = DropDown.GetSearchFilters(filter);
            var model = new SearchViewModel
            {
                Products = products,
                CategoryId = categoryId,
                Term = keyword,
                Count = count,
                Filter = filter,
                Page = page,
                Total = total
            };
            return View(model);
        }
        public virtual ActionResult AutoComplete(string q)
        {
            if (string.IsNullOrWhiteSpace(q))
                Content(null);

            var result = new StringBuilder();
            var items = LuceneProducts.GetTermsScored(q);
            //foreach (var item in items)
            //{
            //    var postUrl = Url.Action(MVC.Product.ActionNames.Index, MVC.Product.Name, routeValues: new { area = "", id = item.Id, name = item.Name.GenerateSlug() }, protocol: "http");
            //    result.AppendLine(item.Name + "|" + postUrl);
            //}

            return PartialView(MVC.Search.Views._SearchPartial, items);
        }

        #endregion
    }
}
