using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DomainClasses.Enums;

namespace DomainClasses.Entities
{
    public class Attribute
    {
        public virtual long Id { get; set; }
        public virtual string Name { get; set; }
        public virtual Category Category{ get; set; }
        public virtual AttributeType Type { get; set; }
        public virtual long CategoryId{ get; set; }
        public virtual byte[] RowVersion { get; set; }
        public virtual ICollection<Value> Values { get; set; }

    }
}
