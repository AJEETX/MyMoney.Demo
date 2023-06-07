using System;
using System.Collections.Generic;

using MyMoney.Demo.Models;

namespace MyMoney.Demo.Domain
{
    public interface ICommandManager
    {
        void Process(string input);
    }

    internal class CommandManager : ICommandManager
    {
        private const int COMMAND_TYPE = 0;
        private const string separator = " ";
        private static readonly Dictionary<CommandType, ICommand> commandLookup = new();

        public CommandManager()
        {
            commandLookup.Add(CommandType.ALLOCATE, new AllocateCommand());
            commandLookup.Add(CommandType.SIP, new SIPCommand());
            commandLookup.Add(CommandType.CHANGE, new ChangeCommand());
            commandLookup.Add(CommandType.BALANCE, new BalanceCommand());
            commandLookup.Add(CommandType.REBALANCE, new ReBalanceCommand());
        }

        public void Process(string input)
        {
            if (string.IsNullOrWhiteSpace(input)) return;

            var commandTypeKey = input.Split(separator)[COMMAND_TYPE];

            var commandType = Enum.Parse<CommandType>(commandTypeKey);

            var command = commandLookup[commandType];

            if (command == default) return;

            command.Transact(input);
        }
    }
}