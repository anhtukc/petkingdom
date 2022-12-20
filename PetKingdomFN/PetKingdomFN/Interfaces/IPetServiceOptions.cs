using PetKingdomFN.BusEntities;
using PetKingdomFN.Models;

namespace PetKingdomFN.Interfaces
{
    public interface IPetServiceOptions
    {
        Task<int> GetNumberOfRecords();
        Task<List<ServiceOption>> GetPageList(Pagination page);
        Task<ServiceOption> GetServiceOptionById(string id);
        Task<ServiceOption> AddServiceOption(ServiceOption service);
        Task<ServiceOption> UpdateServiceOption(ServiceOption service);
        Task<string> DeleteServiceOption(string id);

        //Task<List<ServiceOption>> SearchPetService(Pagination page, string name, int? status);
    }
}
