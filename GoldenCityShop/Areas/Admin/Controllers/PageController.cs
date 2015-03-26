using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using DataLayer.Context;
using GoldenCitShop.Filters;
using GoldenCityShop.Extentions;
using GoldenCityShop.HtmlCleaner;
using ServiceLayer.Interfaces;
using ViewModel.Admin.Page;
using System.Threading.Tasks;

namespace GoldenCityShop.Areas.Admin.Controllers
{
    [RouteArea("Admin")]
    [RoutePrefix("Page")]
    [Route("{action}")]
    [SiteAuthorize(Roles = "admin")]
    public partial class PageController : Controller
    {
        #region Fields

        private readonly IUnitOfWork _unitOfWork;
        private readonly IPageService _pageService;
        #endregion

        #region Constructor
        public PageController(IUnitOfWork unitOfWork, IPageService pageService)
        {
            _unitOfWork = unitOfWork;
            _pageService = pageService;
        }
        #endregion

        #region List,Index
        [HttpGet]
        public virtual ActionResult Index()
        {
            return View();
        }

        public virtual ActionResult List(string term="")
        {
            ViewBag.Term = term;
            var pages = _pageService.GetDataTable(term);
            return PartialView(MVC.Admin.Page.Views._ListPartial,pages);
        }
        #endregion

        #region Create
        [HttpGet]
        public virtual ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public virtual async Task<ActionResult> Create(AddPageViewModel viewModel)
        {
            if (!ModelState.IsValid)
                return View(viewModel);
            viewModel.Content = viewModel.Content.ToSafeHtml();
            if (_pageService.Add(viewModel))
            {
                await _unitOfWork.SaveChangesAsync();
                CacheManager.InvalidateChildActionsCache();
                return RedirectToAction(MVC.Admin.Page.ActionNames.Create, MVC.Admin.Page.Name);
            }
            ModelState.AddModelError("Title", "چنین صفحه ای قبلا ثبت شده است");
            return View(viewModel);
        }
        #endregion

        #region Edit
        [HttpGet]
        [Route("Edit/{id}")]
        public virtual ActionResult Edit(long? id)
        {
            if (id == null) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            var page = _pageService.GetForEditById(id.Value);
            if (page == null)
                return HttpNotFound();
            return View(page);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("Edit/{id}")]
        [ValidateInput(false)]
        public virtual async Task<ActionResult> Edit(EditPageViewModel viewModel)
        {
            if (!ModelState.IsValid)
                return View(viewModel);
            viewModel.Content = viewModel.Content.ToSafeHtml();
            if (_pageService.Edit(viewModel))
            {
                await _unitOfWork.SaveChangesAsync();
                CacheManager.InvalidateChildActionsCache();
                return RedirectToAction(MVC.Admin.Page.ActionNames.Index, MVC.Admin.Page.Name);
            }
            ModelState.AddModelError("Title", "چنین صفحه ای قبلا ثبت شده است");
            return View(viewModel);
        }
        #endregion

        #region Delete
        [HttpPost]
        [ValidateAntiForgeryToken]
        [AjaxOnly]
        public virtual async Task<ActionResult> Delete(long? id)
        {
            if (id == null) return Content(null);
            _pageService.Delete(id.Value);
          await  _unitOfWork.SaveChangesAsync();
          CacheManager.InvalidateChildActionsCache();
            return Content("ok");
        }
        #endregion
    }
}
