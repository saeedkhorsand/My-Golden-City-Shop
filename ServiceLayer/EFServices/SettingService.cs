using System;
using System.Data.Entity;
using System.Linq;
using DataLayer.Context;
using DomainClasses.Entities;
using EFSecondLevelCache;
using ServiceLayer.Interfaces;
using ViewModel.Admin;

namespace ServiceLayer.EFServices
{
    public class SettingService : ISettingService
    {
        #region Fields

        private readonly IDbSet<SiteOption> _options;
        #endregion

        #region Consructor

        public SettingService(IUnitOfWork unitOfWork)
        {
            _options = unitOfWork.Set<SiteOption>();
        }

        #endregion
        public void Update(EditSettingViewModel viewModel)
        {
            var settings = _options.ToList();
            settings.Where(a => a.Name.Equals("StoreName")).FirstOrDefault().Value = viewModel.StorName;
            settings.Where(a => a.Name.Equals("StoreKeyWords")).FirstOrDefault().Value = viewModel.StoreKeyWords;
            settings.Where(a => a.Name.Equals("StoreDescription")).FirstOrDefault().Value = viewModel.StoreDescription;
            settings.Where(a => a.Name.Equals("Tel1")).FirstOrDefault().Value = viewModel.Tel1;
            settings.Where(a => a.Name.Equals("Tel2")).FirstOrDefault().Value = viewModel.Tel2;
            settings.Where(a => a.Name.Equals("PhoneNumber1")).FirstOrDefault().Value = viewModel.PhoneNumber1;
            settings.Where(a => a.Name.Equals("PhoneNumber2")).FirstOrDefault().Value = viewModel.PhoneNumber2;
            settings.Where(a => a.Name.Equals("CommentModeratorStatus")).FirstOrDefault().Value =
                Convert.ToString(viewModel.CommentModeratorStatus);
            settings.Where(a => a.Name.Equals("Address")).FirstOrDefault().Value = viewModel.Address;
        }

        public EditSettingViewModel GetOptionsForEdit()
        {
            var settings = _options.ToList();
            var model = new EditSettingViewModel
            {
                StorName = settings.Where(a => a.Name.Equals("StoreName")).FirstOrDefault().Value,
                StoreKeyWords = settings.Where(a => a.Name.Equals("StoreKeyWords")).FirstOrDefault().Value,
                StoreDescription = settings.Where(a => a.Name.Equals("StoreDescription")).FirstOrDefault().Value,
                Tel1 = settings.Where(a => a.Name.Equals("Tel1")).FirstOrDefault().Value,
                Tel2 = settings.Where(a => a.Name.Equals("Tel2")).FirstOrDefault().Value,
                PhoneNumber1 = settings.Where(a => a.Name.Equals("PhoneNumber1")).FirstOrDefault().Value,
                PhoneNumber2 = settings.Where(a => a.Name.Equals("PhoneNumber2")).FirstOrDefault().Value,
                CommentModeratorStatus =
                    Convert.ToBoolean(settings.Where(a => a.Name.Equals("CommentModeratorStatus")).FirstOrDefault().Value),
                Address = settings.Where(a => a.Name.Equals("Address")).FirstOrDefault().Value
            };
            return model;

        }

        public bool CommentMangement()
        {
            throw new NotImplementedException();
        }


        public EditSettingViewModel GetOptionsForShowOnFooter()
        {
            var settings = _options.Cacheable().ToList();
            var model = new EditSettingViewModel
            {
                StorName = settings.Where(a => a.Name.Equals("StoreName")).FirstOrDefault().Value,
                StoreKeyWords = settings.Where(a => a.Name.Equals("StoreKeyWords")).FirstOrDefault().Value,
                StoreDescription = settings.Where(a => a.Name.Equals("StoreDescription")).FirstOrDefault().Value,
                Tel1 = settings.Where(a => a.Name.Equals("Tel1")).FirstOrDefault().Value,
                Tel2 = settings.Where(a => a.Name.Equals("Tel2")).FirstOrDefault().Value,
                PhoneNumber1 = settings.Where(a => a.Name.Equals("PhoneNumber1")).FirstOrDefault().Value,
                PhoneNumber2 = settings.Where(a => a.Name.Equals("PhoneNumber2")).FirstOrDefault().Value,
                CommentModeratorStatus =
                    Convert.ToBoolean(settings.Where(a => a.Name.Equals("CommentModeratorStatus")).FirstOrDefault().Value),
                Address = settings.Where(a => a.Name.Equals("Address")).FirstOrDefault().Value
            };
            return model;
        }
    }
}
