using PetKingdomFN.BusEntities;
using PetKingdomFN.Models;

namespace PetKingdomFN.Interfaces
{
    public interface IBlogRepository
    {
        Task<DataList<Blog>> GetPageList(Pagination page);
        Task<DataList<Blog>> SearchBlog(Pagination page, basedSearchObject searchObj);
        Task<Blog> GetBlogById(string id);
        Task<Blog> AddBlog(Blog blog);
        Task<Blog> UpdateBlog(Blog blog);
        Task<int> DeleteBlog(string id);
    }
}
