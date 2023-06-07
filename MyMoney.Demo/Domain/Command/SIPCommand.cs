using System.Text.RegularExpressions;

using MyMoney.Demo.Infrastructure;
using MyMoney.Demo.Models;

namespace MyMoney.Demo.Domain
{
    internal partial class SIPCommand : Command, ICommand
    {
        private readonly Regex sipRegex = SIPRegex();

        public void Transact(string input)
        {
            if (portfolio == null || string.IsNullOrEmpty(input) || !sipRegex.IsMatch(input)) return;

            var sipMatch = sipRegex.Match(input);

            if (sipMatch is null) return;

            if (sipMatch.Success)
            {
                var equityAmount = int.Parse(sipMatch.Groups[Constants.EQUITY].Value);
                var debtAmount = int.Parse(sipMatch.Groups[Constants.DEBT].Value);
                var goldAmount = int.Parse(sipMatch.Groups[Constants.GOLD].Value);
                var sipFund = new Fund
                {
                    Debt = debtAmount,
                    Gold = goldAmount,
                    Equity = equityAmount,
                    Month = Month.FEBRUARY,
                };

                portfolio.SIP = sipFund;
            }
        }

        [GeneratedRegex("(?<type>SIP)\\s+(?<equity>[0-9]{1,})\\s(?<debt>[0-9]{1,})\\s(?<gold>[0-9]{1,})\\s*")]
        private static partial Regex SIPRegex();
    }
}