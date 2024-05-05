namespace WebApplication2.Models
{
    public enum TransactionKind
    {
        Default = 0,
        StockPurchase = 1,
        StockSell = 2,
        CryptoPurchase = 3,
        CryptoSell = 4,
        TransferIn = 5,
        TransferOut = 6,
        Opportunity = 7,
        Shop = 8,
    }
}
