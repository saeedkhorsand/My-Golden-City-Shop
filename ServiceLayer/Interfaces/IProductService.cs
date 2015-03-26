using System.Collections.Generic;
using DomainClasses.Entities;
using DomainClasses.Enums;
using ViewModel.Admin.Product;
using Order = DomainClasses.Enums.Order;

namespace ServiceLayer.Interfaces
{
    public interface IProductService
    {
        void IncreaseSell(long id, decimal value);
        void DecreaseSell(long id, decimal value);
        void IncreaseReserve(long id, decimal value);
        void DecreaseReserve(long id, decimal value);
        void IncreaseStock(long id, decimal value);
        void SaveRating(long id, double value);
        IEnumerable<ProductSectionViewModel> GetBelovedProducts();
        IEnumerable<ProductSectionViewModel> GetNewProducts();
        IEnumerable<ProductSectionViewModel> GetMoreSellProducts();
        IEnumerable<ProductSectionViewModel> GetMoreViewedProducts();
        IEnumerable<ProductSectionViewModel> GetSelecionProductOfCategory(long categoryId);
        IEnumerable<ProductLuceneViewModel> GetAllForAddLucene(); 
        void ChangeProductsCategoryById(long lastId, long newId);
        Product GetById(long id);
        void Insert(Product product);
        void Update(EditProductViewModel viewModel);
        EditProductViewModel GetForEdit(long id);
        void Delete(long id);
        bool CanUserRate(long productId, string userName);
        IEnumerable<ProductViewModel> DataList(out int total, string term, bool deleted, bool freeSend, int page,
            int count, long categoryId, ProductOrderBy productOrderBy, Order order,
            ProductType productType);
        void AppendAttributes(AppendAttributeViewModel viewModel);

        IEnumerable<ProductSectionViewModel> DataListSearch(out int total, string term, int page,
            int count, PSFilter filtr, long categoryId);

        bool CanAddToShoppingCart(long id,decimal value);

        void AddUserToLikedUsers(long id, User user);
        bool CanUserAddToWishList(long id, string userName);
        void AddUserToWishList(long id, User user);

        ProductDetailsViewModel GetForShowDetails(long id);
        ProductCompareViewModel GetForAddToCompare(long id);
        void AddBulkProduct(IEnumerable<Product> products);

        
        void RemoveFromWishList(long id,User user);
    }
}
