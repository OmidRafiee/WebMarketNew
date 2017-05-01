using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebMarket.ViewModel.Admin.Product
{
    public class EditProductViewModel
    {
        public DomainClasses.Entity.Product Product { get; set; }
        public IEnumerable <DomainClasses.Entity.Group> Groups { get; set; }
    }
}
