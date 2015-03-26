using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web.Mvc;
using System.Web.UI;
using DataLayer.Context;
using DomainClasses.Entities;
using DomainClasses.Enums;
using GoldenCitShop.Filters;
using GoldenCityShop.Extentions;
using ServiceLayer.Interfaces;
using ViewModel.Admin;

namespace GoldenCityShop.Areas.Admin.Controllers
{
    [RouteArea("Admin")]
    [RoutePrefix("Category")]
    [Route("{action}")]
    [SiteAuthorize(Roles = "admin")]
    public partial class CategoryController : Controller
    {
        #region Fields

        private readonly IAttributeService _attributeService;
        private readonly IUnitOfWork _unitOfWork;
        private readonly ICategoryService _categoryService;
        #endregion

        #region Constructor
        public CategoryController(IUnitOfWork unitOfWork, ICategoryService categoryService, IAttributeService attributeService)
        {
            _unitOfWork = unitOfWork;
            _categoryService = categoryService;
            _attributeService = attributeService;
        }
        #endregion

        #region Create
        [HttpGet]
        public virtual ActionResult Create()
        {
           PopulateCategoriesDropDownList(_categoryService.GetFirstLevelCategories());
            return View();
        }

        // POST: Admin/Category/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public virtual async Task<ActionResult> Create(AddCategoryViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                var category = new Category
                {
                    Description = viewModel.Description,
                    ParentId = viewModel.ParentId == 0 ? null : viewModel.ParentId,
                    KeyWords = viewModel.KeyWords,
                    Name = viewModel.Name,
                    DisplayOrder = viewModel.DisplayOrder,
                    DiscountPercent = viewModel.DiscountPercent
                };
                _attributeService.AddParentAttributeToChild(category.ParentId, category.Id);

                var status = _categoryService.Add(category);
                if (status == AddCategoryStatus.CategoryNameExist)
                {
                    ModelState.AddModelError("Name", "این نام قبلا ثبت شده است");
                    return View(viewModel);
                }
                await _unitOfWork.SaveChangesAsync();
                CacheManager.InvalidateChildActionsCache();
                return RedirectToAction(MVC.Admin.Category.ActionNames.Create, MVC.Admin.Category.Name);
            }

