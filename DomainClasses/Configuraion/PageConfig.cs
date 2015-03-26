using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DomainClasses.Entities;

namespace DomainClasses.Configuraion
{
    public class PageConfig:EntityTypeConfiguration<Page>
    {
        public PageConfig()
        {
            Property(x => x.Content).IsMaxLength();
            Property(x => x.Description).HasMaxLength(400);
            Property(x => x.KeyWords).HasMaxLength(100);
            Property(x => x.Title).HasMaxLength(100);
            Property(x => x.ImagePath).HasMaxLength(400);
            Property(x => x.RowVersion).IsRowVersion();
        }
    }
}
