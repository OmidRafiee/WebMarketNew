using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebMarket.DataLayer.Context;
using WebMarket.DomainClasses.Entity;
using WebMarket.DomainClasses.Enums;
using WebMarket.ServiceLayer.Interfaces;

namespace WebMarket.ServiceLayer.EFServices
{
    using AutoMapper.QueryableExtensions;

    public class GroupService : IGroupService
    {
        #region Fields

        private readonly IUnitOfWork _unitOfWork;
        private readonly IDbSet<Group> _groups;

        #endregion

        #region Constructor

        public GroupService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _groups = _unitOfWork.Set<Group>();
        }

        #endregion

        public IEnumerable<ViewModel.Admin.Group.GroupViewModel> GetFirstLevelGroups()
        {
            return _groups.AsNoTracking().Where(a => a.ParentId == null).ProjectTo<ViewModel.Admin.Group.GroupViewModel>(Market.AutoMapperConfig.Configuration.MapperConfiguration);

        }

        public bool CheckExistName(string name)
        {
            return _groups.Any(group => group.Name == name);
        }

        //public IList<Group> GetFirstLevelGroup()
        //{
        //    return
        //        _groups.AsNoTracking()
        //            .Where(a => a.ParentId == null)
        //            .ToList();
        //}

        public IEnumerable<Group> GetSecondLevelGroups()
        {
            return
                 _groups.AsNoTracking()
                     .Where(a => a.ParentId != null)
                     .ToList();
        }


        public AddGroupStatus Add(Group group)
        {
            if (CheckExistName(group.Name)) return AddGroupStatus.GroupNameExist;

            _groups.Add(group);
            _unitOfWork.SaveChanges();
            return AddGroupStatus.AddSuccessfully;
        }

        public IEnumerable<Group> GetAll()
        {
            return _groups.AsNoTracking().ToList();
        }
    }
}
