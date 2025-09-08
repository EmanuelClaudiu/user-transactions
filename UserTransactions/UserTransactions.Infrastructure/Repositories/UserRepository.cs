using Microsoft.EntityFrameworkCore;
using UserTransactions.Domain.Entities;
using UserTransactions.Infrastructure.Data;
using UserTransactions.Infrastructure.Interfaces;

namespace UserTransactions.Infrastructure.Repositories
{
    public class UserRepository : GenericRepository<User>, IUserRepository
    {
        public UserRepository(ApplicationDbContext dbContext) : base(dbContext) { }
        public async Task<User?> GetByIdAsync(string id)
        {
            return await _dbContext.Set<User>().AsNoTracking().FirstOrDefaultAsync(user => user.UserId == id);
        }

        public async Task DeleteByIdAsync(string id)
        {
            var user = await GetByIdAsync(id);

            if (user == null)
            {
                throw new KeyNotFoundException($"User with id {id} not found.");
            }

            _dbContext.Set<User>().Remove(user);
            await _dbContext.SaveChangesAsync();
        }
    }
}
