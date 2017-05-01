using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebMarket.ViewModel.Admin.User
{
    public class DetailsUserViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Family { get; set; }
        public IEnumerable<string> RoleNames { get; set; }

        public string RoleName
        {
            get
            {
                return string.Join ( "," ,RoleNames ?? new string [ ] { } );
            }
        }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string RegisterDate { get; set; }
        public string LastLoginDate { get; set; }
        
    }
}
