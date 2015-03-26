using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DomainClasses.Enums;

namespace DomainClasses.Entities
{
    public class Order
    {
        public virtual long Id { get; set; }
        public virtual string Address { get; set; }
        public virtual ICollection<OrderDetail> OrderDetails { get; set; }
        public virtual User Buyer { get; set; }
        public virtual DateTime BuyDate { get; set; }
        public virtual DateTime? PostedDate { get; set; }   
        public virtual OrderStatus Status { get; set; }
        public virtual string TransactionCode { get; set; }
        public PeymentType PeymentType { get; set; }
        public virtual decimal? DiscountPrice { get; set; }
        public virtual byte[] RowVersion { get; set; }
        public virtual decimal TotalPrice { get; set; }

    }
}
