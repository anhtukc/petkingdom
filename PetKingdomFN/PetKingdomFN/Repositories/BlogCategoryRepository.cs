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
    public class BlogCategoryRepository : IBlogCategoryRepository
    {

        private readonly PetKingdomContext _DbContext;
        IdGeneration GenerationId = new IdGeneration();
        public BlogCategoryRepository(PetKingdomContext DBContext)
        {
            _DbContext = DBContext;
        }
        public async Task<List<BlogCategory>> GetAll()
        {
            return await _DbContext.BlogCategories.ToListAsync();
        }
        public async Task<DataList<BlogCategory>> GetPageList(Pagination page)
        {
            DataList<BlogCategory> result = new DataList<BlogCategory>();
            string sortQuery = page.sortColumn + " " + page.sortOrder;
            List<BlogCategory> allData = await _DbContext.BlogCategories.OrderBy(sortQuery).ToListAsync();
            result.numberOfRecords = allData.Count();
            result.list = allData.Skip((page.currentPage - 1) * page.pageSize)
                .Take(page.pageSize)
                .ToList();
            return result;
        }

        public async Task<DataList<BlogCategory>> SearchBlogCategory(Pagination page, basedSearchObject searchObj)
        {
            DataList<BlogCategory> result = new DataList<BlogCategory>();
            string sortQuery = page.sortColumn + " " + page.sortOrder;

            List<BlogCategory> allData = await _DbContext.BlogCategories
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
        public async Task<BlogCategory> AddBlogCategory(BlogCategory cate)
        {
 
            cate.Id = Guid.NewGuid().ToString();
            cate.CreatedDate = DateTime.Now;
            cate.UpdateDate = DateTime.Now;
            var obj = _DbContext.BlogCategories.AddAsync(cate);
            await _DbContext.SaveChangesAsync();
            return obj.Result.Entity;
        }
        public async Task<BlogCategory> UpdateBlogCategory(BlogCategory cate)
        {
            cate.UpdateDate = DateTime.Now;
            _DbContext.Entry(cate).State = EntityState.Modified;
            await _DbContext.SaveChangesAsync();
            return cate;
        }
        public async Task<BlogCategory> GetBlogCategoryById(string id)
        {
            var obj = await _DbContext.BlogCategories.Where(x => x.Id == id).FirstAsync();
            return obj;
        }
        public async Task<int> DeleteBlogCategory(string id)
        {
            var BlogCategory = await _DbContext.BlogCategories.FindAsync(id);
            if (BlogCategory is null)
            {
                return 0;
            }
            _DbContext.BlogCategories.Remove(BlogCategory);
            await _DbContext.SaveChangesAsync();
            return 1;

        }

    }
}
