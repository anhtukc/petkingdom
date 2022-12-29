using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.DynamicLinq;
using PetKingdomFN.BusEntities;
using PetKingdomFN.Helpers;
using PetKingdomFN.Interfaces;
using PetKingdomFN.Models;
using System.Linq.Dynamic.Core;
namespace PetKingdomFN.Repositories
{
    public class PetServiceOptionRepository : IPetServiceOptions
    {

        private readonly PetKingdomContext _DbContext;
        IdGeneration GenerationId = new IdGeneration();
        public PetServiceOptionRepository(PetKingdomContext DBContext)
        {
            _DbContext = DBContext;
        }
        public async Task<int> GetNumberOfRecords()
        {
            return await _DbContext.ServiceOptions.CountAsync();
        }
        public async Task<DataList<ServiceOption>> GetPageList(Pagination page)
        {
            DataList<ServiceOption> result = new DataList<ServiceOption>();
            string sortQuery = page.sortColumn + " " + page.sortOrder;
            List<ServiceOption> allData = await _DbContext.ServiceOptions
                .OrderBy(sortQuery)
                .Join(_DbContext.PetServices,
                        x => x.PetServiceId,
                        y => y.Id,
                        (x, y) =>
                        new ServiceOption
                        {
                            Id = x.Id,
                            Name = x.Name,
                            Description = x.Description,
                            EstimatedCompletionTime = x.EstimatedCompletionTime,
                            PetServiceName = y.Name,
                            CreatedDate = x.CreatedDate,
                            UpdateDate = x.UpdateDate,
                            Status = x.Status
                        })
                .ToListAsync();
            result.numberOfRecords = allData.Count();
            result.list = allData.Skip((page.currentPage - 1) * page.pageSize)
                .Take(page.pageSize)
                .ToList();
            return result;
        }
        public async Task<List<ServiceOption>> GetPetServiceOptionByParentId(string petServiceId)
        {
            return await _DbContext.ServiceOptions.Where(x => x.PetServiceId == petServiceId).ToListAsync();
        }
        public async Task<DataList<ServiceOption>> SearchPetServiceOption(Pagination page, basedSearchObject searchObj)
        {
            DataList<ServiceOption> result = new DataList<ServiceOption>();
            string sortQuery = page.sortColumn + " " + page.sortOrder;

            List<ServiceOption> allData = await _DbContext.ServiceOptions
                .Where(x => (string.IsNullOrEmpty(searchObj.name) || x.Name.Contains(searchObj.name))
                && (searchObj.status == -1 || x.Status == searchObj.status)).OrderBy(sortQuery)
                 .Join(_DbContext.PetServices,
                        x => x.PetServiceId,
                        y => y.Id,
                        (x, y) =>
                        new ServiceOption
                        {
                            Id = x.Id,
                            Name = x.Name,
                            PetServiceId = x.PetServiceId,
                            Description = x.Description,
                            EstimatedCompletionTime = x.EstimatedCompletionTime,
                            PetServiceName = y.Name,
                            CreatedDate = x.CreatedDate,
                            UpdateDate = x.UpdateDate,
                            Status = x.Status
                        })
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
        public async Task<ServiceOption> AddPetServiceOption(ServiceOption option)
        {
            ServiceOption priviousId = await _DbContext.ServiceOptions
               .OrderBy("id desc")
               .FirstAsync();
            option.Id = await GenerationId.generateId(priviousId.Id.ToString());
            option.CreatedDate = DateTime.Now;
            option.UpdateDate = DateTime.Now;
            var obj = _DbContext.ServiceOptions.AddAsync(option);
            await _DbContext.SaveChangesAsync();
            return option;
        }
        public async Task<ServiceOption> UpdatePetServiceOption(ServiceOption option)
        {
            option.UpdateDate = DateTime.Now;
            _DbContext.Entry(option).State = EntityState.Modified;
            await _DbContext.SaveChangesAsync();
            return option;
        }
        public async Task<ServiceOption> GetPetServiceOptionById(string id)
        {
            var obj = await _DbContext.ServiceOptions
                .Where(x => x.Id == id)
                .FirstAsync();
            return obj;
        }
        public async Task<int> DeletePetServiceOption(string id)
        {
            var ServiceOption = await _DbContext.ServiceOptions.FindAsync(id);
            if (ServiceOption is null)
            {
                return 0;
            }
            _DbContext.ServiceOptions.Remove(ServiceOption);
            await _DbContext.SaveChangesAsync();
            return 1;

        }
    }
}
