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
    public class BrandRepository : IBrandRepository
    {

        private readonly PetKingdomContext _DbContext;
        IdGeneration GenerationId = new IdGeneration();
        public BrandRepository(PetKingdomContext DBContext)
        {
            _DbContext = DBContext;
        }
        public async Task<DataList<Brand>> GetPageList(Pagination page)
        {
            DataList<Brand> result = new DataList<Brand>();
            string sortQuery = page.sortColumn + " " + page.sortOrder;
            List<Brand> allData = await _DbContext.Brands.OrderBy(sortQuery).ToListAsync();
            result.numberOfRecords = allData.Count();
            result.list = allData.Skip((page.currentPage - 1) * page.pageSize)
                .Take(page.pageSize)
                .ToList();
            return result;
        }

        public async Task<DataList<Brand>> SearchBrand(Pagination page, basedSearchObject searchObj)
        {
            DataList<Brand> result = new DataList<Brand>();
            string sortQuery = page.sortColumn + " " + page.sortOrder;

            List<Brand> allData = await _DbContext.Brands
                .Where(x => (string.IsNullOrEmpty(searchObj.name) || x.Name.Contains(searchObj.name))
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
        public async Task<Brand> AddBrand(Brand brand)
        {

            brand.Id = Guid.NewGuid().ToString();
            brand.CreatedDate = DateTime.Now;
            brand.UpdateDate = DateTime.Now;
            var obj = _DbContext.Brands.AddAsync(brand);
            await _DbContext.SaveChangesAsync();
            return obj.Result.Entity;
        }
        public async Task<Brand> UpdateBrand(Brand brand)
        {
            brand.UpdateDate = DateTime.Now;
            _DbContext.Entry(brand).State = EntityState.Modified;
            await _DbContext.SaveChangesAsync();
            return brand;
        }
        public async Task<Brand> GetBrandById(string id)
        {
            var obj = await _DbContext.Brands.Where(x => x.Id == id).FirstAsync();
            return obj;
        }
        public async Task<int> DeleteBrand(string id)
        {
            var Brand = await _DbContext.Brands.FindAsync(id);
            if (Brand is null)
            {
                return 0;
            }
            _DbContext.Brands.Remove(Brand);
            await _DbContext.SaveChangesAsync();
            return 1;

        }

    }
}
