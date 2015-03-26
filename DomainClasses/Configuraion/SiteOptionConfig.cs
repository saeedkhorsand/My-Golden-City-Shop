using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DomainClasses.Entities;

namespace DomainClasses.Configuraion
{
   public class SiteOptionConfig:EntityTypeConfiguration<SiteOption>
   {
       public SiteOptionConfig()
       {
           Property(a => a.Name).HasMaxLength(50);
           Property(a => a.Value).IsMaxLength();
       }
    }
}
