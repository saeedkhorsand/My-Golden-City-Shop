using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataLayer.Context;
using DomainClasses.Entities;
using EFSecondLevelCache;
using EntityFramework.Extensions;
using ServiceLayer.Interfaces;
using ViewModel.Admin.Setting;

namespace ServiceLayer.EFServices
{
    public class SlideShowService : ISlideShowService
    {
        #region Fields

        private readonly IUnitOfWork _unitOfWork;
        private readonly IDbSet<SlideShow> _slideShows;
        #endregion

        #region Constructor

        public SlideShowService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _slideShows = _unitOfWork.Set<SlideShow>();
        }
        #endregion


        public void Delete(long id)
        {
            _slideShows.Where(a => a.Id == id).Delete();
        }

        public IEnumerable<DomainClasses.Entities.SlideShow> List()
        {
            return _slideShows.OrderByDescending(a => a.Id).Cacheable().ToList();
        }

        public void Add(ViewModel.Admin.Setting.AddSlideShowViewModel viewModel)
        {
            var slide = new SlideShow
            {
                Link = viewModel.Link,
                ImageAltText = viewModel.ImageAltText,
                ImagePath = viewModel.ImagePath,
                Title = viewModel.Title,
                Description = viewModel.Description,
                DataHorizontal = viewModel.DataHorizontal,
                HideTransition = viewModel.HideTransition,
                Position = viewModel.Position,
                ShowTransition = viewModel.ShowTransition,
                DataVertical = viewModel.DataVertical

            };
            _slideShows.Add(slide);
        }

        public void Update(ViewModel.Admin.Setting.EditSlideShowViewModel viewModel)
        {
            var slide = new SlideShow
            {
                Id = viewModel.Id,
                ImageAltText = viewModel.ImageAltText,
                Link = viewModel.Link,
                ImagePath = viewModel.ImagePath,
                Title = viewModel.Title,
                Description = viewModel.Description,
                DataHorizontal = viewModel.DataHorizontal,
                HideTransition = viewModel.HideTransition,
                Position = viewModel.Position,
                ShowTransition = viewModel.ShowTransition,
                DataVertical = viewModel.DataVertical

            };
            _unitOfWork.MarkAsChanged(slide);
        }

        public ViewModel.Admin.Setting.EditSlideShowViewModel GetByIdForEdit(long id)
        {
            return _slideShows.Where(a => a.Id == id).Select(a => new EditSlideShowViewModel
            {
                Id = a.Id,
                ImageAltText = a.ImageAltText,
                ImagePath = a.ImagePath,
                Title = a.Title,
                Link = a.Link,
                Description = a.Description,
                DataHorizontal = a.DataHorizontal,
                HideTransition = a.HideTransition,
                Position = a.Position,
                ShowTransition = a.ShowTransition,
                DataVertical = a.DataVertical

            }).FirstOrDefault();
        }

        public bool AllowAdd()
        {
            return _slideShows.Count() <= 10;
        }
    }
}
