using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DomainClasses.Enums;

namespace ViewModel.Admin.Product
{
    public class SearchViewModel
    {
        public IEnumerable<ProductSectionViewModel> Products { get; set; }
        public bool IsInStok { get; set; }
        public bool SearchInDescription { get; set; }
        public int Page { get; set; }
        public int Count { get; set; }
        public int Total { get; set; }
        public string Term { get; set; }
        public long CategoryId { get; set; }
        public PSFilter Filter { get; set; }
        public long MinPrice { get; set; }
        public long MaxPrice { get; set; }
    }
}
