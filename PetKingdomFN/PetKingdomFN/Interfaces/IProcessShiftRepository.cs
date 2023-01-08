using PetKingdomFN.BusEntities;
using PetKingdomFN.Models;

namespace PetKingdomFN.Interfaces
{
    public interface IProcessShiftRepository
    {

        Task<List<ProcessShift>> GetAllById(string ShiftId);
        Task<List<ProcessShift>> GetPageList(Pagination page,string ShiftId);
        Task<List<ProcessShift>> UploadImage(List<IFormFile> files, string ShiftId);
        Task<int> DeleteImage(string fileName);
    }
}
