using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks;
using DomainClasses.Enums;

namespace DomainClasses.Entities
{
    public class Page
    {
        public virtual long Id { get; set; }
        public virtual string Content { get; set; }
        public virtual string Title { get; set; }
        public virtual string ImagePath { get; set; }
        public virtual string Description { get; set; }
        public virtual string KeyWords { get; set; }
        public virtual int DisplayOrder { get; set; }
        public virtual byte[] RowVersion { get; set; }
        public virtual PageShowPlace PageShowPlace { get; set; }
    }
}