            PopulateCategoriesDropDownList(_categoryService.GetFirstLevelCategories(), viewModel.ParentId);
            return View(viewModel);
        }

        #endregion

        #region Edit
        [HttpGet]
        [Route("Edit/{id}")]
        public virtual ActionResult Edit(long? id)
        {
            if (id == null) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            var category = _categoryService.GetForEdit(id.Value);
            if (category == null) return HttpNotFound();
            CacheManager.InvalidateChildActionsCache();
            return View(category);
        }

        [HttpPost]
        [Route("Edit/{id}")]
        [ValidateAntiForgeryToken]
        public virtual async Task<ActionResult> Edit(EditCategoryViewModel viewModel)
        {
            if (!ModelState.IsValid)
                return View(viewModel);

            var status = _categoryService.Edit(viewModel);
            if (status == EditCategoryStatus.CategoryNameExist)
            {
                ModelState.AddModelError("Name", "این نام قبلا ثبت شده است");
                return View(viewModel);
            }
            await _unitOfWork.SaveChangesAsync();
            CacheManager.InvalidateChildActionsCache();
            return RedirectToAction(MVC.Admin.Category.ActionNames.Index, MVC.Admin.Category.Name);
        }
        #endregion

        #region Index , List
        [HttpGet]
        public virtual ActionResult Index()
        {
            return View();
        }

        [OutputCache(Location = OutputCacheLocation.None, NoStore = true)]
        public virtual ActionResult List(string term = "", int page = 1)
        {
            int total;
            var categories = _categoryService.GetDataTable(out total, term, page, 10);
            ViewBag.TotalCategories = total;
            ViewBag.PageNumber = page;
            return PartialView(MVC.Admin.Category.Views._ListPartial, categories);
        }
        #endregion

        #region Delete

        [HttpPost]
        [ValidateAntiForgeryToken]
        [AjaxOnly]
        public virtual async Task<ActionResult> Delete(long? id)
        {
            if (id == null) return Content(null);
            _categoryService.Delete(id.Value);
            await _unitOfWork.SaveChangesAsync();
            CacheManager.InvalidateChildActionsCache();
            return Content("ok");
        }
        #endregion

        #region Attributes

        #region list
        [HttpGet]
        [Route("{categoryId}/Attributes")]
        public virtual ActionResult AttributeList(long? categoryId)
        {

            if (categoryId == null) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            var category = _categoryService.GetById(categoryId.Value);
            if (category == null) return HttpNotFound();
            var attributes = _attributeService.GetAttributesByCategoryId(categoryId.Value);
            ViewBag.CategoryId = categoryId;
            ViewBag.CategoryName = category.Name;
            return View(attributes);
        }
        #endregion

        #region Edit
        [Route("Attribute/Edit/{id}")]
        [HttpGet]
        public virtual ActionResult EditAttribute(long? id)
        {
            if (id == null) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            var attribute = _attributeService.GetAttributeById(id.Value);
            if (attribute == null) return HttpNotFound();

            return View(new EditAttributeViewModel { Name = attribute.Name, CategoryId = attribute.CategoryId, Id = attribute.Id });
        }
        [Route("Attribute/Edit/{id}")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public virtual async Task<ActionResult> EditAttribute(EditAttributeViewModel viewModel)
        {
            if (!ModelState.IsValid)
                return View(viewModel);
            var attribute = _attributeService.GetAttributeById(viewModel.Id);
            var oldName = attribute.Name;
            attribute.Name = viewModel.Name;
            var status = _attributeService.Edit(attribute);
            if (!status)
            {
                ModelState.AddModelError("Name", "این ویژگی قبلا برای این گروه ثبت شده است");
                return View(viewModel);
            }
            if (viewModel.CascadeAddForChildren)
            {
                var category = _categoryService.GetById(viewModel.CategoryId);
                EditAttributeForChildrenCacade(oldName, viewModel.Name, category);
            }
            await _unitOfWork.SaveChangesAsync();
            return RedirectToAction(MVC.Admin.Category.ActionNames.AttributeList, MVC.Admin.Category.Name,
                new { categoryId = viewModel.CategoryId });
        }
        [NonAction]
        private void EditAttributeForChildrenCacade(string oldName, string newName, Category category)
        {
            if (category == null) return;
            if (!category.Children.Any()) return;
            var children = _categoryService.GetByParenId(category.Id);
            foreach (var child in children.Where(child => _categoryService.HasAttributeByName(oldName, child.Id)))
            {
                _attributeService.EditByCategoryId(oldName, newName, child.Id);
            }
        }
        #endregion

        #region Add
        [Route("{categoryId}/Attribute/Add")]
        [HttpGet]
        public virtual ActionResult AddAttribute(long? categoryId)
        {
            if (categoryId == null) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            var category = _categoryService.GetById(categoryId.Value);
            if (category == null) return HttpNotFound();
            PopulateCategoriesDropDownListForAttribute(category.Id);
            ViewBag.CategoryName = category.Name;
            return View(new AddAttributeViewModel { CategoryId = category.Id });
        }

        [Route("{categoryId}/Attribute/Add")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public virtual async Task<ActionResult> AddAttribute(AddAttributeViewModel viewModel)
        {
            var categryId = viewModel.CategoryId;
            if (ModelState.IsValid)
            {
                var status = _attributeService.Add(viewModel);
                if (!status)
                {
                    ModelState.AddModelError("Name", "این ویژگی قبلا برای این گروه ثبت شده است");
                    return View(viewModel);
                }
                if (viewModel.CascadeAddForChildren)
                {
                    var category = _categoryService.GetCategoryWithChildrenById(viewModel.CategoryId);
                    AddAttributeToChildrenCascade(viewModel, category);
                }
                await _unitOfWork.SaveChangesAsync();
                return RedirectToAction(MVC.Admin.Category.ActionNames.AddAttribute, MVC.Admin.Category.Name,
                    new { categoryId = categryId });
            }
            PopulateCategoriesDropDownListForAttribute(categryId);
            return View(viewModel);

        }


        [NonAction]
        private void AddAttributeToChildrenCascade(AddAttributeViewModel viewModel, Category category)
        {
            if (category == null) return;
            if (!category.Children.Any()) return;
            var childern = _categoryService.GetByParenId(category.Id);
            foreach (var child in childern.Where(child => !_categoryService.HasAttributeByName(viewModel.Name, child.Id)))
            {
                viewModel.CategoryId = child.Id;
                _attributeService.Add(viewModel);
            }

        }
        [HttpPost]
        public virtual ActionResult AddCheckExistAttributeForCategory(string name, long categoryId)
        {
            return _attributeService.ExisByName(name, categoryId) ? Json(false) : Json(true);
        }
        [HttpPost]
        public virtual ActionResult EditCheckExistAttributeForCategory(string name, long id, long categoryId)
        {
            return _attributeService.ExisByName(name, id, categoryId) ? Json(false) : Json(true);
        }
        #endregion

        #region Delete
        [HttpPost]
        [AjaxOnly]
        [ValidateAntiForgeryToken]
        public virtual ActionResult DeleteAttribute(long? id)
        {
            if (id == null) return Content(null);
            var attribute = _attributeService.GetAttributeById(id.Value);
            if (attribute == null) return Content(null);
            var category = _categoryService.GetCategoryWithChildrenById(id.Value);
            _attributeService.Delete(id.Value);
            DeleteAttributeFromChildrenOfCategory(category, attribute.Name);
            return Content("ok");
        }


        [NonAction]
        private void DeleteAttributeFromChildrenOfCategory(Category category, string attributeName)
        {
            if (category == null) return;
            if (!category.Children.Any()) return;
            var children = _categoryService.GetByParenId(category.Id);
            foreach (var child in children)
            {
                _attributeService.DeleteByCategoryIdAndAttribueName(child.Id, attributeName);
            }
        }
        #endregion
        private void PopulateCategoriesDropDownListForAttribute(long selected)
        {
            var categories = _categoryService.GetAll();
            ViewBag.CategoriesForSelect = new SelectList(categories, "Id", "Name", selected);
        }

        #endregion

        #region Validation
        
       

        [HttpPost]
        [OutputCache(Location = OutputCacheLocation.None, NoStore = true)]
        public virtual ActionResult CheckExistCategoryforAdd(string name)
        {
            return _categoryService.CheckExistName(name) ? Json(false) : Json(true);
        }
        [HttpPost]
        [OutputCache(Location = OutputCacheLocation.None, NoStore = true)]
        public virtual ActionResult CheckExistCategoryforEdit(string name, long id)
        {
            return _categoryService.CheckExistName(name, id) ? Json(false) : Json(true);
        }
        #endregion
        void PopulateCategoriesDropDownList(IEnumerable<Category> categories, long? selectedId = null)
        {
            ViewBag.CategoriesForSelect = new SelectList(categories, "Id", "Name",
                selectedId);
        }
    }
}
