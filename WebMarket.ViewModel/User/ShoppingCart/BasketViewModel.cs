using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebMarket.ViewModel.User.ShoppingCart
{
    public class BasketViewModel
    {
        public Admin.Product.ProductsViewModel Product { get; set; }
        public int Count { get; set; }
    }
}
