using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViewModel.Admin.Product
{
    public class ProductSectionViewModel
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string PrincipleImagePath { get; set; }
        public bool IsFreeShipping { get; set; }
        public bool IsInStock { get; set; }
        [DisplayFormat(DataFormatString = "{0:0.#}", ApplyFormatInEditMode = true)]
        public decimal SellCount { get; set; }
        public int ViewCount { get; set; }
        [DisplayFormat(DataFormatString = "{0:###,###.####}", ApplyFormatInEditMode = true)]
        public long Price { get; set; }
        [DisplayFormat(DataFormatString = "{0:0.#}", ApplyFormatInEditMode = true)]
        public decimal Ratio { get; set; }
        public int TotalRaters { get; set; }
        public double? AvrageRate { get; set; }
        [DisplayFormat(DataFormatString = "{0:0.#}", ApplyFormatInEditMode = true)]
        public decimal TotalDiscount { get; set; }

    }
}
