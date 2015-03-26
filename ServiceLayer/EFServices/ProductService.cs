using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using DataLayer.Context;
using DomainClasses.Entities;
using DomainClasses.Enums;
using EFSecondLevelCache;
using EntityFramework.Extensions;
using ServiceLayer.Interfaces;
using ViewModel.Admin.Product;
using Order = DomainClasses.Enums.Order;

namespace ServiceLayer.EFServices
{
    public class ProductService : IProductService
    {
        #region Fields
        private readonly IUnitOfWork _unitOfWork;
        private readonly IDbSet<Product> _products;
        private readonly IValueService _valueService;
        #endregion

        #region Constructor

        public ProductService(IUnitOfWork unitOfWork, IValueService valueService)
        {
            _unitOfWork = unitOfWork;
            _products = _unitOfWork.Set<Product>();
            _valueService = valueService;
        }
        #endregion

        public void ChangeProductsCategoryById(long lastId, long newId)
        {
            var product = GetById(newId);
            if (product != null)
                _products.Where(a => a.CategoryId == lastId).Update(b => new Product { CategoryId = newId });
        }

        public Product GetById(long id)
        {
            return _products.Find(id);
        }

        public void Insert(Product product)
        {
            _products.Add(product);
        }

        public void Update(EditProductViewModel viewModel)
        {
            var product = GetById(viewModel.Id);
            product.Name = viewModel.Name;
            product.ApplyCategoryDiscount = viewModel.ApplyCategoryDiscount;
            product.CategoryId = viewModel.CategoryId;
            product.Deleted = viewModel.Deleted;
            product.IsFreeShipping = viewModel.IsFreeShipping;
            product.Description = viewModel.Description;
            product.DiscountPercent = viewModel.DiscountPercent;
            product.MetaDescription = viewModel.MetaDescription;
            product.MetaKeyWords = viewModel.MetaKeyWords;
            product.Price = viewModel.Price;
            product.NotificationStockMinimum = viewModel.NotificationStockMinimum;
            product.PrincipleImagePath = viewModel.PrincipleImagePath;
            product.Ratio = viewModel.Ratio;
            product.Stock = viewModel.Stock;
            product.Type = viewModel.Type;
        }

        public EditProductViewModel GetForEdit(long id)
        {
            return _products.Where(a => a.Id.Equals(id)).Select(a => new EditProductViewModel
            {
                ApplyCategoryDiscount = a.ApplyCategoryDiscount,
                CategoryId = a.CategoryId,
                Deleted = a.Deleted,
                Description = a.Description,
                DiscountPercent = a.DiscountPercent,
                Id = a.Id,
                IsFreeShipping = a.IsFreeShipping,
                Name = a.Name,
                Ratio = a.Ratio,
                MetaKeyWords = a.MetaKeyWords,
                NotificationStockMinimum = a.NotificationStockMinimum,
                Price = a.Price,
                PrincipleImagePath = a.PrincipleImagePath,
                Stock = a.Stock,
                MetaDescription = a.MetaDescription,
                Type = a.Type,
                OldCategoryId = a.CategoryId

            }).FirstOrDefault();
        }

        public void Delete(long id)
        {
            _products.Where(a => a.Id.Equals(id)).Delete();
        }

