using System.Diagnostics.CodeAnalysis;

namespace MyMoney.Demo.Models
{
    [ExcludeFromCodeCoverage]
    internal class ChangeRate
    {
        public decimal EquityRate { get; set; }
        public decimal DebtRate { get; set; }
        public decimal GoldRate { get; set; }
        public Month Month { get; set; }
    }
}