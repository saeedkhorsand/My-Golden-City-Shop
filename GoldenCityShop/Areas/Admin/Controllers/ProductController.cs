using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using System.Web.Mvc;
using System.Web.UI;
using DataLayer.Context;
using DomainClasses.Entities;
using DomainClasses.Enums;
using GoldenCitShop.Filters;
using GoldenCityShop.Extentions;
using GoldenCityShop.Helpers;
using GoldenCityShop.Searching;
using ServiceLayer.Interfaces;
using ViewModel.Admin.Product;
using Order = DomainClasses.Enums.Order;

namespace GoldenCityShop.Areas.Admin.Controllers
{
    [SiteAuthorize(Roles = "admin")]
    [RoutePrefix("Product")]
    [RouteArea("Admin")]
    [Route("{action}")]
    public partial class ProductController : Controller
    {
        #region Fields

        private readonly IValueService _valueService;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IProductService _productService;
        private readonly ICategoryService _categoryService;
        private readonly IAttributeService _attributeService;
        private readonly IProductImageService _productImageService;
        #endregion

        #region Constructor
        public ProductController(IUnitOfWork unitOfWork, IProductService productService,
            IAttributeService attributeService, ICategoryService categoryService, IProductImageService productImageService, IValueService valueService)
        {
            _unitOfWork = unitOfWork;
            _productService = productService;
            _categoryService = categoryService;
            _attributeService = attributeService;
            _productImageService = productImageService;
            _valueService = valueService;
        }

        #endregion

        #region Index, Lis
        [HttpGet]
        public virtual ActionResult Index()
        {
            //var list = new List<Product>();
            //for (var i = 0; i < 10000; i++)
            //{
            //    var product = new Product
            //    {
            //        Name = "محصول  " + i,
            //        ApplyCategoryDiscount = true,
            //        CategoryId = 8,
            //        Deleted = false,
            //        IsFreeShipping = false,
            //        Description = "salam kala  " + i,
            //        DiscountPercent = 0,
            //        MetaDescription = "ljj",
            //        MetaKeyWords = "dlj",
            //        Price = i,
            //        NotificationStockMinimum = 1,
            //        PrincipleImagePath = "/Uploads/b3c3efc9-f942-49ee-af41-19b7a05844e6شش.png",
            //        Ratio = 1,
            //        SellCount = 0,
            //        ViewCount = 0,
            //        Reserve = 0,
            //        Stock = i,
            //        Type = ProductType.Packing,
            //        Rate = new Rate(),
            //        AddedDate = DateTime.Now
            //    };
            //    list.Add(product);
            //}
            //_productService.AddBulkProduct(list);
            //_unitOfWork.SaveChanges();
            PopulateCategoriesDropDownList(0);
            return View();
        }

        [OutputCache(Location = OutputCacheLocation.None, NoStore = true, Duration = 0)]
        public virtual ActionResult List(bool freeSend = false, bool deleted = false, string term = "", int page = 1, int count =10,
          Order order = Order.Descending, ProductOrderBy productOrderBy = ProductOrderBy.Name, long categoryId = 0, ProductType productType = ProductType.All)
        {
            #region Retrive Data
            int total;
            var products = _productService.DataList(out total, term, deleted, freeSend, page, count, categoryId, productOrderBy,
                order, productType);
            var model = new ProductListViewModel
            {
                CategoryId = categoryId,
                Order = order,
                ProductOrderBy = productOrderBy,
                PageCount = count,
                PageNumber = page,
                ProductList = products,
                ProductType = productType,
                Term = term,
                TotalProducts = total,
                Deleted = deleted,
                FreeSend = freeSend
            };
            #endregion

            ViewBag.ProductOrderByList = DropDown.GetProductOrderByList(productOrderBy);
            ViewBag.CountList = DropDown.GetCountList(count);
            ViewBag.OrderList = DropDown.GetOrderList(order);
            PopulateCategoriesDropDownList(categoryId);
            return PartialView(MVC.Admin.Product.Views._ListPartial, model);
        }


        #endregion

        #region Create
        [HttpGet]
        [Route("Add")]
        public virtual ActionResult Create()
        {
            PopulateCategoriesDropDownList(null);
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("Add")]
        public virtual async Task<ActionResult> Create(AddProductViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                var product = new Product
                {
                    Name = viewModel.Name,
                    ApplyCategoryDiscount = viewModel.ApplyCategoryDiscount,
                    CategoryId = viewModel.CategoryId,
                    Deleted = viewModel.Deleted,
                    IsFreeShipping = viewModel.IsFreeShipping,
                    Description = viewModel.Description,
                    DiscountPercent = viewModel.DiscountPercent,
                    MetaDescription = viewModel.MetaDescription,
                    MetaKeyWords = viewModel.MetaKeyWords,
                    Price = viewModel.Price,
                    NotificationStockMinimum = viewModel.NotificationStockMinimum,
                    PrincipleImagePath = viewModel.PrincipleImagePath,
                    Ratio = viewModel.Ratio,
                    SellCount = 0,
                    ViewCount = 0,
                    Reserve = 0,
                    Stock = viewModel.Stock,
                    Type = viewModel.Type,
                    Rate = new Rate(),
                    AddedDate = DateTime.Now
                };
                _productService.Insert(product);
                var attributes = _attributeService.GetAttributesByCategoryId(viewModel.CategoryId);
                _valueService.AddCategoryAttributesToProduct(attributes, product.Id);
                await _unitOfWork.SaveChangesAsync();
                LuceneProducts.AddIndex(new ProductLuceneViewModel
                {
                    Name = product.Name,
                    Id = product.Id,
                    ImagePath = product.PrincipleImagePath,
                    Description = product.Description
                });
                CacheManager.InvalidateChildActionsCache();
                return RedirectToAction(MVC.Admin.Product.ActionNames.Create, MVC.Admin.Product.Name);
            }
            PopulateCategoriesDropDownList(viewModel.CategoryId);
            if (!ModelState.IsValidField("CategoryId"))
                ModelState.AddModelError("", "گروه محصول را مشخص  کنید");
            return View(viewModel);
        }
        #endregion

