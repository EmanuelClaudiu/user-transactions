using UserTransactions.Domain.Entities;

namespace UserTransactions.Infrastructure.Interfaces
{
    public interface IUserRepository : IGenericRepository<User>
    {
        Task<User?> GetByIdAsync(string id);
        Task DeleteByIdAsync(string id);
    }
}
