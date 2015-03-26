using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using DataLayer.Context;
using DomainClasses.Entities;
using GoldenCitShop.Filters;
using GoldenCityShop.Extentions;
using GoldenCityShop.Helpers;
using ServiceLayer.Interfaces;
using StructureMap.Building;
using ViewModel.Admin;
using ViewModel.Admin.Setting;
using System.Web.UI;

namespace GoldenCityShop.Areas.Admin.Controllers
{
    [RouteArea("Admin")]
    [RoutePrefix("Setting")]
    [Route("{action}")]
    [SiteAuthorize(Roles = "admin")]
    public partial class SettingController : Controller
    {
        #region Fields

        private readonly ISlideShowService _slideShowService;
        private readonly IUnitOfWork _unitOfWork;
        private readonly ISettingService _settingService;
        #endregion

        #region Constructor

        public SettingController(IUnitOfWork unitOfWork, ISettingService settingService, ISlideShowService slideShowService)
        {
            _unitOfWork = unitOfWork;
            _settingService = settingService;
            _slideShowService = slideShowService;
        }
        #endregion

        #region Edit

        [Route("Edit")]
        [HttpGet]
        public virtual ActionResult Edit()
        {
            var model = _settingService.GetOptionsForEdit();
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("Edit")]
        public virtual async Task<ActionResult> Edit(EditSettingViewModel viewModel)
        {
            _settingService.Update(viewModel);
            await _unitOfWork.SaveChangesAsync();
            return RedirectToAction(MVC.Admin.Home.ActionNames.Index, MVC.Admin.Home.Name);
        }

        #endregion

        #region SlideShow
        [Route("Slides-List")]
        public virtual ActionResult ListSlides()
        {
            var slides = _slideShowService.List();
            return View(slides);
        }
        [HttpGet]
        [Route("Add-Slide-show")]
        public virtual ActionResult AddSlide()
        {
            if (!_slideShowService.AllowAdd())
            {
                ViewBag.AllowAddSlide = "nok";
                return View();
            }

            PopulateCategoriesDropDownList(null, null, null);
            return View();

        }
        [HttpPost]
        [Route("Add-Slide-show")]
        [ValidateAntiForgeryToken]
        public virtual async Task<ActionResult> AddSlide(AddSlideShowViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                _slideShowService.Add(viewModel);

                await _unitOfWork.SaveChangesAsync();
                CacheManager.InvalidateChildActionsCache();
                return RedirectToAction(MVC.Admin.Setting.ActionNames.AddSlide, MVC.Admin.Setting.Name);
            }
            PopulateCategoriesDropDownList(viewModel.ShowTransition, viewModel.HideTransition, viewModel.Position);
            return View(viewModel);

        }

        [HttpGet]
        [Route("Edit-Slide-show/{id}")]
        public virtual ActionResult EditSlide(long? id)
        {
            if (id == null) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            var slide = _slideShowService.GetByIdForEdit(id.Value);
            if (slide == null)
                return HttpNotFound();
            PopulateCategoriesDropDownList(slide.ShowTransition, slide.HideTransition, slide.Position);
            return View(slide);
        }
        [HttpPost]
        [Route("Edit-Slide-show/{id}")]
        [ValidateAntiForgeryToken]
        public virtual async Task<ActionResult> EditSlide(EditSlideShowViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                _slideShowService.Update(viewModel);

                await _unitOfWork.SaveChangesAsync();
                CacheManager.InvalidateChildActionsCache();
                return RedirectToAction(MVC.Admin.Setting.ActionNames.ListSlides, MVC.Admin.Setting.Name);
            }
            PopulateCategoriesDropDownList(viewModel.ShowTransition, viewModel.HideTransition, viewModel.Position);
            return View(viewModel);
        }

        [AjaxOnly]
        [HttpPost]
        [OutputCache(Location = OutputCacheLocation.None, NoStore = true)]
        [Route("Delete-Slide-show")]
        public virtual async Task<ActionResult> DeleteSlide(long? id)
        {
            if (id == null) return Content(null);
            _slideShowService.Delete(id.Value);
            await _unitOfWork.SaveChangesAsync();
            CacheManager.InvalidateChildActionsCache();
            return Content("ok");
        }


        void PopulateCategoriesDropDownList(string showTranstion, string hideTranstion, string position)
        {
            ViewBag.ShowTransitionList = DropDown.GetShowTranstionSlide(showTranstion);
            ViewBag.HideTransitionList = DropDown.GetHideTranstionSlide(hideTranstion);
            ViewBag.PositionList = DropDown.GetPositonSlide(position);
        }
        #endregion


    }
}
