using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebMarket.DomainClasses.Entity;

namespace WebMarket.DomainClasses.Configuraion
{
     public class ProductConfig:EntityTypeConfiguration <Product>
    {
         public ProductConfig ()
         {
             this.Property ( a => a.Name ).IsRequired ().HasMaxLength ( 50 );
             this.Property ( a => a.Price ).IsRequired ();
             this.Property(a => a.OffPrice).IsRequired();
             this.Property(a => a.Url).IsRequired().HasMaxLength(100);
             this.Property(a => a.KeyWord).HasMaxLength(300);
             this.Property(a => a.Description).HasMaxLength(500);
             this.Property(a => a.KeyWord).HasMaxLength(300);
             this.Property(a => a.Summery).IsRequired();
             this.Property(a => a.Tag).HasMaxLength(200);
         }
    }
}
