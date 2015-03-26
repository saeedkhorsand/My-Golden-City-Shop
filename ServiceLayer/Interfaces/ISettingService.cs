using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DomainClasses.Entities;
using ViewModel.Admin;

namespace ServiceLayer.Interfaces
{
    public interface ISettingService
    {
        void Update(EditSettingViewModel viewModel);
        EditSettingViewModel GetOptionsForEdit();
        EditSettingViewModel GetOptionsForShowOnFooter();
        bool CommentMangement();
    }

}
