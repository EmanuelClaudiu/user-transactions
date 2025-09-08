using Microsoft.EntityFrameworkCore;
using UserTransactions.Domain.Entities;
using UserTransactions.Domain.Enums;
using UserTransactions.Infrastructure.Data;
using UserTransactions.Infrastructure.Interfaces;

namespace UserTransactions.Infrastructure.Repositories
{
    public class TransactionRepository : GenericRepository<Transaction>, ITransactionsRepository
    {
        public TransactionRepository(ApplicationDbContext dbContext) : base(dbContext) { }
        public async Task<Transaction?> GetByIdAsync(int id)
        {
            return await _dbContext.Set<Transaction>().AsNoTracking().FirstOrDefaultAsync(transaction => transaction.Id == id);
        }

        public async Task<decimal> GetTotalAmountForUserAsync(string userId)
        {
            return await _dbContext.Transactions
                                   .Where(transaction => transaction.UserId == userId)
                                   .SumAsync(transaction => transaction.Amount);
        }

        public async Task<decimal> GetTotalAmountForTransactionTypeAsync(TransactionTypeEnum transactionType)
        {
            return await _dbContext.Transactions
                                   .Where(transaction => transaction.TransactionType == transactionType)
                                   .SumAsync(transaction => transaction.Amount);
        }

        public async Task<IEnumerable<Transaction>> GetTransactionsOverTresholdAsync(decimal treshold)
        {
            return await _dbContext.Transactions
                                   .Where(transaction => transaction.Amount > treshold)
                                   .ToListAsync();
        }
    }
}
