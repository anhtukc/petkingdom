using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using PetKingdomFN.Interfaces;
using PetKingdomFN.Models;
using ServiceStack;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace PetKingdomFN.Repositories
{
    public class JwtUtils: IJwtUtils
    {
        private string Secret;
        private string Audience;
        private string Issuer;

        PetKingdomContext _DbContext;
        public JwtUtils(IConfiguration configuration, PetKingdomContext DbContext) {
            Audience = configuration["JWT:Audience"];
            Issuer = configuration["JWT:Issuer"];
            Secret = configuration["JWT:Secret"];
            _DbContext = DbContext;
        }   
        public async Task<string> GenerateJwtToken(string username, string password)
        {
            Account user = await _DbContext.Accounts.Where(x => x.Username == username && x.Password == password).FirstOrDefaultAsync();
            Customer cus = new Customer();
            Employee emp = new Employee();
            if (user is null)
                return  "Invalid account" ;
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(Secret);
            IdentityOptions _option = new IdentityOptions();



          
            if (user.Permission == "customer")
            {
                cus = await _DbContext.Customers.Where(x => x.AccountId == user.Id).FirstOrDefaultAsync();
            }
            else
            {
                emp = await _DbContext.Employees.Where(x => x.AccountId == user.Id).FirstOrDefaultAsync();
            }
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, user.Username),
                    new Claim("id", user.Id.ToString()),
                    new Claim(ClaimTypes.Role, user.Permission),
                   new Claim("userId", cus.Id is null? emp.Id.ToString():cus.Id.ToString())
                }),
                Expires = DateTime.UtcNow.AddHours(8),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var tmp = tokenHandler.CreateToken(tokenDescriptor);
            var token = tokenHandler.WriteToken(tmp);
            return token;
        }

        public async Task<int?> ValidateJwtToken(string token)
        {
            if (token == null)
                return null;

            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(Secret);
            try
            {
                tokenHandler.ValidateToken(token, new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ClockSkew = TimeSpan.Zero
                }, out SecurityToken validatedToken);

                var jwtToken = (JwtSecurityToken)validatedToken;
                int userId = int.Parse(jwtToken.Claims.First(x => x.Type == "id").Value);

                return userId;
            }
            catch
            {
                return null;
            }
        }
    }
}
