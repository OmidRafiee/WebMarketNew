using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebMarket.DomainClasses.Entity;
using WebMarket.ViewModel.Admin.Setting;

namespace WebMarket.ServiceLayer.Interfaces
{
   public interface ISettingService
   {
       void Update(EditSettingDataEntry viewModel);
       EditSettingDataEntry GetOptionsForEdit();
       EditSettingDataEntry GetOptionsForShowOnFooter();
      
   }
}
