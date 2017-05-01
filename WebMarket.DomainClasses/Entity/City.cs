using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebMarket.DomainClasses.Entity
{
    public class City
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int? StateId { get; set; }

        public virtual State State { get; set; }
        public ICollection<Reseller> Resellers { get; set; }
    }
}
