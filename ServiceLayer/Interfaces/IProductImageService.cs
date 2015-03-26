using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DomainClasses.Entities;
using ViewModel.Admin.Product;

namespace ServiceLayer.Interfaces
{
    public interface IProductImageService
    {
        void Insert(AddProductPicturesViewModel viewModel);
        void Edit(IEnumerable<ProductImage> images);
        IEnumerable<ProductImage> GetImages(long productId);
    }
}
