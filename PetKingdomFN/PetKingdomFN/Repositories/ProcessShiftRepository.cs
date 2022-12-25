using Microsoft.EntityFrameworkCore;
using PetKingdomFN.BusEntities;
using PetKingdomFN.Interfaces;
using PetKingdomFN.Models;
using System.Collections.Generic;
using System.Linq.Dynamic.Core;
namespace PetKingdomFN.Repositories
{
    public class ProcessShiftRepository : IProcessShiftRepository
    {
        private readonly PetKingdomContext _DbContext;
        private readonly ICloudStorageService _cloud;
        private readonly string folder = "shirf/";

        public ProcessShiftRepository(PetKingdomContext DbContext, ICloudStorageService cloud)
        {
            _DbContext = DbContext;
            _cloud = cloud;
        }
        public async Task<List<ProcessShift>> GetAllById(string ShiftId)
        {
            return await _DbContext.ProcessShifts.Where(x => x.ShiftId == ShiftId).ToListAsync();
        }
        public async Task<List<ProcessShift>> GetPageList(Pagination page,string ShiftId)
        {
            string sortQuery = page.sortColumn + " " + page.sortOrder;
            return await _DbContext.ProcessShifts
                .Where(x => x.ShiftId == ShiftId)
                .OrderBy(sortQuery)
                .Skip(page.pageSize * (page.currentPage - 1))
                .Take(page.pageSize)
                .ToListAsync();
        }
        public async Task<List<ProcessShift>> UploadImage(List<IFormFile> files, string ShiftId)
        {
            List<ProcessShift> list = new List<ProcessShift>();
            for (int i = 0; i < files.Count; i++)
            {
                ProcessShift obj = new ProcessShift();
                obj.Id = Guid.NewGuid().ToString("N");
                obj.ShiftId = ShiftId;
                obj.Name = folder + obj.Id;
                obj.Link = await _cloud.UploadFileAsync(files[i], obj.Name);
                obj.Status = 1;
                obj.CreatedDate = DateTime.Now;
                obj.UpdateDate = DateTime.Now;
                list.Add(obj);
            }
            await _DbContext.ProcessShifts.AddRangeAsync(list);
            await _DbContext.SaveChangesAsync();
            return list.ToList();
        }
        public async Task<int> DeleteImage(string id)
        {
            ProcessShift obj = await _DbContext.ProcessShifts.FindAsync(id);
            if (obj is null)
            {
                return 0;
            }
            await _cloud.DeleteFileAsync(obj.Name);
            _DbContext.ProcessShifts.Remove(obj);
            await _DbContext.SaveChangesAsync();
            return 1;
        }
    }
}
