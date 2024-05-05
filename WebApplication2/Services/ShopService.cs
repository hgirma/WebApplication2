using WebApplication2.Models;

namespace WebApplication2.Services
{
    public class ShopService(
        ITransactionRepository transactionRepository,
        TimeProvider timeProvider) : IShopService
    {
        private readonly ITransactionRepository _transactionRepository = transactionRepository;
        private readonly TimeProvider _timeProvider = timeProvider;

        public async Task<IList<ShopItem>> RedeemAsync(AppUser user, ShopItem shopItem)
        {
            var transaction = new Transaction
            {
                Amount = -shopItem.Amount,
                CreatedBy = user.Id,
                CurrencyId = shopItem.CostCurrencyTypeId,
                CurrencyType = shopItem.CostCurrencyType,
                Note = $"Redeemed: {shopItem.Description}",
                UserId = user.Id,
                OpportunityType = "Redeem",
                TransactionKind = TransactionKind.Shop,
                CreatedDate = _timeProvider.GetUtcNow().LocalDateTime,
                IsDeleted = false,
            };

            var userResult = await _transactionRepository.TestAppUserAsync(user);

            if (userResult.Id == "0")
            {
                throw new Exception("User failed");
            }

            var transactionResult = await _transactionRepository.AddAsync(transaction);

            if (transactionResult.Id <= 0)
            {
                throw new Exception("Transaction failed");
            }

            return [shopItem];
        }
    }
}
