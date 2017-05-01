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

    public class SlideShowService : ISlideShowService
    {

        #region Fields

        private readonly IUnitOfWork _unitOfWork;
        private readonly IDbSet<SlideShow> _slideShows;

        #endregion

        #region Constructor

        public SlideShowService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _slideShows = _unitOfWork.Set<SlideShow>();
        }

        #endregion

        #region Implementation of ISlideShowService



        //public async Task<bool> Create(SlideShowDataEntry viewModel)
        //{
        //    try
        //    {
        //        await Task.Run(() =>
        //        {
        //            _slideShows.Add(viewModel);
        //            _unitOfWork.SaveChanges();
        //        });


        //        return true;

        //    }
        //    catch (Exception)
        //    {
        //        return false;
        //    }
        //}

        public async Task<bool> Create(SlideShow slideShow)
        {
            try
            {
                await Task.Run(() =>
                {
                    _slideShows.Add(slideShow);
                    _unitOfWork.SaveChanges();
                });
                return true;

            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool AllowAdd()
        {
            return _slideShows.Count() <= 10;
        }

        public IEnumerable<SlideShowDataEntry> List()
        {
            return _slideShows.OrderByDescending(a => a.Id).ProjectTo<SlideShowDataEntry>(Market.AutoMapperConfig.Configuration.MapperConfiguration);
        }

        public SlideShowDataEntry GetForEdit(int id)
        {
            return _slideShows.Where(p => p.Id.Equals(id)).ProjectTo<SlideShowDataEntry>(Market.AutoMapperConfig.Configuration.MapperConfiguration).FirstOrDefault();
        }

        public async Task<bool> Update(SlideShowDataEntry viewModel, string path)
        {
            try
            {
                var slideShow = _slideShows.Find(viewModel.Id);
                if (path != null)
                {
                    slideShow.Image = path;
                }
                //await Task.Run(() =>
                //{
                    var newSlide = Market.AutoMapperConfig.Configuration.Mapper.Map(viewModel, slideShow);
                    _unitOfWork.MarkAsChanged(newSlide);
                 await  _unitOfWork.SaveChangesAsync();
                //});
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool Delete(int id)
        {
            try
            {
                var slideShow = _slideShows.Find(id);
             
                   _unitOfWork.MarkAsDeleted(slideShow);
                   _unitOfWork.SaveChanges();
              

                return true;
            }
            catch (Exception)
            {
                return false;
            }

        }

        public SlideShowDataEntry GetSlideShowDetail(int id)
        {
            return _slideShows.Where(p => p.Id == id)
                           .ProjectTo<SlideShowDataEntry>(Market.AutoMapperConfig.Configuration.MapperConfiguration)
                           .FirstOrDefault();
        }

        #endregion
    }
}
