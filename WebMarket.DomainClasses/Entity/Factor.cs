using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebMarket.DomainClasses.Entity
{
    public class Factor
    {

        public int Id { get; set; }
        public DateTime BuyDate { get; set; }
        public string FollowCode { get; set; }
        public string Description { get; set; }
        public int? UserId { get; set; }//shaid bedon login kharid konad b in dalil null
        public decimal Price { get; set; }
        public string Name { get; set; }
        public string Mobile { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
        public string CodePosti { get; set; }
        public bool IsFinish { get; set; }

        public virtual ApplicationUser User { get; set; }
    }
}
