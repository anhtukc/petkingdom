
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.DynamicLinq;
using PetKingdomFN.BusEntities;
using PetKingdomFN.Interfaces;
using PetKingdomFN.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Linq.Dynamic.Core;

namespace PetKingdomFN.Repositories
{
    public class ServiceSellPriceRepository : IServiceSellPriceRepository
    {
        private readonly PetKingdomContext _DbContext;
        public ServiceSellPriceRepository(PetKingdomContext DbContext)
        {
            _DbContext = DbContext;
        }
        public async Task<List<ServiceSellPrice>> GetAllServiceSellPrice()
        {
            return await _DbContext.ServiceSellPrices.ToListAsync();
        }
        public async Task<DataList<ServiceSellPrice>> GetPageList(Pagination page, string optionId)
        {
            DataList<ServiceSellPrice> result = new DataList<ServiceSellPrice>();
            string sortQuery = page.sortColumn + " " + page.sortOrder;
            List<ServiceSellPrice> allData = await _DbContext.ServiceSellPrices
                .Where(x=>x.ServiceOptionId == optionId)
                .OrderBy(sortQuery)
                .ToListAsync();
            result.numberOfRecords = allData.Count();
            result.list = allData.Skip((page.currentPage - 1) * page.pageSize)
                .Take(page.pageSize)
                .ToList();
            return result;
        }
        public async Task<List<ServiceSellPrice>> GetServiceSellPriceByOptionId(string optiond)
        {
            List<ServiceSellPrice> obj = await _DbContext.ServiceSellPrices.Where(x => x.ServiceOptionId == optiond&&x.Status==1).ToListAsync();
            return obj;
        }
        public async Task<DataList<ServiceSellPrice>> SearchServiceSellPrice(Pagination page, basedSearchObject searchObj)
        {
            DataList<ServiceSellPrice> result = new DataList<ServiceSellPrice>();
            string sortQuery = page.sortColumn + " " + page.sortOrder;

            List<ServiceSellPrice> allData = await _DbContext.ServiceSellPrices
                .Where(x => (searchObj.status == -1 || x.Status == searchObj.status))
                .OrderBy(sortQuery)
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
        public async Task<ServiceSellPrice> AddServiceSellPrice(ServiceSellPrice acc)
        {
            acc.Id = Guid.NewGuid().ToString();
            acc.CreatedDate = DateTime.Now;
            acc.UpdateDate = DateTime.Now;
            var obj = _DbContext.ServiceSellPrices.AddAsync(acc);
            await _DbContext.SaveChangesAsync();
            return obj.Result.Entity;
        }
        public async Task<ServiceSellPrice> UpdateServiceSellPrice(ServiceSellPrice acc)
        {
            acc.UpdateDate = DateTime.Now;
            _DbContext.Entry(acc).State = EntityState.Modified;
            await _DbContext.SaveChangesAsync();
            return acc;
        }
        public async Task<ServiceSellPrice> GetServiceSellPriceById(string id)
        {
            var obj = await _DbContext.ServiceSellPrices.Where(x => x.Id == id).FirstAsync();
            return obj;
        }
        public async Task<int> DeleteServiceSellPrice(string id)
        {
            var ServiceSellPrice = await _DbContext.ServiceSellPrices.FindAsync(id);
            if (ServiceSellPrice is null)
            {
                return 0;
            }
            _DbContext.ServiceSellPrices.Remove(ServiceSellPrice);
            await _DbContext.SaveChangesAsync();
            return 1;

        }
    }
}
