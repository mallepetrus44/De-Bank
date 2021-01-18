using System;
using De_Bank.Logic;
using Bank.DAL.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Security.Claims;

namespace Bank_Test
{
    [TestClass]
    public class BankLogicTest : BankLogic
    {
        Account account;
        Account account2;
        Account account3;
        IdentityHolder identityHolder;
        Transaction transaction;
        Transaction transaction2;
        BankLogic bankLogic;

        [TestInitialize]
        public void TestInitialize()
        {
            account = new Account
            {
                Id = 11,
                AccountNumber = "NL71LYMB000000012",
                AccountBalance = 500,
                AccountMinimum = 0,
                AccountType = 0
            };
            account2 = new Account
            {
                Id = 12,
                AccountNumber = "NL71LYMB000000013",
                AccountBalance = 100,
                AccountMinimum = 0,
                AccountType = 0
            };
            account3 = new Account
            {
                Id = 13,
                AccountNumber = "NL71LYMB000000014",
                AccountBalance = 1000,
                AccountMinimum = 0,
                AccountType = 0
            };

            

            identityHolder = new IdentityHolder
            {
                FirstName = "John",
                MiddleName = "David",
                LastName = "Wilder"
            };



            transaction = new Transaction
            {
                Id = 1,
                TransactionAmount = 50,
                TransactionDate = DateTime.Now.AddSeconds(-40),
                AccountTo = "NL71LYMB000000015",
                IsPeriodic = false
            };
            transaction2 = new Transaction
            {
                Id = 2,
                TransactionAmount = 50,
                TransactionDate = DateTime.Now.AddSeconds(-10),
                AccountTo = "NL71LYMB000000015",
                IsPeriodic = false
            };

            bankLogic = new BankLogic();
        }


        // door aanpassingen veranderd. nu op basis van een int (zie accountcontroller => Create voor meer details)

        [TestMethod]
        [TestCategory("AccountNummer")]
        public async Task AccountNummerCheck()
        {
            account.AccountNumber = await GetNextAccountNumber(11);

            Assert.AreEqual("NL71LYMB000000012", account.AccountNumber);
        }

        [TestMethod]
        [TestCategory("Transactions")]
        public async Task GetDataForSecondsCheck()
        {
            identityHolder.Transactions = new List<Transaction> { transaction, transaction2 };

            var sut = await GetDataForSeconds(30, identityHolder);

            List<Transaction> expectedTransactions = new List<Transaction> { transaction2 };

            CollectionAssert.AreEqual(expectedTransactions, sut);
        }

        [TestMethod]
        [TestCategory("Transactions")]
        public void GetAccountTransactionsCheck()
        {
            identityHolder.Transactions = new List<Transaction> { transaction, transaction2 };

            var sut = GetAccountTransactions(identityHolder);

            List<Transaction> expectedTransactions = new List<Transaction> { transaction, transaction2 };

            CollectionAssert.AreEqual(expectedTransactions, sut);
        }

        [TestMethod]
        [TestCategory("Transactions")]
        public void GetAllDebetFromAccountCheck()
        {
            identityHolder.Transactions = new List<Transaction> { transaction, transaction2 };

            var sut = GetAccountTransactions(identityHolder);

            List<Transaction> expectedTransactions = new List<Transaction> { transaction, transaction2 };

            CollectionAssert.AreEqual(expectedTransactions, sut);
        }


        [TestMethod]
        [TestCategory("AccountBalance")]
        public void GetAllBalancesAboveCheck()
        {
            List<Account> AllAccounts = new List<Account> { account, account2, account3 };

            var sut = GetAllBalancesAbove(AllAccounts, 500);

            List<Account> ExpectedAccounts = new List<Account> { account, account3 };

            CollectionAssert.AreEqual(ExpectedAccounts, sut);
        }

        [TestMethod]
        [TestCategory("AccountBalance")]
        public void GetAllBalancesBelowCheck()
        {
            List<Account> AllAccounts = new List<Account> { account, account2, account3 };

            var sut = GetAllBalancesBelow(AllAccounts, 500);

            List<Account> ExpectedAccounts = new List<Account> { account, account2 };

            CollectionAssert.AreEqual(ExpectedAccounts, sut);
        }
    }
}
