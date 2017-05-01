using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Market.AutoMapperConfig
{
    using WebMarket.Utilities;

    public class UserProfile : AutoMapper.Profile
    {
        public UserProfile()
        {
            CreateMap<WebMarket.DomainClasses.ApplicationUser, WebMarket.ViewModel.User.User.UserDataEntry>()
                    .ForMember(model => model.ConfirmPassword, op => op.UseValue(true))
                    .ForMember(model => model.Password, op => op.MapFrom(user => user.PasswordHash))
                    .ForMember(model => model.Captcha, op => op.Ignore());

            CreateMap<WebMarket.ViewModel.User.User.UserDataEntry, WebMarket.DomainClasses.ApplicationUser>()
                    .ForMember(model => model.RegisterDate,
                                 op => op.UseValue(DateTime.Now))
                    .ForMember(model => model.LastLoginDate,
                                 op => op.UseValue(DateTime.Now))
                    .ForMember(model => model.Factors,
                                 op => op.Ignore())
                    .ForMember(model => model.FactorItems,
                                 op => op.Ignore())
                    .ForMember(model => model.EmailConfirmed,
                                 op => op.UseValue(true))
                //.ForMember(model => model.Email, op => op.Ignore())
                    .ForMember(model => model.PasswordHash,
                                 op => op.MapFrom(view => view.Password))
                    .ForMember(model => model.SecurityStamp,
                                 op => op.Ignore())
                //.ForMember(model => model.PhoneNumber,op => op.Ignore())
                    .ForMember(model => model.PhoneNumberConfirmed,
                                 op => op.UseValue(true))
                    .ForMember(model => model.TwoFactorEnabled,
                                 op => op.Ignore())
                    .ForMember(model => model.LockoutEnabled,
                                 op => op.Ignore())
                    .ForMember(model => model.LockoutEndDateUtc,
                                 op => op.Ignore())
                    .ForMember(model => model.AccessFailedCount,
                                 op => op.Ignore())
                    .ForMember(model => model.Roles,
                                 op => op.Ignore())
                    .ForMember(model => model.Logins,
                                 op => op.Ignore())
                    .ForMember(model => model.Claims,
                                 op => op.Ignore())
                    .ForMember(model => model.BirthDate,
                                 op => op.ResolveUsing(src =>
                                                         {
                                                             var dt = src.BirthDate;
                                                             return dt.Value.ToMiladiDate();
                                                         }));

                    //.ForMember(model => model.BirthDate,
                    //     op => op.ResolveUsing<PersianDateTimeToMiladiConverter>());


            CreateMap<WebMarket.DomainClasses.ApplicationUser, WebMarket.ViewModel.Admin.User.UserDataEntry>()
                   .ForMember(model => model.ConfirmPassword, op => op.UseValue(true))
                   .ForMember(model => model.Password, op => op.MapFrom(user => user.PasswordHash));


            CreateMap<WebMarket.ViewModel.Admin.User.UserDataEntry, WebMarket.DomainClasses.ApplicationUser>()
                    .ForMember(model => model.RegisterDate, op => op.UseValue(DateTime.Now))
                    .ForMember(model => model.LastLoginDate, op => op.UseValue(DateTime.Now))
                    .ForMember(model => model.Factors, op => op.Ignore())
                    .ForMember(model => model.FactorItems, op => op.Ignore())
                    .ForMember(model => model.EmailConfirmed, op => op.UseValue(true))
                    .ForMember(model => model.PasswordHash, op => op.MapFrom(view => view.Password))
                    .ForMember(model => model.SecurityStamp, op => op.Ignore())
                    .ForMember(model => model.PhoneNumberConfirmed, op => op.UseValue(true))
                    .ForMember(model => model.TwoFactorEnabled, op => op.Ignore())
                    .ForMember(model => model.LockoutEnabled, op => op.Ignore())
                    .ForMember(model => model.LockoutEndDateUtc, op => op.Ignore())
                    .ForMember(model => model.AccessFailedCount, op => op.Ignore())
                    .ForMember(model => model.Roles, op => op.Ignore())
                    .ForMember(model => model.Logins, op => op.Ignore())
                    .ForMember(model => model.Claims, op => op.Ignore())
                    .ForMember(model => model.BirthDate, op => op.ResolveUsing(src =>
                    {
                        var dt = src.BirthDate;
                        return dt.Value.ToMiladiDate();
                    }));


            CreateMap < WebMarket.DomainClasses.ApplicationUser , WebMarket.ViewModel.Admin.User.DetailsUserViewModel >()
                    .ForMember ( model => model.RoleNames ,op => op.MapFrom ( user => user.Roles.Select ( role => role.Role.Name ) ) )
                    //.ForMember(model => model.RoleNames, op => op.Ignore ())
                    .ForMember ( model => model.LastLoginDate ,op => op.Ignore());
            //.ForMember(model => model.Name, op => op.MapFrom(user => user.Name))
            //.ForMember(model => model.Family, op => op.MapFrom(user => user.Family));


            //CreateMap<WebMarket.ViewModel.Admin.User.DetailsUserViewModel, WebMarket.DomainClasses.ApplicationUser>();

        }
    }
}
