using UserTransactions.Application.DTOs;

namespace UserTransactions.Application.Interfaces
{
    public interface ITransactionService
    {
        Task<IEnumerable<TransactionDTO>> GetAllTransactionsAsync();
        Task<TransactionDTO?> GetTransactionByIdAsync(int id);
        Task<TransactionDTO> AddTransactionAsync(TransactionCreateDTO transactionCreateDto);
    }
}
