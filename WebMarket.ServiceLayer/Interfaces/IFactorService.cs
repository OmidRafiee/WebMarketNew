using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebMarket.DataLayer.Context;
using WebMarket.DomainClasses.Entity;

namespace WebMarket.ServiceLayer.Interfaces
{
    public interface IFactorService
    {
         IEnumerable <ViewModel.User.Factor.FactorViewModel> ListFactor();

        IList < ViewModel.User.Factor.FactorViewModel > GetFactorData ( out int total ,
                                                 int page ,
                                                 int count ,
                                                 DomainClasses.Enums.Payments payment );
    }
}
