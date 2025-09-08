using Microsoft.EntityFrameworkCore;
using UserTransactions.Domain.Entities;
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
    }
}
