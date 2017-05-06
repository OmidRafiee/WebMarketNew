using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebMarket.DomainClasses.Entity;
using WebMarket.ViewModel.Admin.Group;
using WebMarket.ViewModel.User.Factor;

namespace Market.AutoMapperConfig
{
    public class FactorProfile:AutoMapper.Profile
    {
        public FactorProfile()
        {
            CreateMap < Factor , FactorViewModel > ();
            CreateMap<FactorViewModel, Factor>()
                .ForMember(model=>model.User,op=>op.Ignore());

            CreateMap<FactorViewModel, FactorViewModel>();
        }
    }
}
