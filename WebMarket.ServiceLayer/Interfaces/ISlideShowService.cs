using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebMarket.DomainClasses.Entity;
using WebMarket.ViewModel.Admin.Setting;

namespace WebMarket.ServiceLayer.Interfaces
{
    public interface ISlideShowService
    {
        Task<bool> Create(SlideShow slideShow);
        bool AllowAdd();
        IEnumerable<SlideShowDataEntry> List();
        SlideShowDataEntry GetForEdit ( int id );
        Task<bool> Update(SlideShowDataEntry viewModel, string path);
        bool Delete ( int id );
        SlideShowDataEntry GetSlideShowDetail(int id);

        //EditSlideShowViewModel GetByIdForEdit(long id);
    }
}
