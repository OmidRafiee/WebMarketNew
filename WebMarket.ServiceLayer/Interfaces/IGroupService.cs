using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebMarket.DomainClasses.Entity;
using WebMarket.ViewModel.Admin.Group;
using WebMarket.DomainClasses.Enums;

namespace WebMarket.ServiceLayer.Interfaces
{
    public interface IGroupService
    {
       IList<Group> GetFirstLevelGroup();
       IEnumerable<Group> GetSecondLevelGroups();
       AddGroupStatus Add(Group group);
       IEnumerable<Group> GetAll();

       bool CheckExistName(string name);
    }
}
