using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViewModel.Admin
{
    public class UserViewModel
    {
        public long Id { get; set; }
        public string UserName { get; set; }
        public string FullName { get; set; }
        public string PhoneNumber { get; set; }
        public int CommentCount { get; set; }
        public int OrderCount { get; set; }
        public string  RoleDescritpion { get; set; }
        public bool Baned  { get; set; }
        public string RegisterType { get; set; }
    }
}
