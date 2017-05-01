using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebMarket.DomainClasses.Entity;
using WebMarket.ViewModel.Admin.Setting;

namespace Market.AutoMapperConfig
{
    public class SettingProfile : AutoMapper.Profile
    {
        public SettingProfile ()
        {
            CreateMap < Setting , EditSettingDataEntry > ()
                 .ForMember(model => model.Id, op => op.Ignore());

            CreateMap < EditSettingDataEntry , Setting > ()
                    .ForMember ( model => model.Id ,op => op.Ignore () );
        }
    }
}
