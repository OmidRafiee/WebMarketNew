using Microsoft.AspNet.Identity.EntityFramework;


namespace WebMarket.DomainClasses
{
    public class ApplicationUser : IdentityUser<int, CustomUserLogin, CustomUserRole, CustomUserClaim>
    {
        public string Name { get; set; }
        public string Family { get; set; }
        public System.DateTime? BirthDate { get; set; }
        public System.DateTime RegisterDate { get; set; }
        public System.DateTime? LastLoginDate { get; set; }
        public bool Gender { get; set; }



        public virtual System.Collections.Generic.ICollection<Entity.Factor> Factors { get; set; }
        public virtual System.Collections.Generic.ICollection<Entity.FactorItem> FactorItems { get; set; }
    }
}
