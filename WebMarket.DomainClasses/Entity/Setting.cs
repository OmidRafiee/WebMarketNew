using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebMarket.DomainClasses.Entity
{
    public class Setting
    {
        public int Id { get; set; }
        public string MarketName { get; set; }
        public string Url { get; set; }
        public string PhoneNumber1 { get; set; }
        public string PhoneNumber2 { get; set; }
        public string ContactUs { get; set; }
        public string AboutUs { get; set; }
        public string Email { get; set; }
        
    }
}
