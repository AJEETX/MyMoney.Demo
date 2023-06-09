using MyMoney.Demo.Models;

namespace MyMoney.Demo.Test.SampleData
{
    internal class TestData : Demo.Domain.Command
    {
        public static Portfolio Portfolio => portfolio;
    }
}