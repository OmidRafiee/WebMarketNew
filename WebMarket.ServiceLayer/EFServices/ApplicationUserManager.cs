using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Data.Entity;
using System.Linq;
using System.Security.Claims;
using System.Security.Principal;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security.Cookies;
using Microsoft.Owin.Security.DataProtection;
using WebMarket.DomainClasses;
using WebMarket.DataLayer.Context;
using WebMarket.ServiceLayer.Interfaces;
using WebMarket.DomainClasses.Enums;
using WebMarket.ViewModel.Admin.User;

namespace WebMarket.ServiceLayer.EFServices
{
    using WebMarket.Utilities;

    public class ApplicationUserManager
        : UserManager<ApplicationUser, int>, IApplicationUserManager
    {
        private readonly IDataProtectionProvider _dataProtectionProvider;
        private readonly IApplicationRoleManager _roleManager;
        private readonly IUserStore<ApplicationUser, int> _store;
        private readonly IUnitOfWork _uow;
        private readonly IDbSet<ApplicationUser> _users;
        private readonly IIdentity _identity;
        private ApplicationUser _user;

        public ApplicationUserManager(IUserStore<ApplicationUser, int> store,
            IUnitOfWork uow,
            IIdentity identity,
            IApplicationRoleManager roleManager,
            IDataProtectionProvider dataProtectionProvider,
            IIdentityMessageService smsService,
            IIdentityMessageService emailService)
            : base(store)
        {
            _store = store;
            _uow = uow;
            _identity = identity;
            _users = _uow.Set<ApplicationUser>();
            _roleManager = roleManager;
            _dataProtectionProvider = dataProtectionProvider;
            this.SmsService = smsService;
            this.EmailService = emailService;


            CreateApplicationUserManager();
        }

        public ApplicationUser FindById(int userId)
        {
            return _users.Find(userId);
        }

        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(ApplicationUser applicationUser)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await CreateIdentityAsync(applicationUser, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here
            return userIdentity;
        }

        public Task<List<ApplicationUser>> GetAllUsersAsync()
        {
            return this.Users.ToListAsync();
        }

        public ApplicationUser GetCurrentUser()
        {
            return _user ?? (_user = this.FindById(GetCurrentUserId()));
        }

        public async Task<ApplicationUser> GetCurrentUserAsync()
        {
            return _user ?? (_user = await this.FindByIdAsync(GetCurrentUserId()));
        }

        public int GetCurrentUserId()
        {
            return _identity.GetUserId<int>();
        }

        public async Task<bool> HasPassword(int userId)
        {
            var user = await FindByIdAsync(userId);
            return user != null && user.PasswordHash != null;
        }

        public async Task<bool> HasPhoneNumber(int userId)
        {
            var user = await FindByIdAsync(userId);
            return user != null && user.PhoneNumber != null;
        }

        public Func<CookieValidateIdentityContext, Task> OnValidateIdentity()
        {
            return SecurityStampValidator.OnValidateIdentity<ApplicationUserManager, ApplicationUser, int>(
                         validateInterval: TimeSpan.FromMinutes(30),
                         regenerateIdentityCallback: (manager, user) => generateUserIdentityAsync(manager, user),
                         getUserIdCallback: id => id.GetUserId<int>());
        }

        public void SeedDatabase()
        {
            const string userName = "demo";
            const string email = "demo@email.com";
            const string password = "123456";
            const string roleName = "Admin";

            //Create Role Admin if it does not exist
            var role = _roleManager.FindRoleByName(roleName);
            if (role == null)
            {
                role = new CustomRole(roleName);
                var roleresult = _roleManager.CreateRole(role);
            }

            var user = this.FindByName(userName);

            if (user == null)
            {
                user = new ApplicationUser
                       {
                           UserName = userName,
                           Email = email,
                           EmailConfirmed = true,
                           Name = "امید",
                           Family = "رفیعی",
                           RegisterDate = DateTime.Now
                       };
                var result = this.Create(user, password);
                result = this.SetLockoutEnabled(user.Id, false);
            }

            // Add user admin to Role Admin if not already added
            var rolesForUser = this.GetRoles(user.Id);
            if (!rolesForUser.Contains(role.Name))
            {
                var result = this.AddToRole(user.Id, role.Name);
            }
        }

        private void CreateApplicationUserManager()
        {
            // Configure validation logic for usernames
            this.UserValidator = new UserValidator<ApplicationUser, int>(this)
            {
                AllowOnlyAlphanumericUserNames = false,
                RequireUniqueEmail = true
            };

            // Configure validation logic for passwords
            this.PasswordValidator = new PasswordValidator
            {
                RequiredLength = 6,
                RequireNonLetterOrDigit = false,
                RequireDigit = true,
                RequireLowercase = false,
                RequireUppercase = false,
            };

            // Configure user lockout defaults
            this.UserLockoutEnabledByDefault = true;
            this.DefaultAccountLockoutTimeSpan = TimeSpan.FromMinutes(5);
            this.MaxFailedAccessAttemptsBeforeLockout = 5;

            // Register two factor authentication providers. This application uses Phone and Emails as a step of receiving a code for verifying the user
            // You can write your own provider and plug in here.
            this.RegisterTwoFactorProvider("PhoneCode", new PhoneNumberTokenProvider<ApplicationUser, int>
            {
                MessageFormat = "Your security code is: {0}"
            });
            this.RegisterTwoFactorProvider("EmailCode", new EmailTokenProvider<ApplicationUser, int>
            {
                Subject = "SecurityCode",
                BodyFormat = "Your security code is {0}"
            });

            if (_dataProtectionProvider != null)
            {
                var dataProtector = _dataProtectionProvider.Create("ASP.NET Identity");
                this.UserTokenProvider = new DataProtectorTokenProvider<ApplicationUser, int>(dataProtector);
            }
        }

        private async Task<ClaimsIdentity> generateUserIdentityAsync(ApplicationUserManager manager, ApplicationUser applicationUser)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(applicationUser, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here
            return userIdentity;
        }



        /////////////my Custom method//////////////

        public IEnumerable<string> GetAllEmail()
        {
            return this.Users.Select(p => p.Email);
        }

        public IEnumerable<string> GetAllUserName()
        {
            return this.Users.Select(p => p.UserName);
        }


        public IList<DetailsUserViewModel> GetDataTable(out int total, string term, int page, int count, DomainClasses.Enums.Order order, UserOrderBy orderBy, UserSearchBy searchBy)
        {
            var selectedUsers = _users.AsNoTracking().Include(a => a.Roles).AsQueryable();

            //var selectedRole = _roleManager.FindRoleByName("");

            if (!string.IsNullOrEmpty(term))
            {
                switch (searchBy)
                {
                    case UserSearchBy.UserName:
                        selectedUsers = selectedUsers.Where(user => user.UserName.Contains(term)).AsQueryable();
                        break;
                    //case UserSearchBy.RoleDescription:
                    //    selectedUsers = selectedUsers.Where(user => user.Role.Description.Contains(term)).AsQueryable();
                    //    break;
                    case UserSearchBy.PhoneNumber:
                        selectedUsers =
                            selectedUsers.Where(user => user.PhoneNumber.Contains(term)).AsQueryable();
                        break;

                }
            }


            if (order == Order.Asscending)
            {
                switch (orderBy)
                {
                    case UserOrderBy.UserName:
                        selectedUsers = selectedUsers.OrderBy(user => user.UserName).AsQueryable();
                        break;
                    case UserOrderBy.PhoneNumber:
                        selectedUsers = selectedUsers.OrderBy(user => user.PhoneNumber).AsQueryable();
                        break;
                    case UserOrderBy.RoleDescription:
                        selectedUsers = selectedUsers.OrderBy(user => user.RegisterDate).AsQueryable();
                        break;
                }
            }
            else
            {
                switch (orderBy)
                {
                    case UserOrderBy.UserName:
                        selectedUsers = selectedUsers.OrderByDescending(user => user.UserName).AsQueryable();
                        break;
                    case UserOrderBy.PhoneNumber:
                        selectedUsers = selectedUsers.OrderBy(user => user.PhoneNumber).AsQueryable();
                        break;
                    case UserOrderBy.RoleDescription:
                        selectedUsers = selectedUsers.OrderByDescending(user => user.RegisterDate).AsQueryable();
                        break;
                }
            }

            var totalQuery = selectedUsers.Count();
            var selectQuery = selectedUsers.Skip((page - 1) * count).Take(count)
                                         .ProjectTo<DetailsUserViewModel>(Market.AutoMapperConfig.Configuration.MapperConfiguration);
            total = totalQuery;
            return selectQuery.ToList();
        }

        public DetailsUserViewModel  GetUserDetail(int id)
        {
            return _users.Where ( p => p.Id == id )
                          .ProjectTo < DetailsUserViewModel > ( Market.AutoMapperConfig.Configuration.MapperConfiguration )
                          .FirstOrDefault ();
        }

        public void Remove(long id)
        {
            var user = _users.Where (p=>p.Id== id);
            _uow.MarkAsDeleted(user);
            _uow.SaveChanges();
        }
    }
}

