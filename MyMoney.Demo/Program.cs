using System;
using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;

using MyMoney.Demo.Infrastructure;

namespace MyMoney.Demo
{
    [ExcludeFromCodeCoverage]
    internal class Program
    {
        private static async Task Main(string[] args)
        {
            Console.WriteLine();
            string input = string.Empty;
            while ((args == null || args.Length == 0) && string.IsNullOrWhiteSpace(input) || !Constants.FileNumbers.Contains(input))
            {
                Console.WriteLine(Constants.InputText + Environment.NewLine);
                Console.WriteLine(Constants.INPUT1);
                Console.WriteLine(Constants.INPUT2 + Environment.NewLine);
                input = Console.ReadLine();

                if (string.IsNullOrWhiteSpace(input) || !Constants.FileNumbers.Contains(input)) continue;

                Console.BackgroundColor = ConsoleColor.White;
                Console.ForegroundColor = ConsoleColor.Black;

                await Startup.Init.Execute(Constants.OptionLookup[input]);
                Console.ResetColor();
            }
        }
    }
}