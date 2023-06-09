using MyMoney.Demo.Domain;
using MyMoney.Demo.Test.SampleData;

using Xunit;

namespace MyMoney.Demo.Test.Domain.UnitTest.Command
{
    public class AllocateCommandTest
    {
        private static readonly int equity = 6000;
        private static readonly int debt = 3000;
        private static readonly int gold = 1000;
        [Fact]
        public void Transact_WhenInValidInput_ShouldNotSetFunds()
        {
            //given
            string unExpectedCommand = "ABCXYZ";
            string input = $"{unExpectedCommand} {equity} {debt} {gold}";

            var sut = new AllocateCommand();

            //when
            sut.Transact(input);

            //then
            Assert.Null(TestData.Portfolio);
        }
        [Fact]
        public void Transact_WhenValidInput_ShouldBeSetFundsSuccessFully()
        {
            //given
            string expectedCommand = "ALLOCATE";
            string input = $"{expectedCommand} {equity} {debt} {gold}";
            var sut = new AllocateCommand();

            //when
            sut.Transact(input);

            //then
            Assert.Equal(TestData.Portfolio.Allocate.Equity, equity);
            Assert.Equal(TestData.Portfolio.Allocate.Debt , debt);
            Assert.Equal(TestData.Portfolio.Allocate.Gold , gold);
        }

    }
}
