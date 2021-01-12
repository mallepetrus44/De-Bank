using De_Bank.Logic;
using De_Bank.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Bank_Test
{
    [TestClass]
    public class BankLogicTest
    {
        Account account;
        AccountHolder accountHolder;
        Transaction transaction;

        [TestInitialize]
        //public void TestInitialize()
        //{
        //    account = new Account
        //    {
        //        Id = 1,
        //        AccountNumber = "102030",
        //        AccountBalance = 1000.00,
        //        AccountMinimum = 0.00,
        //        //AccountHolder =   
        //        //Transactions = 
        //    };

        //    accountHolder = new AccountHolder
        //    {
        //        Id = 1,
        //        //AccountHolderName = "John Wilder",
        //        //Accounts =
        //    };

        //    transaction = new Transaction
        //    {

        //    };
        //}


        [TestMethod]
        public void CheckAcountHolderName()
        {
            //Assert.AreEqual("John Wilder", accountHolder.AccountHolderName, true);
        }
    }
}
