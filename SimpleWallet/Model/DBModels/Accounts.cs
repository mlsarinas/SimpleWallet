namespace DBModels
{
    public class Account
    {
        public long AccountNumber { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public decimal Balance { get; set; }
        public DateTime RegisterDate { get; set; }
    }
}
