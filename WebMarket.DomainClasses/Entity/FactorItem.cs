using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebMarket.DomainClasses.Entity
{
    public class FactorItem
    {
        public int Id { get; set; }
        public int FactorId { get; set; }
        public int ProductId { get; set; }
        public byte Qty { get; set; }//count
        
        public virtual ApplicationUser User { get; set; }
        public virtual Product Product { get; set; }
    }
}
