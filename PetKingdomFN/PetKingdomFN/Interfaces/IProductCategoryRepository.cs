using PetKingdomFN.BusEntities;
using PetKingdomFN.Models;

namespace PetKingdomFN.Interfaces
{
    public interface IProductCategoryRepository
    {
        Task<DataList<ProductCategory>> GetPageList(Pagination page);
        Task<DataList<ProductCategory>> SearchProductCategory(Pagination page, basedSearchObject searchObj);
        Task<ProductCategory> GetProductCategoryById(string id);
        Task<ProductCategory> AddProductCategory(ProductCategory pc);
        Task<ProductCategory> UpdateProductCategory(ProductCategory pc);
        Task<int> DeleteProductCategory(string id);
    }
}
