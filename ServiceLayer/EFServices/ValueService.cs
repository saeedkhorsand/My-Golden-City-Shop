using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using DataLayer.Context;
using DomainClasses.Entities;
using EntityFramework.Extensions;
using ServiceLayer.Interfaces;
using ViewModel.Admin.Product;
using Attribute = DomainClasses.Entities.Attribute;

namespace ServiceLayer.EFServices
{
    public class ValueService : IValueService
    {
        #region Fields

        private readonly IDbSet<Value> _values;
        #endregion

        #region Constructor

        public ValueService(IUnitOfWork unitOfWork)
        {
            _values = unitOfWork.Set<Value>();
        }

        #endregion

        #region CRUD
        public void Insert(Value value)
        {
            throw new NotImplementedException();
        }

        public void AddCategoryAttributesToProduct(IEnumerable<Attribute> attributes, long productId)
        {
            foreach (var attribute in attributes)
            {
                _values.Add(new Value
                {
                    Content = "----",
                    AttributeId = attribute.Id,
                    ProductId = productId
                });
            }
        }
        public IEnumerable<FillProductAttributesViewModel> GetForUpdateValuesByProductId(long productId)
        {
            return _values.Include(a => a.Product).Include(a => a.Attribute).Where(a => a.ProductId == productId).Select(a => new FillProductAttributesViewModel
            {
                Name = a.Attribute.Name,
                Id = a.Id,
                Value = a.Content
            }).ToList();
        }

        public void UpdateValues(IEnumerable<FillProductAttributesViewModel> values)
        {
            if (values == null) return;
            var fillProductAttributesViewModels = values as IList<FillProductAttributesViewModel> ?? values.ToList();
            var ids = fillProductAttributesViewModels.Select(a => a.Id);
            var selectedValues = _values.Where(a => ids.Contains(a.Id)).ToList();
            for (var i = 0; i < selectedValues.Count; i++)
            {
                selectedValues.ElementAt(i).Content = fillProductAttributesViewModels.ElementAt(i).Value;
            }
        }


        public void RemoveByProductId(long productId)
        {
            _values.Where(a => a.ProductId == productId).Delete();
        }


        public string[] GetAttValueOfProduct(long productId)
        {
            return _values.Where(a => a.ProductId == productId).Select(a => a.Attribute.Name + ":" + a.Content).ToArray();
        }


        public IEnumerable<AttributeValueViewModel> GetProductProperties(long id)
        {
            return
                _values.Include(a => a.Attribute)
                    .Include(a => a.Product)
                    .Where(a => a.ProductId == id)
                    .Select(a => new AttributeValueViewModel
                    {
                        Name = a.Attribute.Name,
                        Value = a.Content
                    }).ToList();
        }
        #endregion
    }
}
