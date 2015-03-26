using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DomainClasses.Enums;

namespace ViewModel.Admin
{
    public class UsersListViewModel
    {
        public IEnumerable<UserViewModel> UsersList { get; set; }
        public int PageCount { get; set; }
        public DomainClasses.Enums.Order Order { get; set; }
        public string Term { get; set; }
        public UserOrderBy UserOrderBy { get; set; }
        public int PageNumber { get; set; }
        public int TotalUsers { get; set; }
    }
}
