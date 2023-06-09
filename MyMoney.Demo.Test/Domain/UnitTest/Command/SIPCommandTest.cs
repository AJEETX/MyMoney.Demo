using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using MyMoney.Demo.Domain;
using MyMoney.Demo.Test.SampleData;

using Xunit;

namespace MyMoney.Demo.Test.Domain.UnitTest.Command
{
    public class SIPCommandTest
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

            var sut = new SIPCommand();

            //when
            sut.Transact(input);

            //then
            Assert.Null(TestData.Portfolio);
        }

        [Fact]
        public void Transact_WhenValidInput_ShouldBeSetFundsSuccessFully()
        {
            //given
            string ALLOCATE = "ALLOCATE";
            var allocate = new AllocateCommand();
            allocate.Transact($"{ALLOCATE} {equity} {debt} {gold}");

            string expectedCommand = "SIP";
            string input = $"{expectedCommand} {equity} {debt} {gold}";
            var sut = new SIPCommand();

            //when
            sut.Transact(input);

            //then
            Assert.Equal(TestData.Portfolio.SIP.Equity, equity);
            Assert.Equal(TestData.Portfolio.SIP.Debt, debt);
            Assert.Equal(TestData.Portfolio.SIP.Gold, gold);
        }
    }
}