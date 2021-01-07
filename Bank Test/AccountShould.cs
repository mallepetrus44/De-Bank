using De_Bank.Logic;
using De_Bank.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Bank_Test
{
    [TestClass]
    public class AccountShould
    {
        Account account1;
        Account account2;
        Transaction transaction;
        AccountHolder accountHolder1;
        AccountHolder accountHolder2;
        BankLogic bankLogic;

        [TestInitialize]

        public void TestInitialize()
        {
            account1 = new Account();
            account2 = new Account();
            transaction = new Transaction();
            accountHolder1 = new AccountHolder();
            accountHolder2 = new AccountHolder();
            bankLogic = new BankLogic();
        }


        [TestMethod]
        [TestCategory("Account")]
        public void StartAmountMustBeZero()
        {
            Assert.AreEqual(0, account1.AccountBalance);
        }


        [TestMethod]
        [TestCategory("Transaction")]
        public void MakeTransactionTest()
        {
            
        }

    }


}
