using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebMarket.DataLayer.Context;
using WebMarket.DomainClasses.Entity;
using WebMarket.ServiceLayer.Interfaces;

namespace WebMarket.ServiceLayer.EFServices
{
    using AutoMapper.QueryableExtensions;
    using WebMarket.ViewModel.Admin.Group;
    using WebMarket.ViewModel.Admin.Product;

    public class ProductService : IProductService
    {
        #region Fields

        private readonly IUnitOfWork _unitOfWork;
        private readonly IDbSet<Product> _products;
        private readonly IGroupService _groupService;

        #endregion

        #region Constructor
        public ProductService(IUnitOfWork unitOfWork, IGroupService groupService)
        {
            _unitOfWork = unitOfWork;
            _products = _unitOfWork.Set<Product>();
            _groupService = groupService;
        }
        #endregion

        //#region Methods
        //public bool ExisByName(string name, long categoryId)
        //{
        //    //return _attributes.Any(a => a.CategoryId == categoryId && a.Name.Equals(name));
        //}
        //public bool ExisByName(string name, long id, long categoryId)
        //{
        //    //return _attributes.Any(a => a.CategoryId == categoryId && a.Id != id && a.Name.Equals(name));
        //}
        //#endregion

        #region CRUD
        public async Task<bool> Insert(Product product)
        {
            try
            {
               await Task.Run(() =>
                       {
                           _products.Add(product);
                           _unitOfWork.SaveChanges();
                       });


                return true;

            }
            catch (Exception)
            {
                return false;
            }
        }

        public IEnumerable<Product> GetRows()
        {
            try
            {
                return _products.AsNoTracking().ToList();
            }
            catch (Exception)
            {
                return null;
            }
        }

        public IEnumerable<ProductsViewModel> ListProducts()
        {
            return _products.AsNoTracking().ProjectTo<ProductsViewModel>(Market.AutoMapperConfig.Configuration.MapperConfiguration);
        }


        public ProductDataEntriy GetForEdit(int id)
        {
            var Group = _groupService.GetAll().Select(group => new GroupViewModel
                                                                  {
                                                                      Name = group.Name,
                                                                      Id = group.Id,
                                                                      ParentId = group.ParentId
                                                                  }).AsEnumerable();
            return _products.Where(p => p.Id.Equals(id))
                .Select(product => new ProductDataEntriy
                {
                    Groups = Group,
                    Name = product.Name,
                    Url = product.Url,
                    Tag = product.Tag,
                    GroupId = product.GroupId,
                    Image = product.Image,
                    Description = product.Description,
                    Enable = product.Enable,
                    KeyWord = product.KeyWord,
                    OffPrice = product.OffPrice,
                    Price = product.Price,
                    Summery = product.Summery

                }).FirstOrDefault();

            //return _products.Where(p => p.Id.Equals(id))
            //         .Select(product => new ProductDataEntriy
            //         {
            //             Groups = _groupService.GetAll().Select(group => new GroupViewModel
            //                                 {
            //                                     Name = group.Name,
            //                                     Id = group.Id,
            //                                     ParentId = group.ParentId
            //                                 }),
            //             Name = product.Name,
            //             Url = product.Url,
            //             Tag = product.Tag,
            //             GroupId = product.GroupId,
            //             Image = product.Image,
            //             Description = product.Description,
            //             Enable = product.Enable,
            //             KeyWord = product.KeyWord,
            //             OffPrice = product.OffPrice,
            //             Price = product.Price,
            //             Summery = product.Summery

            //         }).FirstOrDefault();
        }

        public IEnumerable < ProductSectionViewModel > GetMoreSellProduct (int count)
        {
            return _products.AsNoTracking()
               //.Where(a => a.FactorItems.Any())
               .OrderBy(a => a.Id).Take(count).ProjectTo<ProductSectionViewModel>(Market.AutoMapperConfig.Configuration.MapperConfiguration);
               
        }

        public async Task<ProductDataEntriy> Update(int id)
        {
            try
            {
                var product = _products.Find(id);
                var groups = _groupService.GetAll ()
                                     .Select ( group => new GroupViewModel
                                                        {
                                                                Name = group.Name ,
                                                                Id = group.Id ,
                                                                ParentId = group.ParentId
                                                        } ).ToList ();
                var model = new ProductDataEntriy
                  {
                      Groups = groups,
                      Name = product.Name,
                      Url = product.Url,
                      Tag = product.Tag,
                      GroupId = product.GroupId,
                      Image = product.Image,
                      Description = product.Description,
                      Enable = product.Enable,
                      KeyWord = product.KeyWord,
                      OffPrice = product.OffPrice,
                      Price = product.Price,
                      Summery = product.Summery,
                      Id = product.Id
                  };


                await Task.Run(() =>
                {
                    Market.AutoMapperConfig.Configuration.Mapper.Map(product, model);
                    _unitOfWork.MarkAsChanged(model);
                    _unitOfWork.SaveChanges();
                });


            }
            catch ( Exception )
            {
                // ignored
            }
            return null;
        }

        public async Task<bool> Update(ProductDataEntriy viewModel, string path)
        {
            try
            {
                var product = _products.Find(viewModel.Id);
                if (path != null)
                {
                    product.Image = path;
                }
                await Task.Run(() =>
                  {
                      Market.AutoMapperConfig.Configuration.Mapper.Map(viewModel, product);

                      _unitOfWork.MarkAsChanged(product);
                      _unitOfWork.SaveChanges();
                  });
                return true;
            }
            catch (Exception)
            {

                return false;
            }
        }

        public async Task<bool> Delete(int id)
        {
            try
            {
               
                var product = _products.Find(id);
                await Task.Run(() =>
                {
                    _unitOfWork.MarkAsDeleted(product);
                    _unitOfWork.SaveChanges();
                });

                return true;
            }
            catch (Exception)
            {
                return false;
            }

        }

        #endregion



        public Product FindByName(string name)
        {
            try
            {
                return _products.FirstOrDefault(a => a.Name == name);

            }
            catch (Exception)
            {
                return null;
            }
        }

        public ProductsViewModel FindById(int id)
        {
            try
            {
                return _products.Where(p => p.Id == id)
                          .ProjectTo<ProductsViewModel>(Market.AutoMapperConfig.Configuration.MapperConfiguration)
                          .FirstOrDefault();

            }
            catch (Exception)
            {
                return null;
            }
        }

        public bool IsGroupSelected ( int groupId )
        {
            return _products.Find( groupId ) == null;
        }

        public ProductsViewModel FindByUrl ( string url )
        {
            try
            {
                return _products.Where(p => p.Url.Trim() == url.Trim())
                          .ProjectTo<ProductsViewModel>(Market.AutoMapperConfig.Configuration.MapperConfiguration)
                          .FirstOrDefault();

            }
            catch (Exception)
            {
                return null;
            }
        }
    }
}
