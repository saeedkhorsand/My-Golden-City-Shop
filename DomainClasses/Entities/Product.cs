using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks;
using DomainClasses.Enums;

namespace DomainClasses.Entities
{
    public class Product
    {
        public virtual long Id { get; set; }
        public virtual DateTime AddedDate { get; set; }
        public virtual string Name { get; set; }
        public virtual string MetaDescription { get; set; }
        public virtual string Description { get; set; }
        public virtual string MetaKeyWords { get; set; }
        public virtual string PrincipleImagePath { get; set; }
        public virtual bool IsFreeShipping { get; set; }
        public virtual decimal Stock { get; set; }
        public virtual decimal Reserve { get; set; }
        public virtual decimal NotificationStockMinimum { get; set; }
        public virtual decimal SellCount { get; set; }
        public virtual int ViewCount { get; set; }
        public virtual long Price { get; set; }
        public virtual bool Deleted { get; set; }
        public virtual Rate Rate { get; set; }
        public virtual decimal Ratio { get; set; }
        public virtual bool ApplyCategoryDiscount { get; set; }
        public virtual Category Category { get; set; }
        public virtual long CategoryId { get; set; }
        public virtual int DiscountPercent { get; set; }
        public virtual ProductType Type { get; set; }
        public virtual ICollection<OrderDetail> OrderDetails { get; set; }
        public virtual ICollection<ShoppingCart> ShoppingCarts { get; set; }
        public virtual byte[] RowVersion { get; set; }
        [InverseProperty("ProductsFavorite")]
        public virtual ICollection<User> UsersFavorite  { get; set; }
        public virtual ICollection<Value> Values { get; set; }
        public virtual ICollection<Comment> Comments { get; set; }
        public virtual ICollection<ProductImage> Images { get; set; }

        public virtual ICollection<User> LikedUsers { get; set; }
    }
}
