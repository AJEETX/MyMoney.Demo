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
    public class ChangeCommandTest
    {
        private static readonly int equity = 6000;
        private static readonly int debt = 3000;
        private static readonly int gold = 1000;

        [Fact]
        public void Transact_WhenInValidInput_ShouldNotSetFunds()
        {
            //given
            string ALLOCATE = "ALLOCATE";
            var allocate = new AllocateCommand();
            allocate.Transact($"{ALLOCATE} {equity} {debt} {gold}");

            string unExpectedCommand = "ABCXYZ";
            string input = $"{unExpectedCommand}";

            var sut = new ChangeCommand();

            //when
            sut.Transact(input);

            //then
            Assert.True(TestData.Portfolio.ChangeRates.Count == 0);
        }

        [Fact]
        public void Transact_WhenValidInput_ShouldChangeFundsSuccessFully()
        {
            //given
            string ALLOCATE = "ALLOCATE";
            var allocate = new AllocateCommand();
            allocate.Transact($"{ALLOCATE} {equity} {debt} {gold}");
            decimal expectedEquityChangeRate = 4.00M, expectedDebtChangeRate = 10.00M, expectedGoldChangeRate = 2.00M;
            string expectedCommand = $"CHANGE {expectedEquityChangeRate}% {expectedDebtChangeRate}% {expectedGoldChangeRate}% JANUARY";
            var sut = new ChangeCommand();

            //when
            sut.Transact(expectedCommand);

            //then
            Assert.Single(TestData.Portfolio.ChangeRates);
            var actualEquity = TestData.Portfolio.ChangeRates.FirstOrDefault(p => p.EquityRate == expectedEquityChangeRate);
            var actualDebt = TestData.Portfolio.ChangeRates.FirstOrDefault(p => p.DebtRate == expectedDebtChangeRate);
            var actualGold = TestData.Portfolio.ChangeRates.FirstOrDefault(p => p.GoldRate == expectedGoldChangeRate);
            Assert.Equal(expectedEquityChangeRate, actualEquity.EquityRate);
            Assert.Equal(expectedDebtChangeRate, actualDebt.DebtRate);
            Assert.Equal(expectedGoldChangeRate, actualGold.GoldRate);
        }
    }
}