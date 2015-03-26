using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DomainClasses.Entities;

namespace DomainClasses.Configuraion
{
    public class OrderDetailConfig : EntityTypeConfiguration<OrderDetail>
    {
        public OrderDetailConfig()
        {
            HasRequired(a => a.Order)
                .WithMany(a => a.OrderDetails)
                .HasForeignKey(a => a.OrderId)
                .WillCascadeOnDelete(true);
            Property(a => a.RowVersion).IsRowVersion();
        }
    }
}
