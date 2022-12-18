using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PetKingdomFN.BusEntities;
using PetKingdomFN.Models;
namespace PetKingdomFN.Interfaces
{
    public interface IPetServiceRepository
    {
        Task<int> GetNumberOfRecords();
        Task<List<PetService>> GetPageList(Pagination page);
        Task<PetService> GetPetServiceById(string id);
        Task<PetService> AddPetService(PetService service);
        Task<PetService> UpdatePetService(PetService service);
        Task<string> DeletePetService(string id);

        Task<List<PetService>> SearchPetService(Pagination page, string name, int? status);

    }
}
