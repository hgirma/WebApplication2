namespace WebApplication2.Models
{
    public class ShopItem : BaseModel
    {
        public int Id { get; set; }

        public string? Name { get; set; }

        public string? Description { get; set; }

        public int Amount { get; set; }

        public int CostCurrencyTypeId { get; set; } = 1;

        public string CostCurrencyType { get; set; } = "USD";
    }
}