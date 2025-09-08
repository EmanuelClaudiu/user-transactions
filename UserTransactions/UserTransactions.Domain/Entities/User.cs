using System.ComponentModel.DataAnnotations;

namespace UserTransactions.Domain.Entities
{
    public class User
    {
        [Key]
        public string UserId { get; set; }
        public string Name { get; set; }
        public string Adress { get; set; }
        public IEnumerable<Transaction> Transatcions { get; set; }
    }
}
