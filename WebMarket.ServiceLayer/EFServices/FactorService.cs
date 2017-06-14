using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebMarket.DataLayer.Context;
using WebMarket.DomainClasses.Entity;
using AutoMapper.QueryableExtensions;
using WebMarket.ViewModel.User.Factor;
using WebMarket.DomainClasses.Enums;

namespace WebMarket.ServiceLayer.EFServices
{
    public class FactorService : Interfaces.IFactorService
    {
        #region Fields

        private readonly IUnitOfWork _unitOfWork;
        private readonly IDbSet < Factor > _factors;

        #endregion

        #region Constructor

        public FactorService ( IUnitOfWork unitOfWork )
        {
            _unitOfWork = unitOfWork;
            _factors = _unitOfWork.Set < Factor > ();
        }

        #endregion

        #region Implementation of IFactorService

        public IEnumerable < FactorViewModel > ListFactor ()
        {
            return _factors.OrderByDescending ( op => op.IsFinish )
                            .ProjectTo < FactorViewModel > ( Market.AutoMapperConfig.Configuration.MapperConfiguration );
        }


        public IList < FactorViewModel > GetFactorData ( out int total ,int page ,int count ,Payments payment )
        {
            var selectedFactors = ListFactor ().AsQueryable();
            
             switch ( payment )
                {
                    case Payments.Paid:
                        selectedFactors = selectedFactors.Where(factor => factor.IsFinish).AsQueryable ();
                        break;
                    case Payments.UnPaid:
                        selectedFactors = selectedFactors.Where(factor => factor.IsFinish==false).AsQueryable();
                        break;
                }
           

            var totalQuery = selectedFactors.Count ();
            var selectQuery = selectedFactors.Skip ( ( page - 1 ) * count ).Take ( count ).ProjectTo < FactorViewModel >(Market.AutoMapperConfig.Configuration.MapperConfiguration );
           
            total = totalQuery;
            return selectQuery.ToList();

            #endregion
        }
    }
}
