using System;
using Microsoft.Extensions.Configuration;
using DBModels;
using WalletSystem;

namespace WalletSystem
{
    class Program
    {
        private static IWalletRepository _repository;

        static void Main(string[] args)
        {
            IConfiguration configuration = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json")
                .Build();

            _repository = new WalletRepository(configuration);

            // Register a new user
            var newAccount = new Accounts
            {
                LoginName = "john_doe",
                AccountNumber = GenerateAccountNumber(),
                Password = "password123",
                Balance = 0,
                RegisterDate = DateTime.Now
            };

            _repository.RegisterUser(newAccount);

            // Deposit/Withdraw from the user's account
            var user = _repository.GetUserByLoginName("john_doe");
            if (user != null)
            {
                user.Balance += 100; // Deposit
                _repository.UpdateUser(user);

                user.Balance -= 50; // Withdraw
                _repository.UpdateUser(user);
            }

            // Transfer funds between users
            var sender = _repository.GetUserByLoginName("john_doe");
            var receiver = _repository.GetUserByLoginName("jane_smith");
            if (sender != null && receiver != null)
            {
                // Transfer funds from sender to receiver
                var transferAmount = 50;
                if (sender.Balance >= transferAmount)
                {
                    sender.Balance -= transferAmount;
                    receiver.Balance += transferAmount;

                    _repository.UpdateUser(sender);
                    _repository.UpdateUser(receiver);

                    // Add transaction records
                    var transactionFrom = new Transactions
                    {
                        TransactionType = "Transfer",
                        Amount = transferAmount,
                        FromAccountId = sender.AccountNumber,
                        ToAccountId = receiver.AccountNumber,
                        DateOfTransaction = DateTime.Now,
                        EndBalance = sender.Balance
                    };
                    _repository.AddTransaction(transactionFrom);

                    var transactionTo = new Transactions
                    {
                        TransactionType = "Transfer",
                        Amount = transferAmount,
                        FromAccountId = sender.AccountNumber,
                        ToAccountId = receiver.AccountNumber,
                        DateOfTransaction = DateTime.Now,
                        EndBalance = receiver.Balance
                    };
                    _repository.AddTransaction(transactionTo);
                }
            }

            // View transaction history
            var transactions = _repository.GetTransactionsByAccountNumber(user.AccountNumber);
            foreach (var transaction in transactions)
            {
                Console.WriteLine($"Transaction Type: {transaction.TransactionType}");
                Console.WriteLine($"Amount: {transaction.Amount}");
                Console.WriteLine($"Account Number (From): {transaction.FromAccountId}");
                Console.WriteLine($"Account Number (To): {transaction.ToAccountId}");
                Console.WriteLine($"Date of Transaction: {transaction.DateOfTransaction}");
                Console.WriteLine($"End Balance: {transaction.EndBalance}");
                Console.WriteLine();
            }
        }

        private static string GenerateAccountNumber()
        {
            var random = new Random();
            return random.Next(1000000000, 999999999).ToString();
        }
    }
}
