using UserTransactions.Domain.Entities;
using UserTransactions.Domain.Enums;

namespace UserTransactions.Infrastructure.Interfaces
{
    public interface ITransactionsRepository : IGenericRepository<Transaction>
    {
        Task<Transaction?> GetByIdAsync(int id);
        Task<decimal> GetTotalAmountForUserAsync(string userId);
        Task<decimal> GetTotalAmountForTransactionTypeAsync(TransactionTypeEnum transactionType);
        Task<IEnumerable<Transaction>> GetTransactionsOverTresholdAsync(decimal treshold);
    }
}
