using WebApplication2.Models;

namespace WebApplication2.Services
{
    public interface ITransactionRepository
    {
        Task<Transaction> AddAsync(Transaction transaction);

        Task<AppUser> TestAppUserAsync(AppUser appUser);
    }
}
