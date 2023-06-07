using System;
using System.Text.RegularExpressions;

using MyMoney.Demo.Models;

namespace MyMoney.Demo.Domain
{
    internal partial class ReBalanceCommand : Command, ICommand
    {
        private readonly Regex reBalanceRegex = RebalanceRegex();

        public void Transact(string input)
        {
            if (portfolio == null || string.IsNullOrEmpty(input) || !reBalanceRegex.IsMatch(input)) return;

            var reBalanceMatch = reBalanceRegex.Match(input);
            if (reBalanceMatch.Success)
            {
                var equityAllocationAmount = portfolio.Allocate.Equity;
                var debtAllocationAmount = portfolio.Allocate.Debt;
                var goldAllocationAmount = portfolio.Allocate.Gold;

                var equityAllocationPercent = (decimal)equityAllocationAmount / (equityAllocationAmount + debtAllocationAmount + goldAllocationAmount) * 100;
                var debtAllocationPercent = (decimal)debtAllocationAmount / (equityAllocationAmount + debtAllocationAmount + goldAllocationAmount) * 100;
                var goldAllocationPercent = (decimal)goldAllocationAmount / (equityAllocationAmount + debtAllocationAmount + goldAllocationAmount) * 100;

                var (equityTotal, debtTotal, goldTotal) = GetFundTotalTillBalanceMonth(Month.JUNE);

                var equityRebalanceAmount = (int)Math.Floor((equityAllocationPercent / 100) * (equityTotal + debtTotal + goldTotal));

                var debtRebalanceAmount = (int)Math.Floor((debtAllocationPercent / 100) * (equityTotal + debtTotal + goldTotal));

                var goldRebalanceAmount = (int)Math.Floor((goldAllocationPercent / 100) * (equityTotal + debtTotal + goldTotal));

                if (equityRebalanceAmount == 0 && debtRebalanceAmount == 0 && goldRebalanceAmount == 0)
                {
                    Console.WriteLine($"CANNOT_REBALANCE");
                    return;
                }
                Console.WriteLine($"{(equityRebalanceAmount)} {(debtRebalanceAmount)} {(goldRebalanceAmount)}");
            }
        }

        [GeneratedRegex("(?<type>REBALANCE)\\s*")]
        private static partial Regex RebalanceRegex();
    }
}