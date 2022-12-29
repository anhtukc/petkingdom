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
    public class SellBillRepository : ISellBillRepository
    {

        private readonly PetKingdomContext _DbContext;
        IdGeneration GenerationId = new IdGeneration();
        public SellBillRepository(PetKingdomContext DBContext)
        {
            _DbContext = DBContext;
        }
        public async Task<DataList<SellBill>> GetPageList(Pagination page)
        {
            DataList<SellBill> result = new DataList<SellBill>();
            string sortQuery = page.sortColumn + " " + page.sortOrder;
            List<SellBill> allData = await _DbContext.SellBills.OrderBy(sortQuery).ToListAsync();
            result.numberOfRecords = allData.Count();
            result.list = allData.Skip((page.currentPage - 1) * page.pageSize)
                .Take(page.pageSize)
                .ToList();
            return result;
        }

        public async Task<DataList<SellBill>> SearchSellBill(Pagination page, basedSearchObject searchObj)
        {
            DataList<SellBill> result = new DataList<SellBill>();
            string sortQuery = page.sortColumn + " " + page.sortOrder;

            List<SellBill> allData = await _DbContext.SellBills
                .Where(x =>  (searchObj.status == -1 || x.Status == searchObj.status)).OrderBy(sortQuery)
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
        public async Task<SellBill> AddSellBill(SellBill sellBill)
        {

            sellBill.CreatedDate = DateTime.Now;
            sellBill.UpdateDate = DateTime.Now;           
            var obj = _DbContext.SellBills.AddAsync(sellBill);
            await _DbContext.SaveChangesAsync();
            return obj.Result.Entity;
        }
        public async Task<SellBill> UpdateSellBill(SellBill SellBill)
        {
            SellBill.UpdateDate = DateTime.Now;
            _DbContext.Entry(SellBill).State = EntityState.Modified;
            await _DbContext.SaveChangesAsync();
            return SellBill;
        }
      
        public async Task<SellBill> GetSellBillById(string id)
        {
            var obj = await _DbContext.SellBills.Where(x => x.Id == id).FirstAsync();
            return obj;
        }
        public async Task<int> DeleteSellBill(string id)
        {
            var SellBill = await _DbContext.SellBills.FindAsync(id);
            if (SellBill is null)
            {
                return 0;
            }
            _DbContext.SellBills.Remove(SellBill);
            await _DbContext.SaveChangesAsync();
            return 1;

        }

    }
}
