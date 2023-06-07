using System.Collections.Generic;

namespace MyMoney.Demo.Models
{
    internal class Portfolio
    {
        public Fund Allocate { get; set; }
        public Fund SIP { get; set; }
        public List<ChangeRate> ChangeRates { get; set; } = new();
    }
}