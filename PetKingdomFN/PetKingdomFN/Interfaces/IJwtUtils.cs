namespace PetKingdomFN.Interfaces
{
    public interface IJwtUtils
    {
        public Task<string> GenerateJwtToken(string username, string password);
        public Task<int?> ValidateJwtToken(string token);
    }
}
