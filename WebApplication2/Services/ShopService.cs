﻿using WebApplication2.Models;

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

            await _transactionRepository.TestAppUserAsync(user);

            var result = await _transactionRepository.AddAsync(transaction);

            if (result.Id <= 0)
            {
                throw new Exception("Transaction failed");
            }

            return [shopItem];
        }
    }
}
