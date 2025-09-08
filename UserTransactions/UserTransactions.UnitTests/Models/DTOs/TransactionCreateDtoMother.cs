using UserTransactions.Application.DTOs;
using UserTransactions.Domain.Enums;
using UserTransactions.UnitTests.Models.Common;

namespace UserTransactions.UnitTests.Models.DTOs
{
    public static class TransactionCreateDtoMother
    {
        public static TransactionCreateDTO Simple()
        {
            return new TransactionCreateDTO
            {
                UserId = GetRandom.RandomString(20),
                Amount = GetRandom.RandomDecimal(0.01m, 10000m),
                TransactionType = GetRandom.RandomTransactionType()
            };
        }

        public static TransactionCreateDTO WithUserId(this TransactionCreateDTO source, string userId)
        {
            source.UserId = userId;
            return source;
        }

        public static TransactionCreateDTO WithAmount(this TransactionCreateDTO source, decimal amount)
        {
            source.Amount = amount;
            return source;
        }

        public static TransactionCreateDTO WithTransactionType(this TransactionCreateDTO source, TransactionTypeEnum transactionType)
        {
            source.TransactionType = transactionType;
            return source;
        }
    }
}
