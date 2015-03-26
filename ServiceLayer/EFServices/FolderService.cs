using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using DataLayer.Context;
using DomainClasses.Entities;
using EntityFramework.Extensions;
using ServiceLayer.Interfaces;
using ViewModel.Admin.Folder;

namespace ServiceLayer.EFServices
{
    public class FolderService : IFolderService
    {
        #region Fields

        private readonly IUnitOfWork _unitOfWork;
        private readonly IDbSet<Folder> _folders;
        #endregion

        #region Constructor

        public FolderService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _folders = _unitOfWork.Set<Folder>();
        }
        #endregion

        public bool Add(AddFolderViewModel viewModel)
        {
            if (CheckNameExist(viewModel.Name))
                return false;
            var folder = new Folder
            {
                Name = viewModel.Name
            };
            _folders.Add(folder);
            return true;
        }

        public void Delete(long id)
        {
            _folders.Where(a => a.Id.Equals(id)).Delete();
        }

        public bool Edit(EditFolderViewModel viewModel)
        {
            if (CheckNameExist(viewModel.Name, viewModel.Id))
                return false;
            var folder = new Folder
            {
                Name = viewModel.Name,
                Id = viewModel.Id
            };
            _unitOfWork.MarkAsChanged(folder);
            return true;
        }

        public IEnumerable<Folder> GetList()
        {
            return _folders.AsNoTracking().OrderByDescending(a => a.Id).ToList();
        }

        public bool CheckNameExist(string name)
        {
            return _folders.Any(folder => folder.Name.Equals(name));
        }

        public bool CheckNameExist(string name, long id)
        {
            return _folders.Any(folder => folder.Id != id && folder.Name == name);
        }


        public EditFolderViewModel GetForEdit(long id)
        {
            return _folders.Where(a => a.Id.Equals(id)).Select(a =>
                new EditFolderViewModel
                {
                    Name = a.Name,
                    Id = a.Id
                }).FirstOrDefault();
        }
    }
}
