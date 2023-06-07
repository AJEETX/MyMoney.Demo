using System;
using System.IO;
using System.Threading.Tasks;

namespace MyMoney.Demo.Domain
{
    public interface IPortfolioService
    {
        Task Execute(string fileInput);
    }

    internal class PortfolioService : IPortfolioService
    {
        private readonly ICommandManager _commandManager;

        public PortfolioService(ICommandManager commandManager)
        {
            _commandManager = commandManager;
        }

        public async Task Execute(string fileInput)
        {
            if (string.IsNullOrWhiteSpace(fileInput)) return;

            try
            {
                var inputs = await File.ReadAllLinesAsync(fileInput);

                foreach (string input in inputs)
                {
                    _commandManager.Process(input);
                }
            }
            catch (Exception ex)
            {
                //CATCH //STREAM //LOG //
                Console.WriteLine($"{ex.Message}");
                return;
            }
        }
    }
}