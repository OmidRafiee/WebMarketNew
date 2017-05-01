using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebMarket.DomainClasses.Entity;

namespace WebMarket.DomainClasses.Configuraion
{
    public class FactorItemConfig : EntityTypeConfiguration<FactorItem>
    {
        public FactorItemConfig()
        {

        }
    }
}
