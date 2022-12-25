using PetKingdomFN.BusEntities;
using PetKingdomFN.Models;

namespace PetKingdomFN.Interfaces
{
    public interface IBlogCategoryRepository
    {
        Task<DataList<BlogCategory>> GetPageList(Pagination page);
        Task<DataList<BlogCategory>> SearchBlogCategory(Pagination page, basedSearchObject searchObj);
        Task<BlogCategory> GetBlogCategoryById(string id);
        Task<BlogCategory> AddBlogCategory(BlogCategory acc);
        Task<BlogCategory> UpdateBlogCategory(BlogCategory acc);
        Task<int> DeleteBlogCategory(string id);
    }
}
