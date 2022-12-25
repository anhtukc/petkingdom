
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
    public class EmployeeRepository : IEmployeeRepository
    {
        private readonly PetKingdomContext _DbContext;
        public EmployeeRepository(PetKingdomContext DbContext)
        {
            _DbContext = DbContext;
        }
    
        public async Task<DataList<Employee>> GetPageList(Pagination page)
        {
            DataList<Employee> result = new DataList<Employee>();
            string sortQuery = page.sortColumn + " " + page.sortOrder;
            List<Employee> allData = await _DbContext.Employees.OrderBy(sortQuery).ToListAsync();
            result.numberOfRecords = allData.Count();
            result.list = allData.Skip((page.currentPage - 1) * page.pageSize)
                .Take(page.pageSize)
                .ToList();
            return result;
        }

        public async Task<DataList<Employee>> SearchEmployee(Pagination page, basedSearchObject searchObj)
        {
            DataList<Employee> result = new DataList<Employee>();
            string sortQuery = page.sortColumn + " " + page.sortOrder;

            List<Employee> allData = await _DbContext.Employees
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
        public async Task<Employee> AddEmployee(Employee emp)
        {
            emp.Id = Guid.NewGuid().ToString();
            emp.CreatedDate = DateTime.Now;
            emp.UpdateDate = DateTime.Now;
            var obj = _DbContext.Employees.AddAsync(emp);
            await _DbContext.SaveChangesAsync();
            return obj.Result.Entity;
        }
        public async Task<Employee> UpdateEmployee(Employee emp)
        {
            emp.UpdateDate = DateTime.Now;
            _DbContext.Entry(emp).State = EntityState.Modified;
            await _DbContext.SaveChangesAsync();
            return emp;
        }
        public async Task<Employee> GetEmployeeById(string id)
        {
            var obj = await _DbContext.Employees.Where(x => x.Id == id).FirstAsync();
            return obj;
        }
        public async Task<int> DeleteEmployee(string id)
        {
            var Employee = await _DbContext.Employees.FindAsync(id);
            if (Employee is null)
            {
                return 0;
            }
            _DbContext.Employees.Remove(Employee);
            await _DbContext.SaveChangesAsync();
            return 1;

        }
    }
}
