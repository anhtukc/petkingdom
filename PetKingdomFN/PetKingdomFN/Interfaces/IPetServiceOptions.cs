using PetKingdomFN.BusEntities;
using PetKingdomFN.Models;

namespace PetKingdomFN.Interfaces
{
    public interface IPetServiceOptions
    {
        Task<DataList<ServiceOption>> GetPageList(Pagination page);
        Task<ServiceOption> GetPetServiceOptionById(string id);
        Task<List<ServiceOption>> GetPetServiceOptionByParentId(string petServiceId);

        Task<ServiceOption> AddPetServiceOption(ServiceOption service);
        Task<ServiceOption> UpdatePetServiceOption(ServiceOption service);
        Task<int> DeletePetServiceOption(string id);
        Task<DataList<ServiceOption>> SearchPetServiceOption(Pagination page, basedSearchObject searchObj);
    }
}
