using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Market.AutoMapperConfig
{
    public static class Configuration
    {
        public static AutoMapper.IConfigurationProvider MapperConfiguration { get; private set; }

        public static AutoMapper.IMapper Mapper { get; private set; }

        public static void Configure()
        {
            var profiles = from t in System.Reflection.Assembly.GetAssembly(typeof(UserProfile))
                                           .GetTypes()
                           where typeof(AutoMapper.Profile).IsAssignableFrom(t)
                           select (AutoMapper.Profile)Activator.CreateInstance(t);

            MapperConfiguration = new AutoMapper.MapperConfiguration(cfg =>
            {
                foreach (var profile in profiles)
                {
                    cfg.AddProfile(profile);
                }
                cfg.CreateMap < string , DateTime? > ().ConvertUsing < Converters.StringToDateTimeConverter > ();
                cfg.CreateMap<DateTime?, DateTime?>().ConvertUsing<Converters.PersianDateTimeToMiladiConverter>();
            });

            MapperConfiguration.AssertConfigurationIsValid();

            Mapper = new AutoMapper.Mapper(MapperConfiguration);
        }
    }
}
