using PetKingdomFN.BusEntities;
using PetKingdomFN.Models;

namespace PetKingdomFN.Interfaces
{
    public interface IEmployeeRepository
    {
        
        Task<DataList<Employee>> GetPageList(Pagination page);
        Task<DataList<Employee>> SearchEmployee(Pagination page, basedSearchObject searchObj);
        Task<Employee> GetEmployeeById(string id);
        Task<Employee> AddEmployee(Employee acc);
        Task<Employee> UpdateEmployee(Employee acc);
        Task<int> DeleteEmployee(string id);
    }
}
