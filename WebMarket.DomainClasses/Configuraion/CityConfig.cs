using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebMarket.DomainClasses.Entity;

namespace WebMarket.DomainClasses.Configuraion
{
    public class CityConfig : EntityTypeConfiguration<City>
    {
       public CityConfig ()
       {
           this.Property (a => a.Name).IsRequired().HasMaxLength(20);
           this.Property ( a => a.StateId ).IsRequired ();
       }
    }
}
