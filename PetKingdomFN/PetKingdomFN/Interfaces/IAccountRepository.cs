using PetKingdomFN.BusEntities;
using PetKingdomFN.Models;

namespace PetKingdomFN.Interfaces
{
    public interface IAccountRepository
    {
        Task<AccountDataList> GetPageList(Pagination page);
        Task<AccountDataList> SearchAccount(Pagination page, basedSearchObject searchObj);
        Task<Account> GetAccountById(string id);
        Task<Account> AddAccount(Account acc);
        Task<Account> UpdateAccount(Account acc);
        Task<int> DeleteAccount(string id);
    }
}
