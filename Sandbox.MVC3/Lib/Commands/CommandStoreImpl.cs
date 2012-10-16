using System.Collections.Generic;
using System.Linq;

namespace Sandbox.MVC.Lib.Commands
{
    public class CommandStoreImpl : ICommandStore
    {
        private readonly IEnumerable<ICommand> commands;

        public CommandStoreImpl(IEnumerable<ICommand> commands)
        {
            this.commands = commands ?? new List<ICommand>();
        }

        public TCommand GetCommand<TCommand>() where TCommand : ICommand
        {
            var command = commands.First(c => c is TCommand);
            return (TCommand)command;
        }
    }
}