using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DomainClasses.Entities;

namespace ServiceLayer.Interfaces
{
    public interface IOrderService
    {
        void Add(Order order);
        void Delete(long id);
        void Update(Order order);
        IEnumerable<Order> GetListByUserId(long UserId);
        IEnumerable<Order> GetListById(long id);
        IEnumerable<Order> DataTable(out int total, int page, int count);
    }
}
