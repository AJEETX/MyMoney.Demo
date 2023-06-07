using System.Collections.Generic;

namespace MyMoney.Demo.Infrastructure
{
    internal class Constants
    {
        public const string INPUT1_FILE = "input1.txt";
        public const string INPUT2_FILE = "input2.txt";
        public static (string select, string file) OPTION1 = ("1", INPUT1_FILE);
        public static (string select, string file) OPTION2 = ("2", INPUT2_FILE);

        public static Dictionary<string, string> OptionLookup = new Dictionary<string, string> { { OPTION1.select, OPTION1.file }, { OPTION2.select, OPTION2.file } };

        public const string INPUT1 = $"To process `{INPUT1_FILE}`, Type 1 and ENTER";
        public const string INPUT2 = $"To process `{INPUT2_FILE}`, Type 2 and ENTER";

        public const string MONTH = "month", EQUITY = "equity", DEBT = "debt", GOLD = "gold", InputText = "Pls select the correct input file option: ";
    }
}