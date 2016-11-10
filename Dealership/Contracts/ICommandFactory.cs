using Dealership.Engine;

namespace Dealership.Contracts
{
    public interface ICommandFactory
    {
        ICommand CreateCommand(string input);
    }
}
