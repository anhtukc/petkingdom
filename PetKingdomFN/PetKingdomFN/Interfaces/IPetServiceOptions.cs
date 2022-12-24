using PetKingdomFN.BusEntities;
using PetKingdomFN.Models;

namespace PetKingdomFN.Interfaces
{
    public interface IPetServiceOptions
    {
        Task<PetServiceOptionsDataList> GetPageList(Pagination page);
        Task<ServiceOption> GetPetServiceOptionById(string id);
        Task<ServiceOption> AddPetServiceOption(ServiceOption service);
        Task<ServiceOption> UpdatePetServiceOption(ServiceOption service);
        Task<int> DeletePetServiceOption(string id);
        Task<PetServiceOptionsDataList> SearchPetServiceOption(Pagination page, basedSearchObject searchObj);
    }
}
