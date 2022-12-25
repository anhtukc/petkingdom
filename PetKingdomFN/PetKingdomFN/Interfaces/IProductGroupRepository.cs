using PetKingdomFN.BusEntities;
using PetKingdomFN.Models;

namespace PetKingdomFN.Interfaces
{
    public interface IGroupProductRepository
    {
        Task<DataList<GroupProduct>> GetPageList(Pagination page);
        Task<DataList<GroupProduct>> SearchGroupProduct(Pagination page, basedSearchObject searchObj);
        Task<GroupProduct> GetGroupProductById(string id);
        Task<GroupProduct> AddGroupProduct(GroupProduct gp);
        Task<GroupProduct> UpdateGroupProduct(GroupProduct gp);
        Task<int> DeleteGroupProduct(string id);
    }
}
