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
        private readonly ICloudStorageService _cloud;
        private readonly string folder = "img/";
        public CustomerRepository(PetKingdomContext DbContext, ICloudStorageService cloud)
        {
            _DbContext = DbContext;
            _cloud = cloud;
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
        public async Task<Customer> AddCustomer(Customer cus, Account acc)
        {
          
            acc.Id = Guid.NewGuid().ToString();
            acc.UpdateDate = DateTime.Now;
            acc.CreatedDate = DateTime.Now;
            cus.Id = Guid.NewGuid().ToString();
            if (!(cus.file is null))
            {
                cus.Image = "default";
            }
            cus.CreatedDate = DateTime.Now;
            cus.UpdateDate = DateTime.Now;
            cus.AccountId = acc.Id;
            acc.Customers.Add(cus);
            await _DbContext.Accounts.AddAsync(acc);
            await _DbContext.SaveChangesAsync();
            cus.Account= null;
            return cus;
        }
        public async Task<Customer> UpdateCustomer(Customer cus)
        {
            if (!(cus.file is null))
            {
                cus.Image = await _cloud.UploadFileAsync(cus.file, folder + cus.Id);
            }
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
