using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.DynamicLinq;
using PetKingdomFN.BusEntities;
using PetKingdomFN.Helpers;
using PetKingdomFN.Interfaces;
using PetKingdomFN.Models;
using System.Linq.Dynamic.Core;

namespace PetKingdomFN.Repositories
{
    public class ScheduleAvailableRepository : IScheduleAvailableRepository
    {

        private readonly PetKingdomContext _DbContext;
        public ScheduleAvailableRepository(PetKingdomContext DBContext)
        {
            _DbContext = DBContext;
        }
        
        public async Task<List<ScheduleAvailable>> GetScheduleAvailableByOptionId(string optionId , string startedDateFormat)
        {
            DateTime startedDate = DateTime.Parse(startedDateFormat);
            return await _DbContext.ScheduleAvailables
                .Where(x=>x.ServiceOptionId == optionId && x.startedDate>=startedDate)
                .ToListAsync();
        }
        public async Task<DataList<ScheduleAvailable>> GetPageList(Pagination page, string optionId)
        {
            DataList<ScheduleAvailable> result = new DataList<ScheduleAvailable>();
            string sortQuery = page.sortColumn + " " + page.sortOrder;
            List<ScheduleAvailable> allData = await _DbContext.ScheduleAvailables
                .Where(x=>x.ServiceOptionId == optionId)
                .OrderBy(sortQuery)             
                .ToListAsync();
            result.numberOfRecords = allData.Count();
            result.list = allData.Skip((page.currentPage - 1) * page.pageSize)
                .Take(page.pageSize)
                .ToList();
            return result;
        }

        public async Task<DataList<ScheduleAvailable>> SearchScheduleAvailable(Pagination page, basedSearchObject searchObj, string optionId)
        {
            DataList<ScheduleAvailable> result = new DataList<ScheduleAvailable>();
            string sortQuery = page.sortColumn + " " + page.sortOrder;

            List<ScheduleAvailable> allData = await _DbContext.ScheduleAvailables
                .Where(x => x.ServiceOptionId == optionId
                && (searchObj.status == -1 || x.Status == searchObj.status)).OrderBy(sortQuery)
                .ToListAsync();
            if (allData.Count() > 0)
            {
                result.numberOfRecords = allData.Count();
                result.list = allData.Skip((page.currentPage - 1) * page.pageSize)
                    .Take(page.pageSize)
                    .ToList();
            }

            return result;
        }
        public async Task<ScheduleAvailable> AddScheduleAvailable(ScheduleAvailable sa)
        {
           
            sa.Id = Guid.NewGuid().ToString();
            sa.CreatedDate = DateTime.Now;
            sa.UpdateDate = DateTime.Now;
            sa.StartedDate = DateTime.Parse(sa.startedDateFormat);
            sa.EndedDate = DateTime.Parse(sa.endedDateFormat);
            var obj = _DbContext.ScheduleAvailables.AddAsync(sa);
            await _DbContext.SaveChangesAsync();
            return obj.Result.Entity;
        }
        public async Task<ScheduleAvailable> UpdateScheduleAvailable(ScheduleAvailable sa)
        {
            sa.UpdateDate = DateTime.Now;
            sa.StartedDate = DateTime.Parse(sa.startedDateFormat);
            sa.EndedDate = DateTime.Parse(sa.endedDateFormat);
            _DbContext.Entry(sa).State = EntityState.Modified;
            await _DbContext.SaveChangesAsync();
            return sa;
        }
        public async Task<ScheduleAvailable> GetScheduleAvailableById(string id)
        {
          

            var obj = await _DbContext.ScheduleAvailables.Where(x => x.Id == id ).FirstAsync();
            return obj;
        }
        
        public async Task<int> DeleteScheduleAvailable(string id)
        {
            var ScheduleAvailable = await _DbContext.ScheduleAvailables.FindAsync(id);
            if (ScheduleAvailable is null)
            {
                return 0;
            }
            _DbContext.ScheduleAvailables.Remove(ScheduleAvailable);
            await _DbContext.SaveChangesAsync();
            return 1;

        }
    }
}
