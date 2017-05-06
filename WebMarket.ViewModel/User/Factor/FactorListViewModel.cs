using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebMarket.ViewModel.User.Factor
{
    public class FactorListViewModel
    {
        public IEnumerable<FactorViewModel> FactorList { get; set; }
        public DomainClasses.Enums.Payments PaymentBy { get; set; }

        public int PageCount { get; set; }
        public int PageNumber { get; set; }
        public int TotalFactors { get; set; }
    }
}
