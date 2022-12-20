using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.DynamicLinq;
using PetKingdomFN.BusEntities;
using PetKingdomFN.Interfaces;
using PetKingdomFN.Models;
using System.Linq.Dynamic.Core;
namespace PetKingdomFN.Repositories
{
    public class ServiceOptionRepository:IPetServiceOptions
    {

        private readonly PetKingdomContext _DbContext;
        public ServiceOptionRepository(PetKingdomContext DBContext)
        {
            _DbContext = DBContext;
        }
        public async Task<int> GetNumberOfRecords()
        {
            return await _DbContext.ServiceOptions.CountAsync();
        }
        public async Task<List<ServiceOption>> GetPageList(Pagination page)
        {
            string sortQuery = page.sortColumn + " " + page.sortOrder;
            return await _DbContext.ServiceOptions
                .OrderBy(sortQuery)
                .Skip(page.pageSize * (page.currentPage - 1))
                .Take(page.pageSize)
                .ToListAsync();
        }

        //public async Task<List<ServiceOption>> SearchServiceOption(Pagination page, string name, int? status)
        //{
        //    string sortQuery = page.sortColumn + " " + page.sortOrder;
        //    List<ServiceOption> list = await _DbContext.ServiceOptions
        //        .Where(x => (string.IsNullOrEmpty(name) || x.Name.Contains(name))
        //        && (!status.HasValue || x.Status == status))
        //        .OrderBy(sortQuery)
        //        .Skip(page.pageSize * (page.currentPage - 1))
        //        .Take(page.pageSize)
        //        .ToListAsync();
        //    return list;
        //}
        public async Task<ServiceOption> AddServiceOption(ServiceOption option)
        {
            var result = _DbContext.ServiceOptions.AddAsync(option);
            await _DbContext.SaveChangesAsync();
            return result.Result.Entity;
        }
        public async Task<ServiceOption> UpdateServiceOption(ServiceOption option)
        {
            var obj = await _DbContext.ServiceOptions.Where(x => x.Id == option.Id).FirstAsync();
            obj = option;
            _DbContext.SaveChanges();
            return obj;
        }
        public async Task<ServiceOption> GetServiceOptionById(string id)
        {
            var obj = await _DbContext.ServiceOptions.Where(x => x.Id == id).FirstAsync();

            return obj;
        }
        public async Task<string> DeleteServiceOption(string id)
        {
            var ServiceOption = await _DbContext.ServiceOptions.FindAsync(id);
            if (ServiceOption is null)
            {
                return "Not found";
            }
            _DbContext.ServiceOptions.Remove(ServiceOption);
            await _DbContext.SaveChangesAsync();
            return "success";

        }
    }
}
