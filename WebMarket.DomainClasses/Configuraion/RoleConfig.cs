using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebMarket.DomainClasses.Configuraion
{
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.ModelConfiguration;

    public class RoleConfig : EntityTypeConfiguration<DomainClasses.CustomRole>
    {
        public RoleConfig()
        {
            Property(role => role.Id)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
        }
    }
}
