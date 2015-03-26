using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DomainClasses.Entities;

namespace DomainClasses.Configuraion
{
    public class ProductConfig : EntityTypeConfiguration<Product>
    {
        public ProductConfig()
        {
            HasMany(a => a.Values).WithRequired(a => a.Product).WillCascadeOnDelete(true);
            HasMany(a => a.Images).WithRequired(a => a.Product).HasForeignKey(a=>a.ProductId).WillCascadeOnDelete(true);
            HasMany(a => a.ShoppingCarts).WithRequired(a => a.Product).HasForeignKey(a=>a.ProductId).WillCascadeOnDelete(true);
            HasMany(a => a.OrderDetails).WithRequired(a => a.Product).WillCascadeOnDelete(true);

            HasMany(x => x.LikedUsers).WithMany(x => x.LikedProducts).Map(x =>
            {
                x.ToTable("LikeUsersProducts");
                x.MapLeftKey("ProductId");
                x.MapRightKey("UserId");
            });

            HasMany(x => x.UsersFavorite).WithMany(x => x.ProductsFavorite).Map(x =>
            {
                x.ToTable("Favorite");
                x.MapLeftKey("ProductId");
                x.MapRightKey("UserId");
            });
            Property(x => x.Description).IsMaxLength().IsRequired();
            Property(a => a.MetaDescription).HasMaxLength(400).IsRequired();
            Property(a => a.MetaKeyWords).HasMaxLength(100).IsRequired();

            Property(x => x.Name).HasMaxLength(100).IsRequired();
            Property(x => x.Ratio).IsRequired();
            Property(x => x.Price).IsRequired();
            Property(a => a.NotificationStockMinimum).IsRequired();
            Property(a => a.RowVersion).IsRowVersion();
        }
    }
}
