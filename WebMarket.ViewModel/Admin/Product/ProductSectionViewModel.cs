using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebMarket.ViewModel.Admin.Product
{
    public class ProductSectionViewModel
    {
        [System.ComponentModel.DataAnnotations.Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public int Price { get; set; }
        public int OffPrice { get; set; }
        public string Url { get; set; }
        public string Description { get; set; }
        public bool Enable { get; set; }
        public int GroupId { get; set; }
        public string Image { get; set; }
        public string GroupName { get; set; }

    }
}
