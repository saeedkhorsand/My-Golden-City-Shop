using DomainClasses.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViewModel.Admin.Product
{
    public class ProductListViewModel
    {
        public IEnumerable<ProductViewModel> ProductList { get; set; }
        public int PageCount { get; set; }
        public DomainClasses.Enums.Order Order { get; set; }
        public bool FreeSend { get; set; }
        public bool Deleted { get; set; }
        public string Term { get; set; }
        public ProductOrderBy ProductOrderBy { get; set; }
        public ProductType ProductType { get; set; }
        public long CategoryId { get; set; }
        public int PageNumber { get; set; }
        public int TotalProducts { get; set; }
    }
}
