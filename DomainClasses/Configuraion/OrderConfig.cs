using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DomainClasses.Entities;

namespace DomainClasses.Configuraion
{
   public class OrderConfig:EntityTypeConfiguration<Order>
   {
       public OrderConfig()
       {
           Property(a => a.RowVersion).IsRowVersion();
       }
    }
}
