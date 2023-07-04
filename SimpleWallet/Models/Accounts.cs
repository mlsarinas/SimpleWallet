using System.ComponentModel.DataAnnotations;

namespace DBModels
{
    public class Account
    {
        [Required]
        public long AccountNumber { get; set; }
        [Required]
        public string UserName { get; set; }
        [Required]
        public string Password { get; set; }
        public double Balance { get; set; }
        public DateTime RegisterDate { get; set; }
    }
}
