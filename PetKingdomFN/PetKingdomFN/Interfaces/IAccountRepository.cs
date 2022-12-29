using PetKingdomFN.BusEntities;
using PetKingdomFN.Models;

namespace PetKingdomFN.Interfaces
{
    public interface IAccountRepository
    {
        Task<string> CheckCustomerAccount(string username, string phonenumber, string email);
        Task<DataList<Account>> GetPageList(Pagination page);
        Task<DataList<Account>> SearchAccount(Pagination page, basedSearchObject searchObj);
        Task<Account> GetAccountById(string id);
        Task<Account> AddAccount(Account acc);
        Task<Account> UpdateAccount(Account acc);
        Task<int> DeleteAccount(string id);
    }
}
