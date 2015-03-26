using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DomainClasses.Entities;

namespace DomainClasses.Configuraion
{
    public class FolderConfig : EntityTypeConfiguration<Folder>
    {
        public FolderConfig()
        {
            Property(a => a.Name).IsRequired().HasMaxLength(50);
            HasMany(a => a.Pictures)
                .WithRequired(a => a.Folder)
                .WillCascadeOnDelete(true);
        }
    }
}