        public IEnumerable<ProductViewModel> DataList(out int total, string term, bool deleted, bool freeSend, int page, int count, long categoryId, ProductOrderBy productOrderBy, Order order, ProductType productType)
        {
            var selectedProducts =
                _products.AsNoTracking()
                    .Include(a => a.Category)
                    .Include(a => a.Values)
                    .Include(a => a.Images)
                    .AsQueryable();

            if (deleted || freeSend)
                selectedProducts =
                    selectedProducts.Where(a => a.Deleted == deleted && a.IsFreeShipping == freeSend).AsQueryable();

            if (categoryId != 0)
                selectedProducts = selectedProducts.Where(a => a.CategoryId == categoryId).AsQueryable();

            if (productType != ProductType.All)
                selectedProducts = selectedProducts.Where(a => a.Type == productType).AsQueryable();

            if (!string.IsNullOrEmpty(term))
                selectedProducts = selectedProducts.Where(product => product.Name.Contains(term)).AsQueryable();

            if (order == Order.Asscending)
            {
                switch (productOrderBy)
                {
                    case ProductOrderBy.Name:
                        selectedProducts = selectedProducts.OrderBy(product => product.Name).AsQueryable();
                        break;
                    case ProductOrderBy.DiscountPercent:
                        selectedProducts = selectedProducts.OrderBy(product => product.DiscountPercent).AsQueryable();
                        break;
                    case ProductOrderBy.NotificationStockMinimun:
                        selectedProducts = selectedProducts.OrderBy(product => product.NotificationStockMinimum).AsQueryable();
                        break;
                    case ProductOrderBy.Price:
                        selectedProducts = selectedProducts.OrderBy(product => product.Price).AsQueryable();
                        break;
                    case ProductOrderBy.ReserveCount:
                        selectedProducts = selectedProducts.OrderBy(product => product.Reserve).AsQueryable();
                        break;
                    case ProductOrderBy.SellCount:
                        selectedProducts = selectedProducts.OrderBy(product => product.SellCount).AsQueryable();
                        break;
                    case ProductOrderBy.StockCount:
                        selectedProducts = selectedProducts.OrderBy(product => product.Stock).AsQueryable();
                        break;
                    case ProductOrderBy.ViewCount:
                        selectedProducts = selectedProducts.OrderBy(product => product.ViewCount).AsQueryable();
                        break;
                }
            }
            else
            {
                switch (productOrderBy)
                {
                    case ProductOrderBy.Name:
                        selectedProducts = selectedProducts.OrderByDescending(product => product.Name).AsQueryable();
                        break;
                    case ProductOrderBy.DiscountPercent:
                        selectedProducts = selectedProducts.OrderByDescending(product => product.DiscountPercent).AsQueryable();
                        break;
                    case ProductOrderBy.NotificationStockMinimun:
                        selectedProducts = selectedProducts.OrderByDescending(product => product.NotificationStockMinimum).AsQueryable();
                        break;
                    case ProductOrderBy.Price:
                        selectedProducts = selectedProducts.OrderByDescending(product => product.Price).AsQueryable();
                        break;
                    case ProductOrderBy.ReserveCount:
                        selectedProducts = selectedProducts.OrderByDescending(product => product.Reserve).AsQueryable();
                        break;
                    case ProductOrderBy.SellCount:
                        selectedProducts = selectedProducts.OrderByDescending(product => product.SellCount).AsQueryable();
                        break;
                    case ProductOrderBy.StockCount:
                        selectedProducts = selectedProducts.OrderByDescending(product => product.Stock).AsQueryable();
                        break;
                    case ProductOrderBy.ViewCount:
                        selectedProducts = selectedProducts.OrderByDescending(product => product.ViewCount).AsQueryable();
                        break;
                }
            }
            var totalQuery = selectedProducts.FutureCount();
            var selectQuery = selectedProducts.Skip((page - 1) * count).Take(count)
                .Select(a => new ProductViewModel
                {
                    ApplyCategoryDiscount = a.ApplyCategoryDiscount,
                    CategoryName = a.Category.Name,
                    Deleted = a.Deleted,
                    DiscountPercent = a.DiscountPercent,
                    Id = a.Id,
                    IsFreeShipping = a.IsFreeShipping,
                    Name = a.Name,
                    NotificationStockMinimum = a.NotificationStockMinimum,
                    Price = a.Price,
                    PrincipleImagePath = a.PrincipleImagePath,
                    Rate = a.Rate.TotalRating,
                    Ratio = a.Ratio,
                    ReserveCount = a.Reserve,
                    SellCount = a.SellCount,
                    Stock = a.Stock,
                    ViewCount = a.ViewCount,
                    AddedImages = a.Images.Any(),
                    CompletedAttributes = a.Values.Any(),
                    Notification = a.Stock - a.Reserve <= a.NotificationStockMinimum
                }).Future();
            total = totalQuery.Value;
            var products = selectQuery.ToList();
            return products;
        }

