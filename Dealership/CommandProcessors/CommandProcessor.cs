using System;
using Dealership.Contracts;
using Dealership.Engine;

namespace Dealership.CommandProcessors
{
    public abstract class CommandProcessor : ICommandProcessor
    {
        private const string InvalidCommand = "Invalid command!";

        public CommandProcessor()
        {
            this.Successor = null;
        }

        public ICommandProcessor Successor { get; set; }

        public string Process(ICommand command)
        {
            var canHandle = this.CanHandle(command);
            if (canHandle)
            {
                return this.Handle(command);
            }
            else if (this.Successor != null)
            {
                return this.Successor.Process(command);
            }
            else
            {
                return string.Format(InvalidCommand, command.Name);
            }
        }

        protected abstract bool CanHandle(ICommand command);

        protected abstract string Handle(ICommand command);
    }
}
