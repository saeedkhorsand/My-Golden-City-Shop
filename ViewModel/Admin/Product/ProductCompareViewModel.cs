using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViewModel.Admin.Product
{
   public class ProductCompareViewModel
    {
       public long ProductId { get; set; }
       public string ProductName { get; set; }
       public int TotalRaters { get; set; }
       public double?  AvrageRate { get; set; }
       public string Description { get; set; }
       public string[] Attributes { get; set; }
       public long Price { get; set; }
       public int Discount { get; set; }
       public string ImagePath { get; set; }
       public bool FreeSend { get; set; }
    }
}
