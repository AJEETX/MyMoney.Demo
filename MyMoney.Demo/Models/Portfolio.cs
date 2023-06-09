using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace MyMoney.Demo.Models
{
    [ExcludeFromCodeCoverage]
    internal class Portfolio
    {
        public Fund Allocate { get; set; }
        public Fund SIP { get; set; }
        public List<ChangeRate> ChangeRates { get; set; } = new();
    }
}