using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataLayer.Context;
using DomainClasses.Entities;
using EntityFramework.Extensions;
using ServiceLayer.Interfaces;

namespace ServiceLayer.EFServices
{
    public class ShoppingCartService : IShoppingCartService
    {
        #region Fields

        private readonly IProductService _productService;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IDbSet<ShoppingCart> _shoppingCarts;
        #endregion

        #region Constructor

        public ShoppingCartService(IUnitOfWork unitOfWork, IProductService productService)
        {
            _unitOfWork = unitOfWork;
            _shoppingCarts = _unitOfWork.Set<ShoppingCart>();
            _productService = productService;
        }
        #endregion
        public void Add(DomainClasses.Entities.ShoppingCart cart)
        {
            _shoppingCarts.Add(cart);
        }

        public Task<int> Delete(string cartId)
        {
            var cartItems = _shoppingCarts.Where(a => a.CartNumber == cartId);
            cartItems.AsNoTracking().ToList().ForEach(a =>
            {
                _productService.DecreaseReserve(a.ProductId, a.Quantity);
            });
            return cartItems.DeleteAsync();
        }

        public void Update(DomainClasses.Entities.ShoppingCart cart)
        {
            _unitOfWork.MarkAsChanged(cart);
        }

        public IEnumerable<DomainClasses.Entities.ShoppingCart> List(string cartId)
        {
           return _shoppingCarts.AsNoTracking().Where(a => a.CartNumber == cartId).ToList();
        }
        public ShoppingCart GetById(long id)
        {
            return _shoppingCarts.Find(id);
        }

        public ShoppingCart GetCartItem(long productId, string cartId)
        {
            return _shoppingCarts.Where(a => a.ProductId == productId && a.CartNumber == cartId).FirstOrDefault();
        }


        public decimal TotalValueInCart(string cartId)
        {
            return _shoppingCarts.Where(a => a.CartNumber == cartId).Sum(a => a.Quantity);
        }


        public decimal DeleteItem(long productId, string cartId)
        {
            var product =
                _shoppingCarts.AsNoTracking()
                    .Where(a => a.CartNumber == cartId && a.ProductId == productId)
                    .FirstOrDefault();
            if (product == null) return decimal.Zero;
            var value = product.Quantity;
            _shoppingCarts.Where(a => a.ProductId == product.Id).Delete();
            return value;
        }
    }
}
