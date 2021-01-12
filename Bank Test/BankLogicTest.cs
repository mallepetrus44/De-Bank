using De_Bank.Logic;
using De_Bank.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Threading.Tasks;

namespace Bank_Test
{
    [TestClass]
    public class BankLogicTest : BankLogic
    {
        Account account;
        AccountHolder accountHolder;
        //Transaction transaction;
        BankLogic bankLogic;

        [TestInitialize]
        public void TestInitialize()
        {
            account = new Account
            {
                Id = 11,
                AccountBalance = 1000.00M,
                AccountMinimum = 0.00M
            };

            accountHolder = new AccountHolder
            {
                Id = 1,
                FirstName = "John",
                MiddleName = "David",
                LastName = "Wilder"

            };

            //transaction = new Transaction
            //{

            //};

            bankLogic = new BankLogic();
        }


        [TestMethod]
        public async Task AccountNummerCheck()
        {
            account.AccountNumber = await GetNextAccountNumber(account);

            Assert.AreEqual("NL71LYMB000000011", account.AccountNumber);
        }

        [TestMethod]
        public async Task CreateAccountCheck() //Deze test werkt nog niet
        {
            var result = await CreateAccountAsync(accountHolder);

            Assert.AreEqual("John", result.AccountHolder.FirstName);
        }
    }
}
