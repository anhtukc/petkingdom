using PetKingdomFN.BusEntities;
using PetKingdomFN.Models;

namespace PetKingdomFN.Interfaces
{
    public interface IServiceImage
    {
        Task<List<ServiceImage>> GetAllById(string serviceId);
        Task<List<ServiceImage>> GetPageList(Pagination page, string serviceId);
        Task<List<ServiceImage>> UploadImage(List<IFormFile> files, string serviceId);
        Task<int> DeleteImage(string fileName);
    }
}
