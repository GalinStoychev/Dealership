using Dealership.Engine;
using System.Collections.Generic;

namespace Dealership.Contracts
{
    public interface ICommandReader
    {
        IEnumerable<ICommand> ReadCommands();
    }
}
