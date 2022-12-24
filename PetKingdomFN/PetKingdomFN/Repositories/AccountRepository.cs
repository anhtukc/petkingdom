
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
        public async Task<Account> CheckAccount(string username, string password)
        {
            Account acc = new Account();
            acc = await _DbContext.Accounts.Where(x => x.Username == username && x.Password == password).FirstAsync();
            return  acc;
     
        }
        public async Task<AccountDataList> GetPageList(Pagination page)
        {
            AccountDataList result = new AccountDataList();
            string sortQuery = page.sortColumn + " " + page.sortOrder;
            List<Account> allData = await _DbContext.Accounts.OrderBy(sortQuery).ToListAsync();
            result.numberOfRecords = allData.Count();
            result.list = allData.Skip((page.currentPage - 1) * page.pageSize)
                .Take(page.pageSize)
                .ToList();
            return result;
        }

        public async Task<AccountDataList> SearchAccount(Pagination page, basedSearchObject searchObj)
        {
            AccountDataList result = new AccountDataList();
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
        public async Task<Account> AddAccount(Account service)
        {          
            service.Id = Guid.NewGuid().ToString("D");
            service.CreatedDate = DateTime.Now;
            service.UpdateDate = DateTime.Now;
            var obj = _DbContext.Accounts.AddAsync(service);
            await _DbContext.SaveChangesAsync();
            return obj.Result.Entity;
        }
        public async Task<Account> UpdateAccount(Account service)
        {
            service.UpdateDate = DateTime.Now;
            _DbContext.Entry(service).State = EntityState.Modified;
            await _DbContext.SaveChangesAsync();
            return service;
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
