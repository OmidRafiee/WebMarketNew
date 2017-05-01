using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebMarket.DataLayer.Context;
using WebMarket.DomainClasses.Entity;
using WebMarket.ServiceLayer.Interfaces;
using WebMarket.ViewModel.Admin.Setting;

namespace WebMarket.ServiceLayer.EFServices
{
    using AutoMapper.QueryableExtensions;

    public class SettingService:ISettingService
    {
            #region Fields

        private readonly IUnitOfWork _unitOfWork;
        private readonly IDbSet<Setting> _settings;

        #endregion

        #region Constructor
        public SettingService (IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _settings = _unitOfWork.Set<Setting>();
        }
        #endregion

        #region Implementation of ISettingService

        public void Update ( EditSettingDataEntry viewModel )
        {
            var setting = _settings.FirstOrDefault();
            var newSetting = Market.AutoMapperConfig.Configuration.Mapper.Map(viewModel, setting);

            if (setting!=null)
            {
                _settings.Add(newSetting);
                _unitOfWork.SaveChanges();
            }
            else
            {
                _unitOfWork.MarkAsChanged(newSetting);
                _unitOfWork.SaveChanges();
            }
               
           
        }

        public EditSettingDataEntry GetOptionsForEdit ()
        {
            return _settings.ProjectTo<EditSettingDataEntry>(Market.AutoMapperConfig.Configuration.MapperConfiguration).FirstOrDefault();
       }

        public EditSettingDataEntry GetOptionsForShowOnFooter()
        {
            var setting= _settings.AsNoTracking().ToList().FirstOrDefault();
           
            var model = Market.AutoMapperConfig.Configuration.Mapper.Map<EditSettingDataEntry>(setting);

            return model;
        }

        #endregion
    }
}
