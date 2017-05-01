using Microsoft.AspNet.Identity.EntityFramework;

namespace WebMarket.DomainClasses
{
    public class CustomRole : IdentityRole<int, CustomUserRole>
    {
        public CustomRole() { }
        public CustomRole(string name) { Name = name; }


    }
}
