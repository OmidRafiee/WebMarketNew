using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebMarket.DomainClasses.Entity;
using WebMarket.ViewModel.Admin.Product;

namespace WebMarket.ServiceLayer.Interfaces
{
     public interface IProductService
    {
         Task<bool> Insert(Product product);
         Task<ProductDataEntriy> Update(int id);
         Task<bool> Update(ProductDataEntriy viewModel,string path);
         Task < bool > Delete ( int id );
         IEnumerable<Product> GetRows();

         IEnumerable<ProductsViewModel> ListProducts();
         ProductDataEntriy GetForEdit(int id);

         IEnumerable<ProductSectionViewModel> GetMoreSellProduct(int count);
         IEnumerable<ProductSectionViewModel> GetNewProduct(int count);
         IEnumerable<ProductSectionViewModel> GetPopularProduct(int count);

         Product FindByName( string name );
         ProductsViewModel FindById(int id);
         bool IsGroupSelected (int groupId);

         ProductsViewModel FindByUrl(string url);

         
    }
}
