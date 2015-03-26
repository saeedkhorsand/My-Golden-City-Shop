using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using DomainClasses.Entities;
using ViewModel.Admin.Setting;

namespace ServiceLayer.Interfaces
{
    public interface ISlideShowService
    {
        void Add(AddSlideShowViewModel viewModel);
        void Update(EditSlideShowViewModel viewModel);
        void Delete(long id);
        IEnumerable<SlideShow> List();
        EditSlideShowViewModel GetByIdForEdit(long id);
        bool AllowAdd();
    }
}
