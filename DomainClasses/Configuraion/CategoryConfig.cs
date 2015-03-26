using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DomainClasses.Entities;

namespace DomainClasses.Configuraion
{
    public class CategoryConfig : EntityTypeConfiguration<Category>
    {
        public CategoryConfig()
        {
            HasMany(a => a.Products).WithRequired(a => a.Category).HasForeignKey(a => a.CategoryId).WillCascadeOnDelete(false);
            HasMany(a => a.Children).WithOptional(a => a.Parent).HasForeignKey(a => a.ParentId);
            Property(a => a.Description).HasMaxLength(400).IsRequired();
            
            Property(a => a.KeyWords).HasMaxLength(100).IsRequired();
            Property(a => a.Name).HasMaxLength(50).IsRequired();
            Property(a => a.RowVersion).IsRowVersion();
        }
    }
}
