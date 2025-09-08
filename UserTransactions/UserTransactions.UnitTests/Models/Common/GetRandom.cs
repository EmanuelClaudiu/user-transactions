using UserTransactions.Domain.Enums;

namespace UserTransactions.UnitTests.Models.Common
{
    public class GetRandom
    {
        public static string RandomString(int length, string allowedChars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789")
        {
            var random = new Random();

            return new string(Enumerable.Repeat(allowedChars, length)
                .Select(s => s[random.Next(s.Length)]).ToArray());
        }

        public static TransactionTypeEnum RandomTransactionType()
        {
            var values = Enum.GetValues(typeof(TransactionTypeEnum));
            var random = new Random();
            var randomTransactionType = (TransactionTypeEnum)values.GetValue(random.Next(values.Length));

            return randomTransactionType;
        }

        public static decimal RandomDecimal(decimal rangeMin, decimal rangeMax)
        {
            decimal value;
            var random = new Random();

            do
            {
                value = new decimal(random.NextDouble());
            }
            while (value < rangeMin || value > rangeMax);

            return value;
        }

        public static int RandomInt()
        {
            var random = new Random();
            
            return random.Next();
        }
    }
}
