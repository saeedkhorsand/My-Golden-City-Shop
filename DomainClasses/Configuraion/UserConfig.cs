using DomainClasses.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Infrastructure.Annotations;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainClasses.Configuraion
{
    public class UserConfig : EntityTypeConfiguration<User>
    {
        public UserConfig()
        {
            HasMany(a=>a.Orders).WithRequired(a=>a.Buyer).WillCascadeOnDelete(true);

            Property(x => x.UserName).HasMaxLength(50).IsRequired()
                 .HasColumnAnnotation("Index", new IndexAnnotation(new IndexAttribute("IX_UserName"){IsUnique = true}));
            Property(x => x.Password).HasMaxLength(200).IsRequired();
            Property(x => x.PhoneNumber)
                .HasMaxLength(20)
                .IsRequired()
                .HasColumnAnnotation("Index", new IndexAnnotation(new IndexAttribute("IX_PhoneNumber"){IsUnique = true}));
            
            Property(x => x.FirstName).HasMaxLength(50).IsOptional();
            Property(x => x.LastName).HasMaxLength(50).IsOptional();
            Property(x => x.AvatarPath).HasMaxLength(200);
            Property(x => x.Email).HasMaxLength(100).IsOptional();
            Property(x => x.IP).HasMaxLength(20).IsOptional();
            Property(x => x.RowVersion).IsRowVersion();
        }
    }
}
