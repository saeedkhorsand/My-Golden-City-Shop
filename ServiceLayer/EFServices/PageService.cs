using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper.Internal;
using DataLayer.Context;
using DomainClasses.Entities;
using DomainClasses.Enums;
using EFSecondLevelCache;
using EFSecondLevelCache.Contracts;
using EntityFramework.Extensions;
using ServiceLayer.Interfaces;
using ViewModel.Admin.Page;

namespace ServiceLayer.EFServices
{
    public class PageService : IPageService
    {
        #region Fields

        private readonly IUnitOfWork _unitOfWork;
        private readonly IDbSet<Page> _pages;
        #endregion

        #region Constructor

        public PageService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _pages = _unitOfWork.Set<Page>();
        }
        #endregion

        #region private
        bool ExistByTitle(string title)
        {
            return _pages.Any(a => a.Title == title);
        }
        bool ExistByTitle(string title, long id)
        {
            return _pages.Any(a => a.Id != id && a.Title == title);
        }
        #endregion
        public bool Add(AddPageViewModel viewModel)
        {
            if (ExistByTitle(viewModel.Title)) return false;
            var page = new Page
            {
                Content = viewModel.Content,
                Description = viewModel.Description,
                ImagePath = viewModel.ImagePath,
                KeyWords = viewModel.KeyWords,
                PageShowPlace = viewModel.PageShowPlace,
                Title = viewModel.Title,
                DisplayOrder = viewModel.DisplayOrder
            };
            _pages.Add(page);
            return true;
        }

        public bool Edit(EditPageViewModel viewModel)
        {
            if (ExistByTitle(viewModel.Title, viewModel.Id)) return false;
            var page = GetById(viewModel.Id);
            page.Content = viewModel.Content;
            page.Description = viewModel.Description;
            page.ImagePath = viewModel.ImagePath;
            page.KeyWords = viewModel.KeyWords;
            page.PageShowPlace = viewModel.PageShowPlace;
            page.Title = viewModel.Title;
            page.DisplayOrder = viewModel.DisplayOrder;
            return true;
        }

        public void Delete(long id)
        {
            _pages.Where(a => a.Id == id).Delete();
        }

        public IEnumerable<ViewModel.Admin.Page.PageViewModel> GetDataTable(string term)
        {
            var pages = _pages.AsQueryable();
            if (!string.IsNullOrEmpty(term))
            {
                pages = pages.Where(a => a.Title.Contains(term)).AsQueryable();
            }
            return pages.Select(a => new PageViewModel
            {
                Content = a.Content,
                Id = a.Id,
                Title = a.Title,
                LinkImage = a.ImagePath,
                ShowPlace = a.PageShowPlace == PageShowPlace.Body ? "داخل بدنه" : "فوتر سایت"
            }).ToList();
        }

        public DomainClasses.Entities.Page GetById(long id)
        {
            return _pages.Find(id);
        }

        public EditPageViewModel GetForEditById(long id)
        {
            return _pages.Where(a => a.Id == id).Select(a => new EditPageViewModel
            {
                Id = a.Id,
                ImagePath = a.ImagePath,
                Content = a.Content.Substring(0, 100),
                DisplayOrder = a.DisplayOrder,
                Description = a.Description,
                KeyWords = a.KeyWords,
                Title = a.Title,
                PageShowPlace = a.PageShowPlace
            }).FirstOrDefault();
        }


        public IEnumerable<Page> GetByShowPlace(PageShowPlace showPlace)
        {
            return
                _pages.AsNoTracking()
                    .Where(a => a.PageShowPlace == showPlace)
                    .OrderBy(a => a.DisplayOrder)
                    .Cacheable().ToList();
        }
    }
}
