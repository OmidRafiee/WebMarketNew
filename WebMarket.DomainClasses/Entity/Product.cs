using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebMarket.DomainClasses.Entity
{
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Price { get; set; }
        public int OffPrice { get; set; }
        public string Url { get; set; }
        public string KeyWord { get; set; }
        public string Description { get; set; }
        public string Summery { get; set; }
        public string Tag { get; set; }
        public int Like { get; set; }
        public int DisLike { get; set; }
        public bool Enable { get; set; }
        public int GroupId { get; set; }
        public string Image { get; set; }
        public string Status { get; set; }

        public virtual ICollection<FactorItem> FactorItems { get; set; }
        public virtual Group Group { get; set; }
        
    }
}
