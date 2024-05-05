using WebApplication2.Models;

namespace WebApplication2.Services
{
    public interface IShopService
    {
        Task<IList<ShopItem>> RedeemAsync(AppUser user, ShopItem shopItem);
    }
}
