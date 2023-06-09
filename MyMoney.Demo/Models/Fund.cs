using System.Diagnostics.CodeAnalysis;

namespace MyMoney.Demo.Models
{
    [ExcludeFromCodeCoverage]
    internal class Fund
    {
        public int Equity { get; set; }
        public int Debt { get; set; }
        public int Gold { get; set; }
        public Month Month { get; set; }
    }
}