using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DomainClasses.Entities;
using DomainClasses.Enums;
using ViewModel.Admin.Page;

namespace ServiceLayer.Interfaces
{
    public interface IPageService
    {
        bool Add(AddPageViewModel viewModel);
        bool Edit(EditPageViewModel viewModel);
        void Delete(long id);
        Page GetById(long id);
        EditPageViewModel GetForEditById(long id);
        IEnumerable<Page> GetByShowPlace(PageShowPlace showPlace);
        IEnumerable<PageViewModel> GetDataTable(string term);
    }
}