        public void AppendAttributes(AppendAttributeViewModel viewModel)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<ProductSectionViewModel> GetBelovedProducts()
        {

            return _products.AsNoTracking().Where(a => !a.Deleted).Include(a => a.Category)

                .OrderByDescending(a => a.Rate.TotalRaters).ThenByDescending(a => a.Rate.AverageRating).Skip(0).Take(3)
                .Select(a => new ProductSectionViewModel
                {
                    TotalDiscount = a.ApplyCategoryDiscount ? a.DiscountPercent + a.Category.DiscountPercent : a.DiscountPercent,
                    Id = a.Id,
                    IsFreeShipping = a.IsFreeShipping,
                    Name = a.Name,
                    Price = a.Price,
                    PrincipleImagePath = a.PrincipleImagePath,
                    AvrageRate = a.Rate.AverageRating,
                    Ratio = a.Ratio,
                    TotalRaters = a.Rate.TotalRaters ?? 0,
                    SellCount = a.SellCount,
                    IsInStock = a.Stock - a.Reserve >= a.Ratio,
                    ViewCount = a.ViewCount
                }).Cacheable().ToList();

        }

        public IEnumerable<ProductSectionViewModel> GetNewProducts()
        {
            return
                _products.AsNoTracking()
                    .Where(a => !a.Deleted && a.Stock - a.Reserve >= a.Ratio)
                    .Include(a => a.Category)
                    .OrderByDescending(a => a.Rate.TotalRaters).ThenByDescending(a => a.Rate.AverageRating).Skip(0).Take(3)
                    .Select(a => new ProductSectionViewModel
                    {
                        TotalDiscount = a.ApplyCategoryDiscount ? a.DiscountPercent + a.Category.DiscountPercent : a.DiscountPercent,
                        Id = a.Id,
                        IsFreeShipping = a.IsFreeShipping,
                        Name = a.Name,
                        Price = a.Price,
                        PrincipleImagePath = a.PrincipleImagePath,
                        AvrageRate = a.Rate.AverageRating,
                        Ratio = a.Ratio,
                        TotalRaters = a.Rate.TotalRaters ?? 0,
                        SellCount = a.SellCount,
                        IsInStock = a.Stock - a.Reserve >= a.Ratio,
                        ViewCount = a.ViewCount
                    }).Cacheable().ToList();
        }

        public IEnumerable<ProductSectionViewModel> GetMoreSellProducts()
        {
            return _products.AsNoTracking()
                .Where(a => !a.Deleted && a.Stock - a.Reserve >= a.Ratio)
                .Include(a => a.Category)
                .OrderByDescending(a => a.Rate.TotalRaters).ThenByDescending(a => a.Rate.AverageRating).Skip(0).Take(12)
                .Select(a => new ProductSectionViewModel
                {
                    TotalDiscount = a.ApplyCategoryDiscount ? a.DiscountPercent + a.Category.DiscountPercent : a.DiscountPercent,
                    Id = a.Id,
                    IsFreeShipping = a.IsFreeShipping,
                    Name = a.Name,
                    Price = a.Price,
                    PrincipleImagePath = a.PrincipleImagePath,
                    AvrageRate = a.Rate.AverageRating,
                    TotalRaters = a.Rate.TotalRaters ?? 0,
                    Ratio = a.Ratio,
                    SellCount = a.SellCount,
                    ViewCount = a.ViewCount
                }).Cacheable().ToList();
        }
        public IEnumerable<ProductSectionViewModel> GetMoreViewedProducts()
        {
            return
                _products.AsNoTracking()
                    .Where(a => !a.Deleted && a.Stock - a.Reserve >= a.Ratio)
                    .Include(a => a.Category)
                    .OrderByDescending(a => a.Rate.TotalRaters).ThenByDescending(a => a.Rate.AverageRating).Skip(0).Take(3)
                    .Select(a => new ProductSectionViewModel
                    {
                        TotalDiscount = a.ApplyCategoryDiscount ? a.DiscountPercent + a.Category.DiscountPercent : a.DiscountPercent,
                        Id = a.Id,
                        IsFreeShipping = a.IsFreeShipping,
                        Name = a.Name,
                        Price = a.Price,
                        PrincipleImagePath = a.PrincipleImagePath,
                        AvrageRate = a.Rate.AverageRating,
                        Ratio = a.Ratio,
                        TotalRaters = a.Rate.TotalRaters ?? 0,
                        SellCount = a.SellCount,
                        IsInStock = a.Stock - a.Reserve >= a.Ratio,
                        ViewCount = a.ViewCount
                    }).Cacheable().ToList();
        }


