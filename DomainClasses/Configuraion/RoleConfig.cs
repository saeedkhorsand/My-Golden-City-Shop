using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DomainClasses.Entities;

namespace DomainClasses.Configuraion
{
    public class RoleConfig:EntityTypeConfiguration<Role>
    {
        public RoleConfig()
        {
            Property(x => x.Name).HasMaxLength(20);
            Property(x => x.Description).HasMaxLength(400);
        }
    }
}
