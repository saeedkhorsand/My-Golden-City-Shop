
using DomainClasses.Entities;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainClasses.Configuraion
{
    public class AttributeConfig : EntityTypeConfiguration<Attribute>
    {
        public AttributeConfig()
        {
            HasRequired(a => a.Category)
                .WithMany(a => a.Attributes)
                .HasForeignKey(a => a.CategoryId)
                .WillCascadeOnDelete(true);

            HasMany(a=>a.Values).WithRequired(a=>a.Attribute).WillCascadeOnDelete(true);
            Property(a => a.Name).HasMaxLength(50).IsRequired();
            Property(a => a.RowVersion).IsRowVersion();
        }
    }
}