        public IEnumerable<ProductSectionViewModel> DataListSearch(out int total, string term, int page,
            int count, PSFilter filtr, long categoryId)
        {
            var selectedProducts =
                _products.AsNoTracking().Where(a => !a.Deleted)
                    .Include(a => a.Category).AsQueryable();

            if (categoryId != 0)
                selectedProducts = selectedProducts.Where(a => a.CategoryId == categoryId).AsQueryable();

            if (!string.IsNullOrEmpty(term))
                selectedProducts = selectedProducts.Where(product => product.Name.Contains(term)).AsQueryable();
            switch (filtr)
            {
                case PSFilter.All:
                    selectedProducts = selectedProducts.OrderByDescending(a => a.Id).AsQueryable();
                    break;
                case PSFilter.New:
                    selectedProducts = selectedProducts.OrderByDescending(a => a.AddedDate).AsQueryable();
                    break;
                case PSFilter.MoreView:
                    selectedProducts = selectedProducts.OrderByDescending(a => a.ViewCount).AsQueryable();
                    break;
                case PSFilter.MoreSell:
                    selectedProducts = selectedProducts.OrderByDescending(a => a.SellCount).AsQueryable();
                    break;
                case PSFilter.Beloved:
                    selectedProducts = selectedProducts.OrderByDescending(a => a.Rate.TotalRaters).ThenByDescending(a => a.Rate.AverageRating).AsQueryable();
                    break;
                case PSFilter.IsInStock:
                    selectedProducts =
                        selectedProducts.Where(a => a.Stock - a.Reserve >= a.Ratio)
                            .OrderByDescending(a => a.Id)
                            .AsQueryable();
                    break;
                case PSFilter.HasDiscount:
                    selectedProducts =
                        selectedProducts.Where(a => a.DiscountPercent + a.Category.DiscountPercent > 0)
                            .OrderByDescending(a => a.Id)
                            .AsQueryable();
                    break;
                case PSFilter.FreeSend:
                    selectedProducts =
                        selectedProducts.Where(a => a.IsFreeShipping)
                            .OrderByDescending(a => a.Id)
                            .AsQueryable();
                    break;
            }

            var totalQuery = selectedProducts.FutureCount();
            var selectQuery = selectedProducts.Skip((page - 1) * count).Take(count)
                .Select(a => new ProductSectionViewModel
                {
                    AvrageRate = a.Rate.AverageRating,
                    Id = a.Id,
                    IsFreeShipping = a.IsFreeShipping,
                    IsInStock = a.Stock - a.Reserve >= a.Ratio,
                    Name = a.Name,
                    Price = a.Price,
                    TotalRaters = a.Rate.TotalRaters ?? 0,
                    Ratio = a.Ratio,
                    PrincipleImagePath = a.PrincipleImagePath,
                    SellCount = a.SellCount,
                    TotalDiscount = a.ApplyCategoryDiscount ? a.DiscountPercent + a.Category.DiscountPercent : a.DiscountPercent,
                    ViewCount = a.ViewCount
                }).Future();
            total = totalQuery.Value;
            var products = selectQuery.ToList();
            return products;
        }


        public IEnumerable<ProductLuceneViewModel> GetAllForAddLucene()
        {
            return _products.Include(a => a.Values).Select(a => new ProductLuceneViewModel
            {
                Name = a.Name,
                Id = a.Id,
                ImagePath = a.PrincipleImagePath,
                Description = a.Description
            }).ToList();
        }

        public void SaveRating(long id, double value)
        {
            var product = GetById(id);
            if (product == null) return;

            if (!product.Rate.TotalRaters.HasValue) product.Rate.TotalRaters = 0;
            if (!product.Rate.TotalRating.HasValue) product.Rate.TotalRating = 0;
            if (!product.Rate.AverageRating.HasValue) product.Rate.AverageRating = 0;

            product.Rate.TotalRaters++;
            product.Rate.TotalRating += value;
            product.Rate.AverageRating = product.Rate.TotalRating / product.Rate.TotalRaters;

        }


        public bool CanAddToShoppingCart(long id, decimal value)
        {
            var product = GetById(id);
            if (product == null) return false;
            return product.Stock - product.Reserve >= product.Ratio * value;
        }


        public void IncreaseSell(long id, decimal value)
        {
            var product = GetById(id);
            if (product == null) return;
            product.SellCount += value;
        }

        public void DecreaseSell(long id, decimal value)
        {
            var product = GetById(id);
            if (product == null) return;
            product.SellCount -= value;
        }

        public void IncreaseReserve(long id, decimal value)
        {
            var product = GetById(id);
            if (product == null) return;
            product.Reserve += value;
        }

