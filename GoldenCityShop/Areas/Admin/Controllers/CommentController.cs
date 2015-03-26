using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using GoldenCitShop.Filters;

namespace GoldenCityShop.Areas.Admin.Controllers
{
     [SiteAuthorize(Roles = "admin")]
    public partial class CommentController : Controller
    {
        // GET: Admin/Comment
        public virtual ActionResult Index()
        {
            return View();
        }

        // GET: Admin/Comment/Details/5
        public virtual ActionResult Details(int id)
        {
            return View();
        }

        // GET: Admin/Comment/Create
        public virtual ActionResult Create()
        {
            return View();
        }

        // POST: Admin/Comment/Create
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

        // GET: Admin/Comment/Edit/5
        public virtual ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Admin/Comment/Edit/5
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

        // GET: Admin/Comment/Delete/5
        public virtual ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Admin/Comment/Delete/5
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
