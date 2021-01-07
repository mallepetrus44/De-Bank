using De_Bank.Logic;
using De_Bank.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Bank_Test
{
    [TestClass]
    public class BankLogicTest
    {
        [TestMethod]
        public void TestMethod1()
        {
            var sut = new AccountHolder();

            Assert.IsNull(sut.AccountHolderName);
        }
    }
}
