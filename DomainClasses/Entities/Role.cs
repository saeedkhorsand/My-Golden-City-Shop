using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DomainClasses.Entities
{
    public class Role
    {
        public virtual long Id { get; set; }
        public virtual string Name { get; set; }
        public virtual ICollection<User>  Users{ get; set; }
        public virtual string Description { get; set; }
    }
}
