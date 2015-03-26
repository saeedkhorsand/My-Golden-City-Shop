using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DomainClasses.Entities;

namespace ServiceLayer.Interfaces
{
    public interface IShoppingCartService
    {
        void Add(ShoppingCart cart);
        Task<int> Delete(string cartId);
        void Update(ShoppingCart cart);
        IEnumerable<ShoppingCart> List(string cartId);
        ShoppingCart GetById(long id);
        decimal TotalValueInCart(string cartId);
        ShoppingCart GetCartItem(long productId, string cartId);
        decimal DeleteItem(long productId, string cartId);
    }
}
