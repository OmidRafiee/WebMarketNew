using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using WebMarket.DomainClasses;
using WebMarket.ServiceLayer.EFServices;
using WebMarket.ServiceLayer.Interfaces;

namespace WebMarket.ServiceLayer.EFServices
{
    public class ApplicationSignInManager :
        SignInManager<ApplicationUser, int>, IApplicationSignInManager
    {
        private readonly ApplicationUserManager _userManager;
        private readonly IAuthenticationManager _authenticationManager;

        public ApplicationSignInManager(ApplicationUserManager userManager,
                                        IAuthenticationManager authenticationManager) :
            base(userManager, authenticationManager)
        {
            _userManager = userManager;
            _authenticationManager = authenticationManager;
        }
    }
}