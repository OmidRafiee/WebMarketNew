using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebMarket.DomainClasses.Entity;
using WebMarket.ViewModel.Admin.Product;

namespace Market.AutoMapperConfig
{
    public class ProductProfile : AutoMapper.Profile
    {
        public ProductProfile()
        {
            CreateMap<Product, ProductDataEntriy>()
                    .ForMember(model => model.Groups, op => op.Ignore())
                    .ForMember(model => model.Image, op => op.Ignore())
                    .ForMember(model => model.GroupId, op => op.MapFrom(product=>product.Group.Id));

            CreateMap<ProductDataEntriy, Product>()
                    .ForMember(model => model.Id,
                                 op => op.Ignore())
                    .ForMember(model => model.DisLike,
                                 op => op.Ignore())
                    .ForMember(model => model.Like,
                                 op => op.Ignore())
                    .ForMember(model => model.FactorItems,
                                 op => op.Ignore())
                    .ForMember(model => model.Group,
                                 op => op.Ignore());


            CreateMap<Product, ProductSectionViewModel>()
                   .ForMember(model => model.GroupName,
                                 op => op.MapFrom(p => p.Group.Name));
            //CreateMap<ProductSectionViewModel, Product>();

            CreateMap<Product, ProductsViewModel>()
                   .ForMember(model => model.GroupName,
                                 op => op.MapFrom(p => p.Group.Name));

            CreateMap<ProductsViewModel, Product>()
                   .ForMember(model => model.Id,op => op.Ignore())
                    .ForMember(model => model.DisLike,op => op.Ignore())
                    .ForMember(model => model.Like,op => op.Ignore())
                    .ForMember(model => model.FactorItems,op => op.Ignore())
                    .ForMember(model => model.Group,op => op.Ignore());

        }
    }
}
