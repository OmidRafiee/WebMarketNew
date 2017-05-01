
using Microsoft.AspNet.Identity;
using WebMarket.DomainClasses;
using WebMarket.ServiceLayer.Interfaces;

namespace WebMarket.ServiceLayer.EFServices
{
    public class CustomRoleStore : ICustomRoleStore
    {
        private readonly IRoleStore<CustomRole, int> _roleStore;

        public CustomRoleStore(IRoleStore<CustomRole, int> roleStore)
        {
            _roleStore = roleStore;
        }
    }
}