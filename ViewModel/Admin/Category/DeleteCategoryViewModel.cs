using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViewModel.Admin
{
   public class DeleteCategoryViewModel
    {
       public long Id { get; set; }
       public string Name { get; set; }
       public long ReplaceCategoryIdForProducts { get; set; }
    }
}
