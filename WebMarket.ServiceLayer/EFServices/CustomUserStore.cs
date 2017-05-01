using Microsoft.AspNet.Identity.EntityFramework;
using WebMarket.DataLayer.Context;
using WebMarket.DomainClasses;
using WebMarket.ServiceLayer.Interfaces;

namespace WebMarket.ServiceLayer.EFServices
{
    public class CustomUserStore :
        UserStore<ApplicationUser, CustomRole, int, CustomUserLogin, CustomUserRole, CustomUserClaim>,
        ICustomUserStore
    {
        //private readonly IDbSet<ApplicationUser> _myUserStore;
        public CustomUserStore(MarketDbContext context)
            : base(context)
        {
            //_myUserStore = context.Set<ApplicationUser>();
        }

        //public override Task<ApplicationUser> FindByIdAsync(int userId)
        //{
        //   return Task.FromResult(_myUserStore.Find(userId));
        //}
    }
}