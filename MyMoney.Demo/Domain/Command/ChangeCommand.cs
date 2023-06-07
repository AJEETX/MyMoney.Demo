using System;
using System.Text.RegularExpressions;

using MyMoney.Demo.Infrastructure;
using MyMoney.Demo.Models;

namespace MyMoney.Demo.Domain
{
    internal partial class ChangeCommand : Command, ICommand
    {
        private readonly Regex changeRegex = ChangeRegex();

        public void Transact(string input)
        {
            if (portfolio == null || string.IsNullOrEmpty(input) || !changeRegex.IsMatch(input)) return;

            var changeMatch = changeRegex.Match(input);

            if (changeMatch is null) return;

            if (changeMatch.Success)
            {
                var equityRate = decimal.Parse(changeMatch.Groups[Constants.EQUITY].Value);
                var debtRate = decimal.Parse(changeMatch.Groups[Constants.DEBT].Value);
                var goldRate = decimal.Parse(changeMatch.Groups[Constants.GOLD].Value);
                var month = Enum.Parse<Month>(changeMatch.Groups[Constants.MONTH].Value);
                portfolio.ChangeRates.Add(new ChangeRate
                {
                    DebtRate = debtRate,
                    EquityRate = equityRate,
                    GoldRate = goldRate,
                    Month = month,
                });
            }
        }

        [GeneratedRegex("(?<type>CHANGE)\\s+(?<equity>[-]{0,1}[0-9]{1,}[.][0-9]{1,})[%]\\s+(?<debt>[-]{0,1}[0-9]{1,}[.][0-9]{1,})[%]\\s+(?<gold>[-]{0,1}[0-9]{1,}[.][0-9]{1,})[%]\\s+(?<month>[A-Z]{1,})\\s*")]
        private static partial Regex ChangeRegex();
    }
}