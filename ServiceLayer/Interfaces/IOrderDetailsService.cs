using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DomainClasses.Entities;

namespace ServiceLayer.Interfaces
{
    public interface IOrderDetailsService
    {
        void Add(OrderDetail orderDetail);
        void Delete(long id);
        void DeleteByOrderId(long orderId);
        void Update(OrderDetail orderDetail);
        IEnumerable<OrderDetail> GetListByOrderId(long orderId);
        IEnumerable<OrderDetail> GetList();
        IEnumerable<OrderDetail> GetDataTable(out int total, int page, int count);
        OrderDetail GetById(long id);
    }
}
