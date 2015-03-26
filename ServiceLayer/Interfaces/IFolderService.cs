using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DomainClasses.Entities;
using ViewModel.Admin.Folder;

namespace ServiceLayer.Interfaces
{
    public interface IFolderService
    {
        bool Add(AddFolderViewModel viewModel);
        void Delete(long id);
        bool Edit(EditFolderViewModel viewModel);
        EditFolderViewModel GetForEdit(long id);
        IEnumerable<Folder> GetList();
        bool CheckNameExist(string name);
        bool CheckNameExist(string name, long id);
    }
}
