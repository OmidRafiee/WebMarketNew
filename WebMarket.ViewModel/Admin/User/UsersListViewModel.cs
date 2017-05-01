using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebMarket.ViewModel.Admin.User
{
   public class UsersListViewModel
    {
        public IEnumerable<DetailsUserViewModel> UsersList { get; set; }
        public int PageCount { get; set; }
        public DomainClasses.Enums.Order Order { get; set; }
        public string Term { get; set; }
        public DomainClasses.Enums.UserOrderBy UserOrderBy { get; set; }
        public int PageNumber { get; set; }
        public int TotalUsers { get; set; }
    }
}
