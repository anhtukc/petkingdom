using Microsoft.EntityFrameworkCore;
using PetKingdomFN.BusEntities;
using PetKingdomFN.Interfaces;
using PetKingdomFN.Models;
using System.Linq.Dynamic.Core;
using PetKingdomFN.Helpers;
namespace PetKingdomFN.Repositories
{
    public class ShiftRepository : IShiftRepository
    {

        private readonly PetKingdomContext _DbContext;
        IdGeneration GenerationId = new IdGeneration();
        public ShiftRepository(PetKingdomContext DBContext)
        {
            _DbContext = DBContext;
        }
        public async Task<DataList<Shift>> GetPageList(Pagination page)
        {
            DataList<Shift> result = new DataList<Shift>();
            string sortQuery = page.sortColumn + " " + page.sortOrder;
            List<Shift> allData = await _DbContext.Shifts.OrderBy(sortQuery).ToListAsync();
            result.numberOfRecords = allData.Count();
            result.list = allData.Skip((page.currentPage - 1) * page.pageSize)
                .Take(page.pageSize)
                .ToList();
            return result;
        }

        public async Task<DataList<Shift>> SearchShift(Pagination page, basedSearchObject searchObj)
        {
            DataList<Shift> result = new DataList<Shift>();
            string sortQuery = page.sortColumn + " " + page.sortOrder;

            List<Shift> allData = await _DbContext.Shifts
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
        public async Task<Shift> AddShift(Shift Shift)
        {

            Shift.Id = Guid.NewGuid().ToString();
            Shift.CreatedDate = DateTime.Now;
            Shift.UpdateDate = DateTime.Now;
            var obj = _DbContext.Shifts.AddAsync(Shift);
            await _DbContext.SaveChangesAsync();
            return obj.Result.Entity;
        }
        public async Task<Shift> UpdateShift(Shift Shift)
        {
            Shift.UpdateDate = DateTime.Now;
            _DbContext.Entry(Shift).State = EntityState.Modified;
            await _DbContext.SaveChangesAsync();
            return Shift;
        }
        public async Task<Shift> GetShiftById(string id)
        {
            var obj = await _DbContext.Shifts.Where(x => x.Id == id).FirstAsync();
            return obj;
        }
        public async Task<int> DeleteShift(string id)
        {
            var Shift = await _DbContext.Shifts.FindAsync(id);
            if (Shift is null)
            {
                return 0;
            }
            _DbContext.Shifts.Remove(Shift);
            await _DbContext.SaveChangesAsync();
            return 1;

        }

    }
}