        public void DecreaseReserve(long id, decimal value)
        {
            var product = GetById(id);
            if (product == null) return;
            product.Reserve -= value;
        }

        public void IncreaseStock(long id, decimal value)
        {
            var product = GetById(id);
            if (product == null) return;
            product.Stock += value;
        }


        public bool CanUserRate(long productId, string userName)
        {
            return GetById(productId).LikedUsers.All(a => a.UserName != userName);
        }


        public void AddUserToLikedUsers(long id, User user)
        {
            var product = GetById(id);
            product.LikedUsers.Add(user);
        }


        public bool CanUserAddToWishList(long id, string userName)
        {
            return GetById(id).UsersFavorite.All(a => a.UserName != userName);
        }

        public void AddUserToWishList(long id, User user)
        {
            GetById(id).UsersFavorite.Add(user);
        }
        public ProductCompareViewModel GetForAddToCompare(long id)
        {
            var product = _products.Where(a => a.Id == id).Include(a => a.Category).FirstOrDefault();
            var attributes = _valueService.GetAttValueOfProduct(id);
            return new ProductCompareViewModel
            {
                Attributes = attributes,
                ProductName = product.Name,
                ImagePath = product.PrincipleImagePath,
                AvrageRate = product.Rate.AverageRating,
                Description = product.Description,
                ProductId = product.Id,
                TotalRaters = product.Rate.TotalRaters ?? 0,
                Price = product.Price,
                FreeSend = product.IsFreeShipping,
                Discount = product.ApplyCategoryDiscount ? product.DiscountPercent + product.Category.DiscountPercent : product.DiscountPercent,
            };
        }

        public ProductDetailsViewModel GetForShowDetails(long id)
        {
            var product = _products
                .Where(a => a.Id == id)
                .Include(a => a.Images).Include(a => a.Category).Include(a => a.Comments).FirstOrDefault();
            product.ViewCount++;
            return new ProductDetailsViewModel
            {
                TotalDiscount =
                    product.ApplyCategoryDiscount
                        ? product.DiscountPercent + product.Category.DiscountPercent
                        : product.DiscountPercent,
                Id = product.Id,
                IsFreeShipping = product.IsFreeShipping,
                Name = product.Name,
                Price = product.Price,
                TotalRaters = product.Rate.TotalRaters ?? 0,
                CategoryId = product.CategoryId,
                CommentsCount = product.Comments.Count,
                AvrageRate = product.Rate.AverageRating,
                Ratio = product.Ratio,
                PrincipleImage = product.PrincipleImagePath,
                Images = product.Images.Select(a => a.Path).ToArray(),
                SellCount = product.SellCount,
                IsInStock = product.Stock - product.Reserve >= product.Ratio,
                ViewCount = product.ViewCount,
                Description = product.Description
            };
        }

        public void AddBulkProduct(IEnumerable<Product> products)
        {
            try
            {
                _unitOfWork.AutoDetectChangesEnabled();
                ((DbSet<Product>)_products).AddRange(products);
            }
            finally
            {
                _unitOfWork.AutoDetectChangesEnabled(false);
            }
        }




        public void RemoveFromWishList(long id, User user)
        {
            GetById(id).UsersFavorite.Remove(user);
        }


        public IEnumerable<ProductSectionViewModel> GetSelecionProductOfCategory(long categoryId)
        {
            return
                _products.AsNoTracking()
                    .Where(a => !a.Deleted && a.Stock - a.Reserve >= a.Ratio && a.CategoryId == categoryId)
                    .Include(a => a.Category)
                    .OrderBy(a => a.SellCount).Skip(0).Take(4)
                    .Select(a => new ProductSectionViewModel
                    {
                        TotalDiscount = a.ApplyCategoryDiscount ? a.DiscountPercent + a.Category.DiscountPercent : a.DiscountPercent,
                        Id = a.Id,
                        IsFreeShipping = a.IsFreeShipping,
                        Name = a.Name,
                        Price = a.Price,
                        TotalRaters = a.Rate.TotalRaters ?? 0,
                        PrincipleImagePath = a.PrincipleImagePath,
                        AvrageRate = a.Rate.AverageRating,
                        Ratio = a.Ratio,
                        SellCount = a.SellCount,
                        IsInStock = a.Stock - a.Reserve >= a.Ratio,
                        ViewCount = a.ViewCount
                    }).Cacheable().ToList();
        }
    }
}
