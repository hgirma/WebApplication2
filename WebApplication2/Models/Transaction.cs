namespace WebApplication2.Models
{
    public class Transaction : BaseModel
    {
        public int Id { get; set; }

        public TransactionKind TransactionKind { get; set; }

        public string? UserId { get; set; }

        public int OpportunityId { get; set; }

        public string? OpportunityType { get; set; }

        public int CurrencyId { get; set; }

        public string? CurrencyType { get; set; }

        public double Amount { get; set; }

        public string? Note { get; set; }

        public string? CreatedBy { get; set; }

        public DateTime? CreatedDate { get; set; } = TimeProvider.System.GetLocalNow().LocalDateTime;

        public bool IsDeleted { get; set; }

        public virtual AppUser? User { get; set; }
    }
}
