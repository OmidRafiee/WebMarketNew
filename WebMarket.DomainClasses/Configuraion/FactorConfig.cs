using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebMarket.DomainClasses.Entity;

namespace WebMarket.DomainClasses.Configuraion
{
    public class FactorConfig : EntityTypeConfiguration<Factor>
    {
        public FactorConfig()
        {
            this.Property(a => a.Description).HasMaxLength(400);
            this.Property(a => a.Name).IsRequired().HasMaxLength(50);
            this.Property(a => a.Mobile).IsRequired();
            this.Property(a => a.Email).IsRequired();
            this.Property(a => a.Address).IsRequired();
            this.Property(a => a.CodePosti).HasMaxLength(10);
        }
    }
}
