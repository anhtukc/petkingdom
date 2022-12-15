namespace PetKingdomFN.Interfaces
{
    public interface IAccountRepository
    {
        Task<string> SignIn();
    }
}
