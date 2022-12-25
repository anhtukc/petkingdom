
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
    public class GroupProductRepository : IGroupProductRepository
    {
        private readonly PetKingdomContext _DbContext;
        public GroupProductRepository(PetKingdomContext DbContext)
        {
            _DbContext = DbContext;
        }
        public async Task<DataList<GroupProduct>> GetPageList(Pagination page)
        {
            DataList<GroupProduct> result = new DataList<GroupProduct>();
            string sortQuery = page.sortColumn + " " + page.sortOrder;
            List<GroupProduct> allData = await _DbContext.GroupProducts.OrderBy(sortQuery).ToListAsync();
            result.numberOfRecords = allData.Count();
            result.list = allData.Skip((page.currentPage - 1) * page.pageSize)
                .Take(page.pageSize)
                .ToList();
            return result;
        }

        public async Task<DataList<GroupProduct>> SearchGroupProduct(Pagination page, basedSearchObject searchObj)
        {
            DataList<GroupProduct> result = new DataList<GroupProduct>();
            string sortQuery = page.sortColumn + " " + page.sortOrder;

            List<GroupProduct> allData = await _DbContext.GroupProducts
                .Where(x => (string.IsNullOrEmpty(searchObj.name) || x.Name.Contains(searchObj.name)))
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
        public async Task<GroupProduct> AddGroupProduct(GroupProduct gp)
        {
            gp.Id = Guid.NewGuid().ToString();
            gp.CreatedDate = DateTime.Now;
            gp.UpdateDate = DateTime.Now;
            var obj = _DbContext.GroupProducts.AddAsync(gp);
            await _DbContext.SaveChangesAsync();
            return obj.Result.Entity;
        }
        public async Task<GroupProduct> UpdateGroupProduct(GroupProduct gp)
        {
            gp.UpdateDate = DateTime.Now;
            _DbContext.Entry(gp).State = EntityState.Modified;
            await _DbContext.SaveChangesAsync();
            return gp;
        }
        public async Task<GroupProduct> GetGroupProductById(string id)
        {
            var obj = await _DbContext.GroupProducts.Where(x => x.Id == id).FirstAsync();
            return obj;
        }
        public async Task<int> DeleteGroupProduct(string id)
        {
            var GroupProduct = await _DbContext.GroupProducts.FindAsync(id);
            if (GroupProduct is null)
            {
                return 0;
            }
            _DbContext.GroupProducts.Remove(GroupProduct);
            await _DbContext.SaveChangesAsync();
            return 1;

        }
    }
}
