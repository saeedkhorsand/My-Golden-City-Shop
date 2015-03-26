using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DomainClasses.Entities;

namespace DomainClasses.Configuraion
{
    public class ValueConfig:EntityTypeConfiguration<Value>
    {
        public ValueConfig()
        {
            Property(a => a.Content).IsRequired().HasMaxLength(400);
        }
    }
}
