using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainClasses.Entities
{
    public class ProductImage
    {
        public virtual long Id { get; set; }

        public virtual Product Product { get; set; }
        public virtual string Path { get; set; }
        [ForeignKey("Product")]
        public virtual long ProductId  { get; set; }
    }
}
