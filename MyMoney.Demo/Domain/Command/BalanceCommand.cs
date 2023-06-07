using System;
using System.Text.RegularExpressions;

using MyMoney.Demo.Infrastructure;
using MyMoney.Demo.Models;

namespace MyMoney.Demo.Domain
{
    internal partial class BalanceCommand : Command, ICommand
    {
        private readonly Regex balanceRegex = BalanceRegex();

        public void Transact(string input)
        {
            if (portfolio == null || string.IsNullOrEmpty(input) || !balanceRegex.IsMatch(input)) return;

            var balanceMatch = balanceRegex.Match(input);

            if (balanceMatch is null) return;

            if (balanceMatch.Success)
            {
                var balanceMonth = Enum.Parse<Month>(balanceMatch.Groups[Constants.MONTH].Value);

                var (equityTotal, debtTotal, goldTotal) = GetFundTotalTillBalanceMonth(balanceMonth);

                Console.WriteLine($"{equityTotal}  {debtTotal} {goldTotal}");
            }
        }

        [GeneratedRegex("(?<type>BALANCE)\\s+(?<month>[A-Z]{1,})\\s*")]
        private static partial Regex BalanceRegex();
    }
}