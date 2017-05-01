using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebMarket.ViewModel.Admin.User
{
   public class UserViewModel
   {
       public long Id { get; set; }
       public string UserName { get; set; }
       public string FullName { get; set; }
       public string PhoneNumber { get; set; }
       public string RoleDescritpion { get; set; }
       public DateTime? LastLoginDate  { get; set; }
       public int OrderCount { get; set; }
    }
}
