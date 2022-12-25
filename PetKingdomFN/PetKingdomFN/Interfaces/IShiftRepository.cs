using PetKingdomFN.BusEntities;
using PetKingdomFN.Models;

namespace PetKingdomFN.Interfaces
{
    public interface IShiftRepository
    {
        Task<DataList<Shift>> GetPageList(Pagination page);
        Task<DataList<Shift>> SearchShift(Pagination page, basedSearchObject searchObj);
        Task<Shift> GetShiftById(string id);
        Task<Shift> AddShift(Shift shift);
        Task<Shift> UpdateShift(Shift shift);
        Task<int> DeleteShift(string id);
    }
}
