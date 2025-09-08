using UserTransactions.Application.DTOs;
using UserTransactions.Application.Interfaces;
using UserTransactions.Domain.Exceptions;

namespace UserTransactions.Application.Validators
{
    public class TransactionValidator : ITransactionValidator
    {
        private readonly IUserService _userService;

        public TransactionValidator(IUserService userService)
        {
            _userService = userService;
        }

        public async Task ValidateTransactionUserAsync(TransactionDTO transaction)
        {
            if ((await _userService.GetUserByIdAsync(transaction.UserId)) == null)
            {
                throw new UserNotFoundException($"User with id: {transaction.UserId} was not found.");
            }
        }
        public void ValidateTransactionAmount(TransactionDTO transaction)
        {
            if (transaction.Amount < 0.01m)
            {
                throw new InvalidTransactionObjectException("Transaction amount is smaller than the minimum allowed of 0.01.");
            }
        }
        public void ValidateTransactionType(TransactionDTO transaction)
        {
            if (!Enum.IsDefined(transaction.TransactionType))
            {
                throw new InvalidTransactionObjectException("Provided transaction type is invalid.");
            }
        }
    }
}
