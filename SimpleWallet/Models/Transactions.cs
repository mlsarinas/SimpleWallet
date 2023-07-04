using System.ComponentModel.DataAnnotations;

namespace DBModels
{
    public class Transaction
    {
        [Required]
        public string TransactionId { get; set; }
        public string TransactionType { get; set; }
        public string FromAccountId { get; set; }
        public string ToAccountId { get; set; }
        public DateTime TransactionDate { get; set; }
        public double BalanceAfterTransaction { get; set; }
        public double BalanceBeforeTransaction { get; set; }
    }
}