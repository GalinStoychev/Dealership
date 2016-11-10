using Dealership.Contracts;
using System;

namespace Dealership.ConsoleClient.Common
{
    public class ConsoleInputReader : IInputReader
    {
        public string ReadLine()
        {
            var result = Console.ReadLine();

            return result;
        }
    }
}
