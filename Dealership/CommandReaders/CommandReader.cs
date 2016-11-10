using Dealership.Contracts;
using System.Collections.Generic;
using Dealership.Engine;

namespace Dealership.CommandReaders
{
    public class CommandReader : ICommandReader
    {
        private readonly ICommandFactory commandFactory;
        private readonly IInputReader inputReader;

        public CommandReader(IInputReader inputReader, ICommandFactory commandFactory)
        {
            this.inputReader = inputReader;
            this.commandFactory = commandFactory;
        }

        public IEnumerable<ICommand> ReadCommands()
        {
            var commands = new List<ICommand>();

            var currentLine = this.inputReader.ReadLine();

            while (!string.IsNullOrEmpty(currentLine))
            {
                var currentCommand = this.commandFactory.CreateCommand(currentLine);
                commands.Add(currentCommand);

                currentLine = this.inputReader.ReadLine();
            }

            return commands;
        }
    }
}
