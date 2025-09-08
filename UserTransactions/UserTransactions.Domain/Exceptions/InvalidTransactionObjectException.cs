namespace UserTransactions.Domain.Exceptions
{
    public class InvalidTransactionObjectException : Exception
    {
        public InvalidTransactionObjectException(string message) : base(message)
        { }
    }
}
