using System;
using System.Collections.Generic;
using System.Linq;

using MyMoney.Demo.Models;

namespace MyMoney.Demo.Domain
{
    public interface ICommand
    {
        void Transact(string input);
    }

    internal class Command
    {
        private static readonly List<Month> _months = Enum.GetValues(typeof(Month)).Cast<Month>().OrderBy(o => ((int)o)).ToList();
        protected static Portfolio portfolio;

        protected static (int equityTotal, int debtTotal, int goldTotal) GetFundTotalTillBalanceMonth(Month balanceMonth)
        {
            int equityRounded = 0, debtRounded = 0, goldRounded = 0;

            var janChangeRate = portfolio.ChangeRates.Where(c => c.Month == Month.JANUARY).FirstOrDefault();

            if (janChangeRate == null)  return (equityRounded, debtRounded, goldRounded);

            equityRounded = portfolio.Allocate.Equity + (int)Math.Floor(portfolio.Allocate.Equity * (janChangeRate.EquityRate / 100));

            goldRounded = portfolio.Allocate.Gold + (int)Math.Floor(portfolio.Allocate.Gold * (janChangeRate.GoldRate / 100));

            debtRounded = portfolio.Allocate.Debt + (int)Math.Floor(portfolio.Allocate.Debt * (janChangeRate.DebtRate / 100));

            foreach (var month in _months.Where(m => m <= balanceMonth).Skip(1))
            {
                var changeRate = portfolio.ChangeRates.Where(c => c.Month == month).FirstOrDefault();

                if (changeRate == null) return (0, 0, 0);

                equityRounded = portfolio.SIP.Equity + equityRounded;

                equityRounded += (int)Math.Floor(equityRounded * (changeRate.EquityRate / 100));

                goldRounded = portfolio.SIP.Gold + goldRounded;

                goldRounded += (int)Math.Floor(goldRounded * (changeRate.GoldRate / 100));

                debtRounded = portfolio.SIP.Debt + debtRounded;

                debtRounded += (int)Math.Floor(debtRounded * (changeRate.DebtRate / 100));
            }
            return (equityRounded, debtRounded, goldRounded);
        }
    }
}