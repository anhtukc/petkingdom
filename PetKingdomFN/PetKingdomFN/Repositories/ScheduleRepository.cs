using Microsoft.EntityFrameworkCore;
using PetKingdomFN.BusEntities;
using PetKingdomFN.Interfaces;
using PetKingdomFN.Models;
using System.Linq.Dynamic.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PetKingdomFN.Helpers;
namespace PetKingdomFN.Repositories
{
    public class ScheduleRepository : IScheduleRepository
    {

        private readonly PetKingdomContext _DbContext;
        IdGeneration GenerationId = new IdGeneration();
        public ScheduleRepository(PetKingdomContext DBContext)
        {
            _DbContext = DBContext;
        }
        public async Task<DataList<Schedule>> GetPageList(Pagination page)
        {
            DataList<Schedule> result = new DataList<Schedule>();
            string sortQuery = page.sortColumn + " " + page.sortOrder;
            List<Schedule> allData = await _DbContext.Schedules.OrderBy(sortQuery).ToListAsync();
            result.numberOfRecords = allData.Count();
            result.list = allData.Skip((page.currentPage - 1) * page.pageSize)
                .Take(page.pageSize)
                .ToList();
            return result;
        }

        public async Task<DataList<Schedule>> SearchSchedule(Pagination page, basedSearchObject searchObj)
        {
            DataList<Schedule> result = new DataList<Schedule>();
            string sortQuery = page.sortColumn + " " + page.sortOrder;

            List<Schedule> allData = await _DbContext.Schedules
                .Where(x => (searchObj.status == -1 || x.Status == searchObj.status)).OrderBy(sortQuery)
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
        public async Task<Schedule[]> AddSchedule(Schedule[] schedule)
        {

            for(int i = 0; i < schedule.Length; i++)
            {
                schedule[i].Id = Guid.NewGuid().ToString();
                schedule[i].CreatedDate = DateTime.Now;
                schedule[i].UpdateDate = DateTime.Now;
            }
            var list = _DbContext.Schedules.AddRangeAsync(schedule);
            await _DbContext.SaveChangesAsync();
            return schedule;
        }
        public async Task<Schedule> UpdateSchedule(Schedule schedule)
        {
            schedule.UpdateDate = DateTime.Now;
            _DbContext.Entry(schedule).State = EntityState.Modified;
            await _DbContext.SaveChangesAsync();
            return schedule;
        }
        public async Task<Schedule> GetScheduleById(string id)
        {
            var obj = await _DbContext.Schedules.Where(x => x.Id == id).FirstAsync();
            return obj;
        }
        public async Task<int> DeleteSchedule(string id)
        {
            var Schedule = await _DbContext.Schedules.FindAsync(id);
            if (Schedule is null)
            {
                return 0;
            }
            _DbContext.Schedules.Remove(Schedule);
            await _DbContext.SaveChangesAsync();
            return 1;

        }

    }
}
