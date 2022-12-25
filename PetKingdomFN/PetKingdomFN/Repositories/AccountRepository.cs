
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
    public class AccountRepository:IAccountRepository
    {
        private readonly PetKingdomContext _DbContext;
        public AccountRepository(PetKingdomContext DbContext) {
            _DbContext = DbContext;
        }
        public async Task<DataList<Account>> GetPageList(Pagination page)
        {
            DataList<Account> result = new DataList<Account>();
            string sortQuery = page.sortColumn + " " + page.sortOrder;
            List<Account> allData = await _DbContext.Accounts.OrderBy(sortQuery).ToListAsync();
            result.numberOfRecords = allData.Count();
            result.list = allData.Skip((page.currentPage - 1) * page.pageSize)
                .Take(page.pageSize)
                .ToList();
            return result;
        }

        public async Task<DataList<Account>> SearchAccount(Pagination page, basedSearchObject searchObj)
        {
            DataList<Account> result = new DataList<Account>();
            string sortQuery = page.sortColumn + " " + page.sortOrder;

            List<Account> allData = await _DbContext.Accounts
                .Where(x => (string.IsNullOrEmpty(searchObj.name) || x.Username.Contains(searchObj.name)))
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
        public async Task<Account> AddAccount(Account acc)
        {          
            acc.Id = Guid.NewGuid().ToString();
            acc.CreatedDate = DateTime.Now;
            acc.UpdateDate = DateTime.Now;
            var obj = _DbContext.Accounts.AddAsync(acc);
            await _DbContext.SaveChangesAsync();
            return obj.Result.Entity;
        }
        public async Task<Account> UpdateAccount(Account acc)
        {
            acc.UpdateDate = DateTime.Now;
            _DbContext.Entry(acc).State = EntityState.Modified;
            await _DbContext.SaveChangesAsync();
            return acc;
        }
        public async Task<Account> GetAccountById(string id)
        {
            var obj = await _DbContext.Accounts.Where(x => x.Id == id).FirstAsync();
            return obj;
        }
        public async Task<int> DeleteAccount(string id)
        {
            var Account = await _DbContext.Accounts.FindAsync(id);
            if (Account is null)
            {
                return 0;
            }
            _DbContext.Accounts.Remove(Account);
            await _DbContext.SaveChangesAsync();
            return 1;

        }
    }
}
