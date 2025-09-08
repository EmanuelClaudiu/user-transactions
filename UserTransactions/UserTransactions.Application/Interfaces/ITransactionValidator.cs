using UserTransactions.Application.DTOs;

namespace UserTransactions.Application.Interfaces
{
    public interface ITransactionValidator
    {
        Task ValidateTransactionUserAsync(TransactionDTO transaction);
        void ValidateTransactionAmount(TransactionDTO transaction);
        void ValidateTransactionType(TransactionDTO transaction);
    }
}
