using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebMarket.DomainClasses.Entity;

namespace WebMarket.ViewModel.User.Product
{
   public class HomeIndexViewModel
    {
       public IEnumerable<Admin.Product.ProductSectionViewModel >NewProducts { get; set; }
       public IEnumerable<Admin.Product.ProductSectionViewModel> BestSellerProducts { get; set; }
       public IEnumerable<Admin.Product.ProductSectionViewModel> PopularProducts { get; set; }
       public IEnumerable <Admin.Setting.SlideShowDataEntry> SlideShows { get; set; }   
    }
}
