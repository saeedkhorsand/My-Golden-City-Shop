using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainClasses.Entities
{
    public class Category
    {
        public virtual long Id { get; set; }
        public virtual string Name { get; set; }
        public virtual Category Parent { get; set; }
        public virtual long? ParentId { get; set; }
        public virtual ICollection<Category> Children  { get; set; }
        public virtual int DisplayOrder { get; set; }
        public virtual ICollection<Product> Products  { get; set; }
        public virtual ICollection<Attribute> Attributes { get; set; }
        public virtual string Description { get; set; }
        public virtual string KeyWords { get; set; }
        public virtual int DiscountPercent { get; set; }
        public virtual byte[] RowVersion { get; set; }

    }
}
