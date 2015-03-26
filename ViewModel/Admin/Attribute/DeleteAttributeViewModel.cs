using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViewModel.Admin
{
   public class DeleteAttributeViewModel
    {
       public long Id { get; set; }
       [DisplayName("نام خصوصیت")]
       public string  Name { get; set; }
       public long  CategoryId { get; set; }
       [DisplayName("حذف ویژگی از گروه های فرزند")]
       public bool CascadeDeleteForChildrenCategory { get; set; }

    }
}
