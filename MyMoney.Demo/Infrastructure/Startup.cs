using System.Diagnostics.CodeAnalysis;

using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

using MyMoney.Demo.Domain;

namespace MyMoney.Demo.Infrastructure
{
    [ExcludeFromCodeCoverage]
    internal static class Startup
    {
        public static IPortfolioService Init
        {
            get
            {
                var services = new ServiceCollection();
                services.AddLogging(configure => configure.AddConsole())
                    .AddSingleton<IPortfolioService, PortfolioService>()
                    .AddSingleton<ICommandManager, CommandManager>();
                var serviceProvider = services.BuildServiceProvider();
                return serviceProvider.GetService<IPortfolioService>();
            }
        }
    }
}