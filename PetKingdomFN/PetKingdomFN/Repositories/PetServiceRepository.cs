using Microsoft.EntityFrameworkCore;
using PetKingdomFN.BusEntities;
using PetKingdomFN.Interfaces;
using PetKingdomFN.Models;
using System.Linq.Dynamic.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using  PetKingdomFN.Helpers;
namespace PetKingdomFN.Repositories
{
    public class PetServiceRepository:IPetServiceRepository
    {

        private readonly PetKingdomContext _DbContext;
        IdGeneration GenerationId = new IdGeneration();
        public PetServiceRepository(PetKingdomContext DBContext)
        {
            _DbContext = DBContext;
        }
        public async Task<int> GetNumberOfRecords()
        {
            return await _DbContext.PetServices.CountAsync();
        }
        public async Task<List<PetService>> GetAll()
        {
            List<PetService> allData = await _DbContext
                .PetServices
                .OrderBy("name asc")
                .ToListAsync();
            return allData;
        }
        public async Task<DataList<PetService>> GetPageList(Pagination page)
        {
            DataList<PetService> result = new DataList<PetService>();
            string sortQuery = page.sortColumn + " " + page.sortOrder;
            List<PetService> allData = await _DbContext.PetServices.OrderBy(sortQuery).ToListAsync();
            result.numberOfRecords = allData.Count();
            result.list = allData.Skip((page.currentPage - 1) * page.pageSize)
                .Take(page.pageSize)
                .ToList();
            return result;
        }

        public async Task<DataList<PetService>> SearchPetService(Pagination page, basedSearchObject searchObj)
        {
            DataList<PetService> result = new DataList<PetService>();
            string sortQuery = page.sortColumn + " " + page.sortOrder;

            List<PetService> allData= await _DbContext.PetServices
                .Where(x => (string.IsNullOrEmpty(searchObj.name) || x.Name.Contains(searchObj.name)) 
                && (searchObj.status == -1 || x.Status == searchObj.status)).OrderBy(sortQuery)
                .ToListAsync();
            if(allData.Count() > 0)
            {
                result.numberOfRecords = allData.Count();

                result.list = allData.Skip((page.currentPage - 1) * page.pageSize)
                    .Take(page.pageSize)
                    .ToList();
            }
           
            return result;  
        }
        public async Task<PetService> AddPetService(PetService service)
        {
            PetService priviousId = await _DbContext.PetServices
                .OrderBy("id desc")
                .FirstAsync();
            service.Id = await GenerationId.generateId(priviousId.Id.ToString());
            service.CreatedDate = DateTime.Now;
            service.UpdateDate = DateTime.Now;
            var obj =  _DbContext.PetServices.AddAsync(service);
            await _DbContext.SaveChangesAsync();
            return obj.Result.Entity;
        }
        public async Task<PetService> UpdatePetService(PetService service)
        {
            service.UpdateDate = DateTime.Now;
            _DbContext.Entry(service).State = EntityState.Modified;
            await _DbContext.SaveChangesAsync();
            return service;
        }
        public async Task<PetService> GetPetServiceById(string id)
        {
            var obj = await _DbContext.PetServices.Where(x => x.Id == id).FirstAsync();           
            return obj;
        }
        public async Task<int> DeletePetService(string id)
        {
            var petService = await _DbContext.PetServices.FindAsync(id);
            if(petService is null)
            {
                return 0;
            }
             _DbContext.PetServices.Remove(petService);
            await _DbContext.SaveChangesAsync();
            return 1;

        }
       
    }
}
