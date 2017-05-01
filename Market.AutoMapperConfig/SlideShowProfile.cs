using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebMarket.DomainClasses.Entity;
using WebMarket.ViewModel.Admin.Setting;

namespace Market.AutoMapperConfig
{
    public class SlideShowProfile : AutoMapper.Profile
    {
        public SlideShowProfile ()
        {
            CreateMap<SlideShow, SlideShowDataEntry>();
            CreateMap<SlideShowDataEntry, SlideShow>();
        }
    }
}
