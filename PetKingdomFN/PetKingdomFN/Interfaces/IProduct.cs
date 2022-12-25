using PetKingdomFN.BusEntities;
using PetKingdomFN.Models;

namespace PetKingdomFN.Interfaces
{
    public interface IProductRepository
    {
        Task<DataList<Product>> GetPageList(Pagination page);
        Task<DataList<Product>> SearchProduct(Pagination page, basedSearchObject searchObj);
        Task<Product> GetProductById(string id);
        Task<Product> AddProduct(Product pd);
        Task<Product> UpdateProduct(Product pd);
        Task<int> DeleteProduct(string id);
    }
}
