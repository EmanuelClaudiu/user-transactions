using UserTransactions.Domain.Enums;

namespace UserTransactions.Application.DTOs
{
    public class TransactionDTO
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public decimal Amount { get; set; }
        public TransactionTypeEnum TransactionType { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
