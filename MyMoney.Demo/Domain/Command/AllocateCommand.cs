using System.Text.RegularExpressions;

using MyMoney.Demo.Infrastructure;
using MyMoney.Demo.Models;

namespace MyMoney.Demo.Domain
{
    internal partial class AllocateCommand : Command, ICommand
    {
        private readonly Regex allocateInputRegex = AllocateRegex();

        public void Transact(string input)
        {
            if (string.IsNullOrEmpty(input) || !allocateInputRegex.IsMatch(input)) return;

            var allocateMatch = allocateInputRegex.Match(input);

            if (allocateMatch is null) return;

            if (allocateMatch.Success)
            {
                var equityAmount = int.Parse(allocateMatch.Groups[Constants.EQUITY].Value);
                var debtAmount = int.Parse(allocateMatch.Groups[Constants.DEBT].Value);
                var goldAmount = int.Parse(allocateMatch.Groups[Constants.GOLD].Value);
                portfolio = new Portfolio
                {
                    Allocate = new Fund
                    {
                        Debt = debtAmount,
                        Equity = equityAmount,
                        Gold = goldAmount,
                        Month = Month.JANUARY
                    },
                };
            }
        }

        [GeneratedRegex("(?<type>ALLOCATE)\\s+(?<equity>[0-9]{1,})\\s(?<debt>[0-9]{1,})\\s(?<gold>[0-9]{1,})\\s*")]
        private static partial Regex AllocateRegex();
    }
}