using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.WebControls.WebParts;
using GoldenCitShop.Filters;

namespace GoldenCityShop.Areas.Admin.Controllers
{
    [RouteArea("Admin")]
    [Route("{action}")]
    [SiteAuthorize(Roles = "admin")]
    public partial class HomeController : Controller
    {
        [Route("Home")]
        public virtual ActionResult Index()
        {
            return View();
        }

      
        public virtual ActionResult SideBar()
        {
            return PartialView(MVC.Admin.Home.Views._SideBarPartial);
        }

        // GET: Admin/Home/Create
        public virtual ActionResult Create()
        {
            return View();
        }

        // POST: Admin/Home/Create
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

        // GET: Admin/Home/Edit/5
        public virtual ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Admin/Home/Edit/5
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

        // GET: Admin/Home/Delete/5
        public virtual ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Admin/Home/Delete/5
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
