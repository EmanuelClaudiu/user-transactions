using UserTransactions.Application.DTOs;
using UserTransactions.UnitTests.Models.Common;

namespace UserTransactions.UnitTests.Models.DTOs
{
    public static class TransactionDtoMother
    {
        public static TransactionDTO Simple()
        {
            return new TransactionDTO
            {
                Id = GetRandom.RandomInt(),
                UserId = GetRandom.RandomString(20),
                Amount = GetRandom.RandomDecimal(0.01m, 10000m),
                TransactionType = GetRandom.RandomTransactionType(),
                CreatedAt = DateTime.Now
            };
        }

        public static TransactionDTO WithValues(this TransactionDTO source, TransactionCreateDTO transactionCreateDto)
        {
            source.UserId = transactionCreateDto.UserId;
            source.Amount = transactionCreateDto.Amount;
            source.TransactionType = transactionCreateDto.TransactionType;
            return source;
        }
    }
}
