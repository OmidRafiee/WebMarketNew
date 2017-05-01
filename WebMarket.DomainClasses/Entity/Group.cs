using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebMarket.DomainClasses.Entity
{
    public class Group
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int? ParentId { get; set; }
      
        public ICollection<Product> Products { get; set; }
        [System.ComponentModel.DataAnnotations.Schema.ForeignKey("ParentId")]
        public virtual ICollection<Group> Children { get; set; }
    }
}
