using UserTransactions.Domain.Enums;

namespace UserTransactions.Application.DTOs
{
    public class TransactionCreateDTO
    {
        public string UserId { get; set; }
        public decimal Amount { get; set; }
        public TransactionTypeEnum TransactionType { get; set; }
    }
}
