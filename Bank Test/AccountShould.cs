using De_Bank.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Bank_Test
{
    [TestClass]
    public class AccountShould
    {

         [TestInitialize]
        public void TestInitialize()
        {
            Account account1 = new Account
            {
                Id = 1,
                AccountBalance = 1000.00,
                AccountMinimum = 0.00,

            };
            Account account2 = new Account
            {
                Id = 2,
                AccountBalance = 1000.00,
                AccountMinimum = 0.00,
            };

            AccountHolder accountHolder = new AccountHolder
            {
                Id = 1,
                AccountHolderName = "John Wilder",
            };

            Transaction transaction = new Transaction
            {
                TransactionAmount = 2
            };
        }

        [TestMethod]
        public void StartAmountMustBeZero()
        {
            var account = new Account();

            Assert.AreEqual(0, account.AccountBalance);
        }

      

    }
}
