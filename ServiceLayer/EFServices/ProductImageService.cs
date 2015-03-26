using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.UI.WebControls;
using DataLayer.Context;
using DomainClasses.Entities;
using ServiceLayer.Interfaces;

namespace ServiceLayer.EFServices
{
    public class ProductImageService : IProductImageService
    {
        #region Fields

        private readonly IUnitOfWork _unitOfWork;
        private readonly IDbSet<ProductImage> _productImages;
        #endregion

        #region Constructor

        public ProductImageService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _productImages = _unitOfWork.Set<ProductImage>();
        }
        #endregion
        public void Insert(ViewModel.Admin.Product.AddProductPicturesViewModel viewModel)
        {
            _productImages.Add(new ProductImage
            {
                Path = viewModel.Image1,
                ProductId = viewModel.ProductId
            });
            _productImages.Add(new ProductImage
            {
                Path = viewModel.Image2,
                ProductId = viewModel.ProductId
            });
            _productImages.Add(new ProductImage
            {
                Path = viewModel.Image3,
                ProductId = viewModel.ProductId
            });
            _productImages.Add(new ProductImage
            {
                Path = viewModel.Image4,
                ProductId = viewModel.ProductId
            });
            _productImages.Add(new ProductImage
            {
                Path = viewModel.Image5,
                ProductId = viewModel.ProductId
            });
        }

        public void Edit(IEnumerable<DomainClasses.Entities.ProductImage> images)
        {
            foreach (var image in images)
            {
                _unitOfWork.MarkAsChanged(image);
            }
        }

        public IEnumerable<DomainClasses.Entities.ProductImage> GetImages(long productId)
        {
            return _productImages.Include(a => a.Product).Where(a => a.ProductId == productId).ToList();
        }
    }
}
