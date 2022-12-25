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
    public class ProductCategoryRepository : IProductCategoryRepository
    {

        private readonly PetKingdomContext _DbContext;
        IdGeneration GenerationId = new IdGeneration();
        public ProductCategoryRepository(PetKingdomContext DBContext)
        {
            _DbContext = DBContext;
        }
        public async Task<DataList<ProductCategory>> GetPageList(Pagination page)
        {
            DataList<ProductCategory> result = new DataList<ProductCategory>();
            string sortQuery = page.sortColumn + " " + page.sortOrder;
            List<ProductCategory> allData = await _DbContext.ProductCategories.OrderBy(sortQuery).ToListAsync();
            result.numberOfRecords = allData.Count();
            result.list = allData.Skip((page.currentPage - 1) * page.pageSize)
                .Take(page.pageSize)
                .ToList();
            return result;
        }

        public async Task<DataList<ProductCategory>> SearchProductCategory(Pagination page, basedSearchObject searchObj)
        {
            DataList<ProductCategory> result = new DataList<ProductCategory>();
            string sortQuery = page.sortColumn + " " + page.sortOrder;

            List<ProductCategory> allData = await _DbContext.ProductCategories
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
        public async Task<ProductCategory> AddProductCategory(ProductCategory cate)
        {

            cate.Id = Guid.NewGuid().ToString();
            cate.CreatedDate = DateTime.Now;
            cate.UpdateDate = DateTime.Now;
            var obj = _DbContext.ProductCategories.AddAsync(cate);
            await _DbContext.SaveChangesAsync();
            return obj.Result.Entity;
        }
        public async Task<ProductCategory> UpdateProductCategory(ProductCategory cate)
        {
            cate.UpdateDate = DateTime.Now;
            _DbContext.Entry(cate).State = EntityState.Modified;
            await _DbContext.SaveChangesAsync();
            return cate;
        }
        public async Task<ProductCategory> GetProductCategoryById(string id)
        {
            var obj = await _DbContext.ProductCategories.Where(x => x.Id == id).FirstAsync();
            return obj;
        }
        public async Task<int> DeleteProductCategory(string id)
        {
            var ProductCategory = await _DbContext.ProductCategories.FindAsync(id);
            if (ProductCategory is null)
            {
                return 0;
            }
            _DbContext.ProductCategories.Remove(ProductCategory);
            await _DbContext.SaveChangesAsync();
            return 1;

        }

    }
}
