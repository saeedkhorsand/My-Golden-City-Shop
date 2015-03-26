using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Web.UI;
using DataLayer.Context;
using DomainClasses.Entities;
using GoldenCitShop.Filters;
using ServiceLayer.Interfaces;
using ViewModel.Admin.Folder;
using System.Threading.Tasks;

namespace GoldenCityShop.Areas.Admin.Controllers
{
    [RoutePrefix("Folder")]
    [RouteArea("Admin")]
    [Route("{action}")]
    [SiteAuthorize(Roles = "admin")]
    public partial class FolderController : Controller
    {
        #region Fields

        private readonly IPictureService _pictureService;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IFolderService _folderService;
        #endregion

        #region Constructor

        public FolderController(IUnitOfWork unitOfWork, IFolderService folderService, IPictureService pictureService)
        {
            _unitOfWork = unitOfWork;
            _folderService = folderService;
            _pictureService = pictureService;
        }
        #endregion

        #region CRUD

        [HttpGet]
        [Route("FolderList")]
        public virtual ActionResult Index(string elementId="")
        {
            ViewBag.ElementId = elementId;
            var folders = _folderService.GetList();
            if (Request.IsAjaxRequest())
                return PartialView(MVC.Admin.Shared.Views._FoldersLightBox, folders);
            return View(folders);
        }
        [HttpGet]
        [Route("Create-Folder")]
        public virtual ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("Create-Folder")]
        public virtual async Task<ActionResult> Create(AddFolderViewModel viewModel)
        {
            if (!ModelState.IsValid)
                return View(viewModel);
            if (!_folderService.Add(viewModel))
            {
                ModelState.AddModelError("Name", "این نام قبلا وارد شده است");
            }
            await _unitOfWork.SaveChangesAsync();
            return RedirectToAction(MVC.Admin.Folder.ActionNames.Create, MVC.Admin.Folder.Name);
        }
        [HttpGet]
        [Route("Edit/{id}")]
        public virtual ActionResult Edit(long? id)
        {
            if (id == null) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            var folder = _folderService.GetForEdit(id.Value);
            if (folder == null) return HttpNotFound();
            return View(folder);
        }

        [HttpPost]
        [Route("Edit/{id}")]
        [ValidateAntiForgeryToken]
        public virtual async Task<ActionResult> Edit(EditFolderViewModel viewModel)
        {
            if (!ModelState.IsValid) return View(viewModel);
            if (!_folderService.Edit(viewModel))
            {
                ModelState.AddModelError("Name", "این نام قبلا وارد شده است");
            }
            await _unitOfWork.SaveChangesAsync();
            return RedirectToAction(MVC.Admin.Folder.ActionNames.Index, MVC.Admin.Folder.Name);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        [AjaxOnly]
        public virtual async Task<ActionResult> Delete(long? id)
        {
            if (id == null) return Content(null);
            var pathes = _pictureService.GetPicturesOfFolder(id.Value);
            _folderService.Delete(id.Value);
            foreach (var path in pathes)
            {
                if (System.IO.File.Exists(Server.MapPath("~/Uploads/" + path)))
                    System.IO.File.Delete(Server.MapPath("~/Uploads/" + path));
            }
            await _unitOfWork.SaveChangesAsync();
            return Content("ok");
        }

        #endregion

        #region Picture
        [HttpGet]
        [Route("{folderId}/Pictures")]
        public virtual ActionResult Pictures(long? folderId,string elementId="")
        {
            ViewBag.ElementId = elementId;
            if (folderId == null) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            if (_folderService.GetForEdit(folderId.Value) == null) return HttpNotFound();
            ViewBag.FolderId = folderId.Value;
            if (Request.IsAjaxRequest())
                return PartialView(MVC.Admin.Shared.Views._PictureLightBox);
            return View();
        }

        [OutputCache(Location = OutputCacheLocation.None, NoStore = true)]
        public virtual ActionResult List(int page = 1, long folderId = 0)
        {
            int total;
            var pictures = _pictureService.GetAll(page, 10, out total, folderId);
            ViewBag.Total = total;
            ViewBag.PageNumber = page;
            ViewBag.FolderId = folderId;
            return PartialView(MVC.Admin.Folder.Views._ListPartial, pictures);
        }

        [HttpGet]
        [Route("{folderId}/Add-Picture")]
        public virtual ActionResult AddPictureToFolder(long? folderId)
        {
            if (folderId == null) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            if (_folderService.GetForEdit(folderId.Value) == null) return HttpNotFound();
            ViewBag.FolderId = folderId.Value;
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("{folderId}/Add-Picture")]
        public virtual async Task<ActionResult> Create(IEnumerable<HttpPostedFileBase> files, long folderId)
        {
            var validImageTypes = new string[]
            {
                "image/gif",
                "image/jpeg",
                "image/jpg",
                "image/png"
            };
            var httpPostedFileBases = files as HttpPostedFileBase[] ?? files.ToArray();
            if (!httpPostedFileBases.Any())
            {
                ModelState.AddModelError("", "مشکلی در انجام عملیات پیش آمده است");
                return View();
            }
            foreach (var file in httpPostedFileBases)
            {
                if (file != null && !validImageTypes.Contains(file.ContentType))
                {
                    ModelState.AddModelError("", " پسوند تصویر انتخاب شده غیر مجاز است");
                    return View();
                }
                if (file == null || file.ContentLength <= 0) continue;
                const string uploadDir = "~/Uploads";
                var fileName = Guid.NewGuid().ToString();
                var imagePath = Path.Combine(Server.MapPath(uploadDir), fileName);
                file.SaveAs(imagePath);
                _pictureService.Add(new Picture { Path = fileName, FolderId = folderId });
            }
            await _unitOfWork.SaveChangesAsync();
            return RedirectToAction(MVC.Admin.Folder.ActionNames.Pictures, MVC.Admin.Folder.Name,
                new { folderId = folderId });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [AjaxOnly]
        public virtual ActionResult DeletePicture(long? id)
        {
            if (id == null) return Content(null);
            var picture = _pictureService.GetById(id.Value);
            if (picture == null) return HttpNotFound();
            _pictureService.Delete(id.Value);

            if (System.IO.File.Exists(Server.MapPath("~/Uploads/" + picture.Path)))
                System.IO.File.Delete(Server.MapPath("~/Uploads/" + picture.Path));
            return Content("ok");
        }
        #endregion

        #region LightBox
        [OutputCache(Location = OutputCacheLocation.None, NoStore = true)]
        public virtual ActionResult ListForLightBox(int page = 1, long folderId = 0)
        {
            int total;
            var pictures = _pictureService.GetAll(page, 18, out total, folderId);
            ViewBag.Total = total;
            ViewBag.PageNumber = page;
            ViewBag.FolderId = folderId;
            return PartialView(MVC.Admin.Shared.Views._PicturesList, pictures);
        }

        #endregion
        [HttpPost]
        [OutputCache(Location = OutputCacheLocation.None, NoStore = true)]
        public virtual JsonResult CheckFolderNameExistForAdd(string name)
        {
            return _folderService.CheckNameExist(name) ? Json(false) :
            Json(true);
        }
        [HttpPost]
        [OutputCache(Location = OutputCacheLocation.None, NoStore = true)]
        public virtual JsonResult CheckFolderNameExistForEdit(string name, long id)
        {
            return _folderService.CheckNameExist(name, id) ? Json(false) :
            Json(true);
        }
    }
}
