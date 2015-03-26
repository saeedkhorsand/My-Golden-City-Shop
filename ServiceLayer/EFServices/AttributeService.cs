using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using DataLayer.Context;
using DomainClasses.Entities;
using DomainClasses.Enums;
using EntityFramework.Extensions;
using ServiceLayer.Interfaces;
using ViewModel.Admin;

namespace ServiceLayer.EFServices
{
    public class AttributeService : IAttributeService
    {
        #region Fields

        private readonly IUnitOfWork _unitOfWork;
        private readonly IDbSet<Attribute> _attributes;
        #endregion

        #region Constructor

        public AttributeService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _attributes = _unitOfWork.Set<Attribute>();
        }
        #endregion

        #region Methods
        public bool ExisByName(string name, long categoryId)
        {
            return _attributes.Any(a => a.CategoryId == categoryId && a.Name.Equals(name));
        }
        public bool ExisByName(string name, long id, long categoryId)
        {
            return _attributes.Any(a => a.CategoryId == categoryId && a.Id != id && a.Name.Equals(name));
        }
        #endregion

        #region CRUD
        public void Delete(long id)
        {
            _attributes.Where(a => a.Id == id).Delete();
        }

        public bool Add(AddAttributeViewModel viewModel)
        {
            if (ExisByName(viewModel.Name, viewModel.CategoryId)) return false;
            var attribute = new Attribute
            {
                Name = viewModel.Name,
                CategoryId = viewModel.CategoryId,
                Type = AttributeType.Text
            };
            _attributes.Add(attribute);
            return true;
        }
        public bool Edit(Attribute attribute)
        {
            if (ExisByName(attribute.Name, attribute.Id, attribute.CategoryId)) return false;
            _unitOfWork.MarkAsChanged(attribute);
            return true;
        }
        public IEnumerable<Attribute> GetAttributesByCategoryId(long id)
        {
            return _attributes.AsNoTracking().Where(a => a.CategoryId == id).ToList();
        }

        public Attribute GetAttributeById(long id)
        {
            return _attributes.Find(id);
        }
        public void AddParentAttributeToChild(long? parentId, long categoryId)
        {
            if (parentId == null) return;
            var categoryAttributes = GetAttributesByCategoryId(parentId.Value);
            var attributes = categoryAttributes as IList<Attribute> ?? categoryAttributes.ToList();
            if (!attributes.Any()) return;
            foreach (var attribute in attributes)
            {
                Add(new Attribute
                {
                    Name = attribute.Name,
                    CategoryId = categoryId,
                    Type = attribute.Type
                });
            }
        }

        public void Add(Attribute attribute)
        {
            _attributes.Add(attribute);
        }

        public void EditByCategoryId(string oldName, string newName, long categoryId)
        {
            var attribute = _attributes.Where(a => a.CategoryId == categoryId && a.Name.Equals(oldName));
            attribute.Update(a => new Attribute { Name = newName });
        }

        public void DeleteByCategoryIdAndAttribueName(long categoryId, string attributeName)
        {
            _attributes.Where(a => a.CategoryId == categoryId && a.Name == attributeName).Delete();
        }
        #endregion

    }
}
