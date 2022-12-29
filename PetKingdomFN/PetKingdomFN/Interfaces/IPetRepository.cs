using PetKingdomFN.BusEntities;
using PetKingdomFN.Models;

namespace PetKingdomFN.Interfaces
{
    public interface IPetRepository
    {
        Task<DataList<Pet>> GetPageList(Pagination page);
        Task<DataList<Pet>> SearchPet(Pagination page, basedSearchObject searchObj);
        Task<List<Pet>> GetPetByCustomerId(string customerId);

        Task<Pet> GetPetById(string id);
        Task<Pet> AddPet(Pet pet);
        Task<Pet> UpdatePet(Pet pet);
        Task<int> DeletePet(string id);
    }
}
