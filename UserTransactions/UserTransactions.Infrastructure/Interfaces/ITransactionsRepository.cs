using UserTransactions.Domain.Entities;

namespace UserTransactions.Infrastructure.Interfaces
{
    public interface ITransactionsRepository : IGenericRepository<Transaction>
    {
        Task<Transaction?> GetByIdAsync(int id);
    }
}
