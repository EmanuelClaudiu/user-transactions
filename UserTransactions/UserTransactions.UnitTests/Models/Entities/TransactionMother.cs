using UserTransactions.Application.DTOs;
using UserTransactions.Domain.Entities;
using UserTransactions.UnitTests.Models.Common;

namespace UserTransactions.UnitTests.Models.Entities
{
    public static class TransactionMother
    {
        public static Transaction Simple()
        {
            return new Transaction
            {
                Id = GetRandom.RandomInt(),
                UserId = GetRandom.RandomString(20),
                Amount = GetRandom.RandomDecimal(0.01m, 10000m),
                TransactionType = GetRandom.RandomTransactionType(),
                CreatedAt = DateTime.Now
            };
        }

        public static Transaction WithValues(this Transaction source, TransactionDTO transactionDto)
        {
            source.Id = transactionDto.Id;
            source.UserId = transactionDto.UserId;
            source.Amount = transactionDto.Amount;
            source.TransactionType = transactionDto.TransactionType;
            source.CreatedAt = transactionDto.CreatedAt;

            return source;
        }
    }
}
