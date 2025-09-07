using Microsoft.EntityFrameworkCore;
using UserTransactions.Domain.Entities;

namespace UserTransactions.Infrastructure.Data
{
    public class ApplicationDbContext : DbContext
    {
        public DbSet<User> Users { get; set; }

        public string DbConnString { get; }
        public ApplicationDbContext()
        {
            // connection details ought to be pulled from a safer place (i.e. env variables, secrets, etc.)
            // for simplicity I'll just use the localdb default connection
            DbConnString = "Server=(localdb)\\MSSQLLocalDB;Database=UserTransactions;Trusted_Connection=True;MultipleActiveResultSets=true";
        }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
            DbConnString = "Server=(localdb)\\MSSQLLocalDB;Database=UserTransactions;Trusted_Connection=True;MultipleActiveResultSets=true";
        }
        protected override void OnConfiguring(DbContextOptionsBuilder options)
            => options.UseSqlServer($"{DbConnString}");
    }
}
