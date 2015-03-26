using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ViewModel.Admin;
using ViewModel.Admin.Product;

namespace ServiceLayer.Interfaces
{
    public interface IAttributeService
    {
        void Delete(long id);
        bool Add(AddAttributeViewModel viewModel);
        void EditByCategoryId(string oldName, string newName, long categoryId);
        bool ExisByName(string name, long categoryId);
        bool ExisByName(string name, long id, long categoryId);
        IEnumerable<DomainClasses.Entities.Attribute> GetAttributesByCategoryId(long id);
        DomainClasses.Entities.Attribute GetAttributeById(long id);
        bool Edit(DomainClasses.Entities.Attribute attribute);
        void Add(DomainClasses.Entities.Attribute attribute);
        void DeleteByCategoryIdAndAttribueName(long categoryId, string attributeName);
        void AddParentAttributeToChild(long? parentId, long categoryId);

    }
}
