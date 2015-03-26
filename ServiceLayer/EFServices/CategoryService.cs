using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using DataLayer.Context;
using DomainClasses.Entities;
using DomainClasses.Enums;
using EFSecondLevelCache;
using EntityFramework.Extensions;
using ServiceLayer.Interfaces;
using ViewModel.Admin;

namespace ServiceLayer.EFServices
{
    public class CategoryService : ICategoryService
    {
        #region Fields
        private readonly IUnitOfWork _unitOfWork;
        private readonly IDbSet<Category> _categories;
        #endregion

        #region Constructor

        public CategoryService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _categories = _unitOfWork.Set<Category>();
        }
        #endregion

        #region  Methods
        public bool CheckExistName(string name)
        {
            return _categories.Any(category => category.Name == name);
        }
        public bool CheckExistName(string name, long id)
        {
            return _categories.Any(category => category.Id != id && category.Name == name);
        }
        #endregion

        #region CRUD
        public IEnumerable<Category> GetSecondLevelCategories()
        {
            return
                _categories.AsNoTracking()
                    .Where(a => a.ParentId != null)
                    .ToList();
        }
        public IEnumerable<Category> GetAll()
        {
            return _categories.AsNoTracking().ToList();
        }

        public Category GetCategoryWithChildrenById(long id)
        {
            return _categories.AsNoTracking()
                .Include(a => a.Children).FirstOrDefault(a => a.Id == id);
        }

        public EditCategoryViewModel GetForEdit(long id)
        {

            return _categories.Where(a => a.Id == id).Select(category =>
                new EditCategoryViewModel
                {
                    Id = category.Id,
                    Description = category.Description,
                    Name = category.Name,
                    DiscountPercent = category.DiscountPercent,
                    DisplayOrder = category.DisplayOrder,
                    KeyWords = category.KeyWords
                }).FirstOrDefault();
        }

        public Category GetById(long id)
        {
            return _categories.Find(id);
        }

        public Category GetByName(string name)
        {
            throw new NotImplementedException();
        }

        public IList<Category> GetFirstLevelCategories()
        {
            return
                _categories.AsNoTracking()
                    .Where(a => a.ParentId == null)
                    .ToList();
        }

        public IEnumerable<ShowCatetoryViewModel> GetDataTable(out int total, string term, int page, int count = 10)
        {
            var selectedCategories = _categories.AsNoTracking().OrderBy(a=>a.Id).AsQueryable();
            if (!string.IsNullOrEmpty(term))
            {
                selectedCategories = selectedCategories.Where(a => a.Name.Contains(term));
            }

            var totalQuery = selectedCategories.FutureCount();
            var query = selectedCategories.Skip((page - 1) * count).Take(count).Select(a => new ShowCatetoryViewModel
            {
                Name = a.Name,
                Id = a.Id,
                DiscountPercent = a.DiscountPercent
            }).Future();
            total = totalQuery.Value;
            var categories = query.ToList();
            return categories;
        }
        public void Delete(long id)
        {
            var category = GetById(id);
            _unitOfWork.MarkAsDeleted(category);
        }
        public AddCategoryStatus Add(Category category)
        {
            if (CheckExistName(category.Name)) return AddCategoryStatus.CategoryNameExist;

            _categories.Add(category);
            return AddCategoryStatus.AddSuccessfully;
        }

        public EditCategoryStatus Edit(EditCategoryViewModel category)
        {
            if (CheckExistName(category.Name, category.Id)) return EditCategoryStatus.CategoryNameExist;
            var selectedCategory = GetById(category.Id);
            selectedCategory.Description = category.Description;
            selectedCategory.KeyWords = category.KeyWords;
            selectedCategory.Name = category.Name;
            selectedCategory.DisplayOrder = category.DisplayOrder;
            selectedCategory.DiscountPercent = category.DiscountPercent;
            selectedCategory.Id = category.Id;
            return EditCategoryStatus.EditSuccessfully;
        }
        public IEnumerable<Category> GetByParenId(long parentId)
        {
            return
               _categories.AsNoTracking()
                   .Where(a => a.ParentId == parentId)
                   .ToList();
        }
        #endregion

        #region Attributes
        public bool HasAttributeByName(string attributeName, long id)
        {
            return
                _categories.Include(a => a.Attributes)
                    .Any(a => a.Id == id && a.Attributes.Any(b => b.Name == attributeName));
        }
        #endregion

        #region Menu
        public IEnumerable<Category> GetCategoriesForMenu()
        {
            return
                _categories.AsNoTracking().Include(a => a.Children).Where(a => a.ParentId == null)
                    .Cacheable().ToList();
        }

        #endregion

    }
}
