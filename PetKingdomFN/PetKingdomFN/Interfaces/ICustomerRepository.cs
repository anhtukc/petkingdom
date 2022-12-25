using PetKingdomFN.BusEntities;
using PetKingdomFN.Models;

namespace PetKingdomFN.Interfaces
{
    public interface ICustomerRepository
    {
        Task<DataList<Customer>> GetPageList(Pagination page);
        Task<DataList<Customer>> SearchCustomer(Pagination page, basedSearchObject searchObj);
        Task<Customer> GetCustomerById(string id);
        Task<Customer> AddCustomer(Customer acc);
        Task<Customer> UpdateCustomer(Customer acc);
        Task<int> DeleteCustomer(string id);

    }
}
