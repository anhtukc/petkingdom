using PetKingdomFN.BusEntities;
using PetKingdomFN.Models;

namespace PetKingdomFN.Interfaces
{
    public interface IBrandRepository
    {
        Task<DataList<Brand>> GetPageList(Pagination page);
        Task<DataList<Brand>> SearchBrand(Pagination page, basedSearchObject searchObj);
        Task<Brand> GetBrandById(string id);
        Task<Brand> AddBrand(Brand br);
        Task<Brand> UpdateBrand(Brand br);
        Task<int> DeleteBrand(string id);
    }
}
