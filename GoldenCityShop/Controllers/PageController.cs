using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using DataLayer.Context;
using ServiceLayer.Interfaces;
using GoldenCityShop.ActionFilters;

namespace GoldenCityShop.Controllers
{
    public partial class PageController : Controller
    {
        #region Fields

        private readonly IUnitOfWork _unitOfWork;
        private readonly IPageService _pageService;
        #endregion

        #region Constructor

        public PageController(IUnitOfWork unitOfWork, IPageService pageService)
        {
            _pageService = pageService;
            _unitOfWork = unitOfWork;
        }
        #endregion

        [Route("Page/{id}/{name}")]
        public virtual ActionResult Index(long? id)
        {
            if (id == null) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            var page = _pageService.GetById(id.Value);
            if (page == null) return HttpNotFound();
            return View(page);
        }

        // GET: Page/Details/5
        public virtual ActionResult Details(int id)
        {
            return View();
        }

        // GET: Page/Create
        public virtual ActionResult Create()
        {
            return View();
        }

        // POST: Page/Create
        [HttpPost]
        public virtual ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Page/Edit/5
        public virtual ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Page/Edit/5
        [HttpPost]
        public virtual ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Page/Delete/5
        public virtual ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Page/Delete/5
        [HttpPost]
        public virtual ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
