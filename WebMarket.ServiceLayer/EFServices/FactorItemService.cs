using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebMarket.DataLayer.Context;
using WebMarket.DomainClasses.Entity;
using WebMarket.ServiceLayer.Interfaces;
using WebMarket.ViewModel.Admin.User;

namespace WebMarket.ServiceLayer.EFServices
{
    public class FactorItemService:Interfaces.IFactorItemService
    {
        #region Fields

        private readonly IUnitOfWork _unitOfWork;
        private readonly IProductService _productService;
        private readonly IDbSet<FactorItem> _factorItems;

        #endregion

        #region Constructor

        public FactorItemService(IUnitOfWork unitOfWork,IProductService productService)
        {
            _unitOfWork = unitOfWork;
            _productService = productService;
            _factorItems = _unitOfWork.Set<FactorItem>();
        }

        #endregion

        #region Implementation of IFactorItem

     

        #endregion
    }
}
