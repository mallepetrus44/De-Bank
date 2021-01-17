using De_Bank.Logic;
using Bank.DAL.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Threading.Tasks;

namespace Bank_Test
{
    [TestClass]
    public class BankLogicTest : BankLogic
    {
        Account account;
        IdentityHolder identityHolder;
        //Transaction transaction;
        BankLogic bankLogic;

        [TestInitialize]
        public void TestInitialize()
        {
            account = new Account
            {
                Id = 11,
                AccountBalance = 1000,
                AccountMinimum = 0
            };

            identityHolder = new IdentityHolder
            {
                FirstName = "John",
                MiddleName = "David",
                LastName = "Wilder"

            };

            //transaction = new Transaction
            //{

            //};

            bankLogic = new BankLogic();
        }


        // door aanpassingen veranderd. nu op basis van een int (zie accountcontroller => Create voor meer details)

        [TestMethod]
        public async Task AccountNummerCheck()
        {
            account.AccountNumber = await GetNextAccountNumber(11);

            Assert.AreEqual("NL71LYMB000000012", account.AccountNumber);
        }


        // door aanpassingen veranderd. CreateAccountAsync is vervallen (zie accountcontroller => Create voor meer details)


        //[TestMethod]
        //public async Task CreateAccountCheck()
        //{
        //    var result = await CreateAccountAsync();

        //    Assert.AreEqual("John David Wilder", result.IdentityHolder.FullName);
        //    Assert.AreEqual("NL71LYMB000000001", result.AccountNumber);
        //}
    }
}