        #region Delete
        [HttpPost]
        [ValidateAntiForgeryToken]
        [AjaxOnly]
        [OutputCache(Location = OutputCacheLocation.None, NoStore = true, Duration = 0)]
        public virtual async Task<ActionResult> Delete(long? id)
        {
            if (id == null) return Content(null);
            _productService.Delete(id.Value);
            await _unitOfWork.SaveChangesAsync();
            LuceneProducts.DeleteIndex(id.Value);
            CacheManager.InvalidateChildActionsCache();
            return Content("ok");
        }


        #endregion

        #region Edit
        [HttpGet]
        [Route("Edit/{id}")]
        public virtual ActionResult Edit(long? id)
        {
            if (id == null) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            var product = _productService.GetForEdit(id.Value);
            if (product == null) return HttpNotFound();
            PopulateCategoriesDropDownList(product.CategoryId);
            return View(product);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("Edit/{id}")]
        public virtual async Task<ActionResult> Edit(EditProductViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                _productService.Update(viewModel);
                if (viewModel.OldCategoryId != viewModel.CategoryId)
                {
                    _valueService.RemoveByProductId(viewModel.Id);
                    var attributes = _attributeService.GetAttributesByCategoryId(viewModel.CategoryId);
                    _valueService.AddCategoryAttributesToProduct(attributes, viewModel.Id);
                }

                await _unitOfWork.SaveChangesAsync();
                LuceneProducts.UpdateIndex(new ProductLuceneViewModel
                {
                    Name = viewModel.Name,
                    Id = viewModel.Id,
                    ImagePath = viewModel.PrincipleImagePath,
                    Description = viewModel.Description
                });
                CacheManager.InvalidateChildActionsCache();
                return RedirectToAction(MVC.Admin.Product.ActionNames.Index, MVC.Admin.Product.Name);
            }

            PopulateCategoriesDropDownList(viewModel.CategoryId);
            if (!ModelState.IsValidField("CategoryId"))
                ModelState.AddModelError("", "گروه محصول را مشخص  کنید");
            return View(viewModel);
        }
        #endregion

        #region Pictures
        [HttpGet]
        [Route("{productId}/Add-Pictures")]
        public virtual ActionResult AddPictures(long? productId)
        {
            if (productId == null) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            var product = _productService.GetById(productId.Value);
            if (product == null) return HttpNotFound();

            return View(new AddProductPicturesViewModel { ProductId = productId.Value });
        }
        [HttpPost]
        [Route("{productId}/Add-Pictures")]
        [ValidateAntiForgeryToken]
        public virtual async Task<ActionResult> AddPictures(AddProductPicturesViewModel viewModel)
        {
            _productImageService.Insert(viewModel);
            await _unitOfWork.SaveChangesAsync();
            return RedirectToAction(MVC.Admin.Product.ActionNames.Index, MVC.Admin.Product.Name);
        }

        [HttpGet]
        [Route("{productId}/Edit-Pictures")]
        public virtual ActionResult EditPictures(long? productId)
        {
            if (productId == null) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            var product = _productService.GetById(productId.Value);
            if (product == null) return HttpNotFound();
            var images = _productImageService.GetImages(productId.Value);
            return View(images);
        }
        [HttpPost]
        [Route("{productId}/Edit-Pictures")]
        [ValidateAntiForgeryToken]
        public virtual async Task<ActionResult> EditPictures(IEnumerable<ProductImage> images)
        {
            _productImageService.Edit(images);
            await _unitOfWork.SaveChangesAsync();
            return RedirectToAction(MVC.Admin.Product.ActionNames.Index, MVC.Admin.Product.Name);
        }
        #endregion

        #region FillAttributesOfCategoy
        [HttpGet]
        [AjaxOnly]
        [OutputCache(Location = OutputCacheLocation.None, NoStore = true, Duration = 0, VaryByParam = "*")]
        public virtual ActionResult FillAttributes(long? productId)
        {
            if (productId == null) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            var values = _valueService.GetForUpdateValuesByProductId(productId.Value);
            return PartialView(MVC.Admin.Product.Views.FillAttributes, values);
        }

        [HttpPost]
        [AjaxOnly]
        [OutputCache(Location = OutputCacheLocation.None, NoStore = true, Duration = 0, VaryByParam = "*")]
        [ValidateAntiForgeryToken]
        public virtual async Task<ActionResult> FillAttributes(IEnumerable<FillProductAttributesViewModel> values)
        {
            _valueService.UpdateValues(values);
            await _unitOfWork.SaveChangesAsync();
            return Content("ok");
        }
        #endregion
        void PopulateCategoriesDropDownList(long? selectedId)
        {
            var categories = _categoryService.GetSecondLevelCategories();
            ViewBag.Categories = new SelectList(categories, "Id", "Name",
                selectedId);
        }
    }
}
