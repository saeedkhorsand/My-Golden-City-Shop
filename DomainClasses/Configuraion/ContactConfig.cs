using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DomainClasses.Entities;

namespace DomainClasses.Configuraion
{
    public class ContactConfig : EntityTypeConfiguration<Contact>
    {
     
        public ContactConfig()
        {
            Property(a => a.Content).IsMaxLength().IsRequired();
            Property(a => a.Title).HasMaxLength(100).IsRequired();
            HasRequired(a => a.User).WithMany(a => a.Contacts).WillCascadeOnDelete(true);
            Property(a => a.RowVersion).IsRowVersion();
        }
    }
}
