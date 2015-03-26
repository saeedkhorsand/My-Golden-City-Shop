using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataLayer.Context;
using DomainClasses.Entities;
using EntityFramework.Extensions;
using ServiceLayer.Interfaces;

namespace ServiceLayer.EFServices
{
    class OrderDetailsService:IOrderDetailsService
    {
        #region Fields

        private readonly IUnitOfWork _unitOfWork;
        private readonly IDbSet<OrderDetail> _orderDetails;
      
        #endregion

        #region Constructor

        public OrderDetailsService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _orderDetails = _unitOfWork.Set<OrderDetail>();
        }
        #endregion
        public void Add(DomainClasses.Entities.OrderDetail orderDetail)
        {
            _orderDetails.Add(orderDetail);
        }

        public void Delete(long id)
        {
            _orderDetails.Where(a => a.Id == id).Delete();
        }

        public void DeleteByOrderId(long orderId)
        {
            _orderDetails.Where(a => a.OrderId == orderId).Delete();
        }

        public void Update(DomainClasses.Entities.OrderDetail orderDetail)
        {
            _unitOfWork.MarkAsChanged(orderDetail);
        }


        public IEnumerable<DomainClasses.Entities.OrderDetail> GetListByOrderId(long orderId)
        {
            return _orderDetails.AsNoTracking().Where(a => a.OrderId == orderId).ToList();
        }

        public IEnumerable<DomainClasses.Entities.OrderDetail> GetList()
        {
            return _orderDetails.AsNoTracking().ToList();
        }

        public IEnumerable<DomainClasses.Entities.OrderDetail> GetDataTable(out int total, int page, int count)
        {
            throw new NotImplementedException();
        }

        public DomainClasses.Entities.OrderDetail GetById(long id)
        {
            return _orderDetails.Find(id);
        }
    }
}
