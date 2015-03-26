using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DomainClasses.Entities;

namespace DomainClasses.Configuraion
{
    public class CommentConfig : EntityTypeConfiguration<Comment>
    {
        public CommentConfig()
        {
            HasRequired(a => a.Product)
                .WithMany(a => a.Comments)
                .HasForeignKey(a => a.ProductId)
                .WillCascadeOnDelete(false);

            HasOptional(x => x.Parent)
                .WithMany(x => x.Children)
                .HasForeignKey(x => x.ParentId)
                .WillCascadeOnDelete(false);

            HasMany(x => x.LikedUsers).WithMany(x => x.LikedComments).Map(x =>
            {
                x.ToTable("LikeUsersComments");
                x.MapLeftKey("CommentId");
                x.MapRightKey("UserId");
            });


            Property(x => x.Content).IsMaxLength();
            Property(x => x.RowVersion).IsRowVersion();
        }
    }
}
