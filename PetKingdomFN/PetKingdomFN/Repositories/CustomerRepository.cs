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
    public class CustomerRepository:ICustomerRepository
    {
        private readonly PetKingdomContext _DbContext;
        public CustomerRepository(PetKingdomContext DbContext)
        {
            _DbContext = DbContext;
        }
        public async Task<DataList<Customer>> GetPageList(Pagination page)
        {
            DataList<Customer> result = new DataList<Customer>();
            string sortQuery = page.sortColumn + " " + page.sortOrder;
            List<Customer> allData = await _DbContext.Customers.OrderBy(sortQuery).ToListAsync();
            result.numberOfRecords = allData.Count();
            result.list = allData.Skip((page.currentPage - 1) * page.pageSize)
                .Take(page.pageSize)
                .ToList();
            return result;
        }

        public async Task<DataList<Customer>> SearchCustomer(Pagination page, basedSearchObject searchObj)
        {
            DataList<Customer> result = new DataList<Customer>();
            string sortQuery = page.sortColumn + " " + page.sortOrder;

            List<Customer> allData = await _DbContext.Customers
                .Where(x => (string.IsNullOrEmpty(searchObj.name) || x.FirstName.Contains(searchObj.name)))
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
        public async Task<Customer> AddCustomer(Customer cus)
        {
            cus.Id = Guid.NewGuid().ToString();
            cus.CreatedDate = DateTime.Now;
            cus.UpdateDate = DateTime.Now;
            var obj = _DbContext.Customers.AddAsync(cus);
            await _DbContext.SaveChangesAsync();
            return obj.Result.Entity;
        }
        public async Task<Customer> UpdateCustomer(Customer cus)
        {
            cus.UpdateDate = DateTime.Now;
            _DbContext.Entry(cus).State = EntityState.Modified;
            await _DbContext.SaveChangesAsync();
            return cus;
        }
        public async Task<Customer> GetCustomerById(string id)
        {
            var obj = await _DbContext.Customers.Where(x => x.Id == id).FirstAsync();
            return obj;
        }
        public async Task<int> DeleteCustomer(string id)
        {
            var Customer = await _DbContext.Customers.FindAsync(id);
            if (Customer is null)
            {
                return 0;
            }
            _DbContext.Customers.Remove(Customer);
            await _DbContext.SaveChangesAsync();
            return 1;

        }
    }
}
