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
    public class ProductRepository : IProductRepository
    {

        private readonly PetKingdomContext _DbContext;
        IdGeneration GenerationId = new IdGeneration();
        public ProductRepository(PetKingdomContext DBContext)
        {
            _DbContext = DBContext;
        }
        public async Task<int> GetNumberOfRecords()
        {
            return await _DbContext.Products.CountAsync();
        }
        public async Task<DataList<Product>> GetPageList(Pagination page)
        {
            DataList<Product> result = new DataList<Product>();
            string sortQuery = page.sortColumn + " " + page.sortOrder;
            List<Product> allData = await _DbContext.Products.OrderBy(sortQuery).ToListAsync();
            result.numberOfRecords = allData.Count();
            result.list = allData.Skip((page.currentPage - 1) * page.pageSize)
                .Take(page.pageSize)
                .ToList();
            return result;
        }

        public async Task<DataList<Product>> SearchProduct(Pagination page, basedSearchObject searchObj)
        {
            DataList<Product> result = new DataList<Product>();
            string sortQuery = page.sortColumn + " " + page.sortOrder;

            List<Product> allData = await _DbContext.Products
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
        public async Task<Product> AddProduct(Product pd)
        {
            Product priviousId = await _DbContext.Products
                .OrderBy("id desc")
                .FirstAsync();
            pd.Id = await GenerationId.generateId(priviousId.Id.ToString());
            pd.CreatedDate = DateTime.Now;
            pd.UpdateDate = DateTime.Now;
            var obj = _DbContext.Products.AddAsync(pd);
            await _DbContext.SaveChangesAsync();
            return obj.Result.Entity;
        }
        public async Task<Product> UpdateProduct(Product pd)
        {
            pd.UpdateDate = DateTime.Now;
            _DbContext.Entry(pd).State = EntityState.Modified;
            await _DbContext.SaveChangesAsync();
            return pd;
        }
        public async Task<Product> GetProductById(string id)
        {
            var obj = await _DbContext.Products.Where(x => x.Id == id).FirstAsync();
            return obj;
        }
        public async Task<int> DeleteProduct(string id)
        {
            var Product = await _DbContext.Products.FindAsync(id);
            if (Product is null)
            {
                return 0;
            }
            _DbContext.Products.Remove(Product);
            await _DbContext.SaveChangesAsync();
            return 1;

        }

    }
}
