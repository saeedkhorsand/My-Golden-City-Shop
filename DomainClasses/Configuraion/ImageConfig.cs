using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DomainClasses.Entities;

namespace DomainClasses.Configuraion
{
   public class ImageConfig:EntityTypeConfiguration<ProductImage>
   {
       public ImageConfig()
       {
           Property(a => a.Path).IsOptional().IsMaxLength();
       }
    }
}
