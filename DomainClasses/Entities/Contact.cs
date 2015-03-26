using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DomainClasses.Enums;

namespace DomainClasses.Entities
{
    public class Contact
    {
        public virtual long Id { get; set; }
        public virtual string Title { get; set; }
        public virtual string Content { get; set; }
        public virtual User User { get; set; }
        public virtual DateTime Date { get; set; }
        public virtual byte[] RowVersion { get; set; }
        public virtual bool IsSeen { get; set; }
        public ContactType Type { get; set; }
    }
}
