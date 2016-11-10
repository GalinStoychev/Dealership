using Dealership.Engine;

namespace Dealership.Contracts
{
    public interface ICommandProcessor
    {
        string Process(ICommand command);

        ICommandProcessor Successor { get; set; }
    }
}
