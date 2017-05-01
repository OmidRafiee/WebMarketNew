using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebMarket.DomainClasses.Entity;
using WebMarket.ViewModel.Admin.Group;

namespace Market.AutoMapperConfig
{
    public class GroupProfile:AutoMapper.Profile
    {
        public GroupProfile ()
        {
            CreateMap < Group , GroupDataEntriy > ();
                   

            CreateMap<GroupDataEntriy, Group>()
                    .ForMember(model=>model.Products,op=>op.Ignore())
                    .ForMember(model=>model.Id,op=>op.Ignore())
                   .ForMember(model => model.Children, op => op.Ignore());
        }
    }
}
