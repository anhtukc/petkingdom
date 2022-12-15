using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore.DynamicLinq;
using Microsoft.IdentityModel.Tokens;
using PetKingdomFN.Interfaces;
using PetKingdomFN.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace PetKingdomFN.Repositories
{
    public class AccountRepository:IAuthentication
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
    }
}
