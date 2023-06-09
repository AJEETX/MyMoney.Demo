using MyMoney.Demo.Domain;
using MyMoney.Demo.Test.SampleData;

using Xunit;

namespace MyMoney.Demo.Test.Domain.UnitTest.Command
{
    public class BalanceCommandTest
    {
        private static readonly int equity = 6000;
        private static readonly int debt = 3000;
        private static readonly int gold = 1000;

        [Fact]
        public void Transact_WhenInValidInput_ShouldNotGetFunds()
        {
            //given
            string unExpectedCommand = "ABCXYZ";
            string month = "January";
            string input = $"{unExpectedCommand} {month}";

            var sut = new BalanceCommand();

            //when
            sut.Transact(input);

            //then
            Assert.Null(TestData.Portfolio);
        }

        [Fact]
        public void Transact_WhenValidInput_ShouldGetFundsSuccessFully()
        {
            //given
            string ALLOCATE = "ALLOCATE";
            var allocate = new AllocateCommand();
            allocate.Transact($"{ALLOCATE} {equity} {debt} {gold}");

            string expectedCommand = "BALANCE";
            string month = "JANUARY";
            string input = $"{expectedCommand} {month}";
            var sut = new BalanceCommand();

            //when
            sut.Transact(input);

            //then
            Assert.Equal(TestData.Portfolio.Allocate.Equity, equity);
            Assert.Equal(TestData.Portfolio.Allocate.Debt, debt);
            Assert.Equal(TestData.Portfolio.Allocate.Gold, gold);
        }
    }
}