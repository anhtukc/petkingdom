using PetKingdomFN.BusEntities;
using PetKingdomFN.Models;

namespace PetKingdomFN.Interfaces
{
    public interface IServiceImage
    {
        Task<int> GetNumberOfRecords();
        Task<List<ServiceImage>> GetAllById(string serviceId);
        Task<List<ServiceImage>> GetPageList(Pagination page);
        Task<List<ServiceImage>> UploadImage(List<IFormFile> files, string serviceId);
        Task<string> DeleteImage(string fileName);
    }
}
