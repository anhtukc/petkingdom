using Microsoft.EntityFrameworkCore;
using PetKingdomFN.BusEntities;
using PetKingdomFN.Interfaces;
using PetKingdomFN.Models;
using System.Linq.Dynamic.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PetKingdomFN.Repositories
{
    public class PetServiceRepository:IPetServiceRepository
    {

        private readonly PetKingdomContext _DbContext;

        public PetServiceRepository(PetKingdomContext DBContext)
        {
            _DbContext = DBContext;
        }
        public async Task<int> GetNumberOfRecords()
        {
            return await _DbContext.PetServices.CountAsync();
        }
        public async Task<List<PetService>> GetPageList(Pagination page)
        {
            string sortQuery = page.sortColumn + " " + page.sortOrder;
            return await _DbContext.PetServices
                .OrderBy(sortQuery)
                .Skip(page.pageSize * (page.currentPage - 1))
                .Take(page.pageSize)
                .ToListAsync();
        }

        public async Task<List<PetService>> SearchList(Pagination page, string name, int? status)
        {
            string sortQuery = page.sortColumn + " " + page.sortOrder;
            List<PetService> list = await _DbContext.PetServices
                .Where(x => (string.IsNullOrEmpty(name) || x.Name.Contains(name)) 
                && (!status.HasValue || x.Status == status))
                .OrderBy(sortQuery)
                .Skip(page.pageSize * (page.currentPage - 1))
                .Take(page.pageSize)
                .ToListAsync();
            return list;  
        }
        public async Task<PetService> AddPetService(PetService service)
        {
            var result = _DbContext.PetServices.AddAsync(service);
            await _DbContext.SaveChangesAsync();
            return result.Result.Entity;
        }
        public async Task<PetService> ChangePetService(PetService service)
        {
            var obj = await _DbContext.PetServices.Where(x=>x.Id == service.Id).FirstAsync();
            obj = service;
            _DbContext.SaveChanges();
            return obj;

        }
    }
}
