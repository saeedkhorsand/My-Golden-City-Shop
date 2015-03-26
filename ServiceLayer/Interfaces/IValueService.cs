using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DomainClasses.Entities;
using ViewModel.Admin.Product;

namespace ServiceLayer.Interfaces
{
    public interface IValueService
    {
        void Insert(Value value);
        void AddCategoryAttributesToProduct(IEnumerable<DomainClasses.Entities.Attribute> attributes, long productId);
        IEnumerable<FillProductAttributesViewModel> GetForUpdateValuesByProductId(long productId);
        void UpdateValues(IEnumerable<FillProductAttributesViewModel> values);

        void RemoveByProductId(long productId);
        IEnumerable<AttributeValueViewModel> GetProductProperties(long id);
        string[] GetAttValueOfProduct(long productId);
    }
}
