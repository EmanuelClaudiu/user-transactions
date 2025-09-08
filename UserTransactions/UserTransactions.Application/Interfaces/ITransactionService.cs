using UserTransactions.Application.DTOs;
using UserTransactions.Domain.Enums;

namespace UserTransactions.Application.Interfaces
{
    public interface ITransactionService
    {
        Task<IEnumerable<TransactionDTO>> GetAllTransactionsAsync();
        Task<TransactionDTO?> GetTransactionByIdAsync(int id);
        Task<TransactionDTO> AddTransactionAsync(TransactionCreateDTO transactionCreateDto);
        Task<decimal> CalculateTotalAmountForUserAsync(string userId);
        Task<decimal> CalculateTotalAmountForTransactionTypeAsync(TransactionTypeEnum transactionType);
        Task<IEnumerable<TransactionDTO>> GetTransactionsOverTresholdAsync(decimal treshold);
    }
}
