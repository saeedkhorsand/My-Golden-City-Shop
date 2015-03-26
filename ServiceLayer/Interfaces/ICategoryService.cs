using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DomainClasses.Entities;
using DomainClasses.Enums;
using ViewModel.Admin;

namespace ServiceLayer.Interfaces
{
    public interface ICategoryService
    {
        IEnumerable<Category> GetCategoriesForMenu();
        bool HasAttributeByName(string attributeName,long id);
        IList<Category> GetFirstLevelCategories();
        IEnumerable<Category> GetSecondLevelCategories();
        IEnumerable<ShowCatetoryViewModel> GetDataTable(out int total, string term, int page, int count);
        AddCategoryStatus Add(Category category);
        EditCategoryStatus Edit(EditCategoryViewModel category);
        IEnumerable<Category> GetAll();
        EditCategoryViewModel GetForEdit(long id);
        Category GetCategoryWithChildrenById(long id);
        Category GetById(long id);
        Category GetByName(string name);
        void Delete(long id);
        bool CheckExistName(string name);
        bool CheckExistName(string name, long id);

        IEnumerable<Category> GetByParenId(long parentId);
    }
}
