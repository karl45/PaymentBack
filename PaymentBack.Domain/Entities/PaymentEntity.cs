using PaymentBack.Domain.Enums;

namespace PaymentBack.Domain.Entities
{
    public class PaymentEntity
    {
        public long Id { get; set; }

        public required string WalletNumber { get; set; }

        public required long Account { get; set; }

        public required string Email { get; set; }

        public string? Phone { get; set; }

        public required decimal Amount { get; set; }

        public required string Currency { get; set; }

        public string? Comment { set; get; }

        public DateTime CreatedAt { set; get; }

        public Status Status { set; get; }
    }
}
