using Microsoft.Extensions.Time.Testing;
using NSubstitute;
using WebApplication2.Models;
using WebApplication2.Services;

namespace WebApplication2.Tests
{
    public class UnitTest1
    {
        private readonly ITransactionRepository _transactionRepository;
        private readonly ShopService _sut;
        private readonly FakeTimeProvider _timeProvider = new();

        public UnitTest1()
        {
            _transactionRepository = Substitute.For<ITransactionRepository>();

            _sut = new ShopService(_transactionRepository, _timeProvider);
        }

        [Fact]
        public async Task Test1()
        {
            // Arrange
            var timeNow = DateTime.Now;
            _timeProvider.SetLocalTimeZone(TimeZoneInfo.Local);
            _timeProvider.SetUtcNow(timeNow);

            var shopItem = new ShopItem
            {
                Id = 1,
                Name = "Test",
                Description = "Test",
                Amount = 1
            };

            var user = new AppUser
            {
                Id = "0",
                Firstname = "Test",
                Lastname = "test2",
                CreatedDate = timeNow
            };

            var userAdded = new AppUser
            {
                Id = "1",
                Firstname = "Test",
                Lastname = "test2",
                CreatedDate = timeNow
            };

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
                CreatedDate = timeNow,
                IsDeleted = false,
            };

            var transactionAdded = new Transaction
            {
                Id = 1,
                Amount = -shopItem.Amount,
                CreatedBy = user.Id,
                CurrencyId = shopItem.CostCurrencyTypeId,
                CurrencyType = shopItem.CostCurrencyType,
                Note = $"Redeemed: {shopItem.Description}",
                UserId = user.Id,
                OpportunityType = "Redeem",
                TransactionKind = TransactionKind.Shop,
                CreatedDate = timeNow,
                IsDeleted = false,
            };

            // this fails too, it returns null in _sut.RedeemAsync, so opted to set the return value in the next line
            // with Arg.Any<Transaction>()
            // _transactionRepository.AddAsync(transaction).Returns(transactionAdded);

            _transactionRepository.AddAsync(Arg.Any<Transaction>()).Returns(transactionAdded);

            _transactionRepository.TestAppUserAsync(user).Returns(userAdded);

            // Act
            _ = await _sut.RedeemAsync(user, shopItem);

            // Assert
            await _transactionRepository.Received(1).TestAppUserAsync(user); // this passes
            await _transactionRepository.Received(1).AddAsync(transaction); // this fails

            // this passes
            await _transactionRepository.Received(1).AddAsync(
                Arg.Is<Transaction>(x =>
                x.TransactionKind == transaction.TransactionKind &&
                x.UserId == transaction.UserId &&
                x.OpportunityId == transaction.OpportunityId &&
                x.OpportunityType == transaction.OpportunityType &&
                x.CurrencyId == transaction.CurrencyId &&
                x.CurrencyType == transaction.CurrencyType &&
                x.Amount == transaction.Amount &&
                x.Note == transaction.Note &&
                x.CreatedBy == transaction.CreatedBy &&
                x.CreatedDate == transaction.CreatedDate &&
                x.IsDeleted == transaction.IsDeleted));
        }
    }
}