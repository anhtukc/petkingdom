using PetKingdomFN.Models;

namespace PetKingdomFN.Interfaces
{
    public interface IAuthentication
    {
        Task<Account> CheckAccount(string username, string password);
    }
}
