using Microsoft.EntityFrameworkCore;
using PetKingdomFN.BusEntities;
using PetKingdomFN.Interfaces;
using PetKingdomFN.Models;
using System.Collections.Generic;
using System.Linq.Dynamic.Core;
namespace PetKingdomFN.Repositories
{
    public class ServiceImageRepository:IServiceImage
    {
        private readonly PetKingdomContext _DbContext;
        private readonly ICloudStorageService _cloud;
        private readonly string folder = "img/services/"; 

        public ServiceImageRepository(PetKingdomContext DbContext, ICloudStorageService cloud) {
            _DbContext= DbContext;
            _cloud= cloud;
        }
        public async Task<List<ServiceImage>> GetAllById(string serviceId)
        {
            return await _DbContext.ServiceImages.Where(x=>x.ServiceId==serviceId).ToListAsync();
        }
        public async Task<List<ServiceImage>> GetPageList(Pagination page)
        {
            string sortQuery = page.sortColumn + " " + page.sortOrder;
            return await _DbContext.ServiceImages
                .OrderBy(sortQuery)
                .Skip(page.pageSize * (page.currentPage - 1))
                .Take(page.pageSize)
                .ToListAsync();
        }
        public async Task<int> GetNumberOfRecords()
        {
            return await _DbContext.PetServices.CountAsync();
        }
        public async Task<List<ServiceImage>> UploadImage(List<IFormFile> files, string serviceId)
        {
            List < ServiceImage > list = new List < ServiceImage >();
            for (int i = 0;i<files.Count;i++) {
                ServiceImage obj = new ServiceImage();
                obj.Id = Guid.NewGuid().ToString("N");
                obj.ServiceId = serviceId;
                obj.Name = folder + obj.Id;
                obj.Link = await _cloud.UploadFileAsync(files[i], obj.Name);
                obj.Status = 1;
                list.Add(obj);
            }
            await _DbContext.ServiceImages.AddRangeAsync(list);
            await _DbContext.SaveChangesAsync();
            return list.ToList();
        }
        public async Task<string> DeleteImage(string id) {
            ServiceImage obj =  await _DbContext.ServiceImages.FindAsync(id);    
            if(obj is null) {
                return "not found";
            }
            await _cloud.DeleteFileAsync(obj.Name);

            _DbContext.ServiceImages.Remove(obj);
            await _DbContext.SaveChangesAsync();
            return "success";
        }
    }
}
