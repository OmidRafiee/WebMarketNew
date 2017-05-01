using Microsoft.AspNet.Identity.EntityFramework;

namespace WebMarket.DomainClasses
{
    public class CustomUserRole : IdentityUserRole<int>
    {

        public virtual CustomRole Role { get; set; }
        public virtual ApplicationUser User { get; set; }
    }
}
