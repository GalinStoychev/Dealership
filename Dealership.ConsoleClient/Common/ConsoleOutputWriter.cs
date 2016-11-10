using Dealership.Contracts;
using System;

namespace Dealership.ConsoleClient.Common
{
    public class ConsoleOutputWriter : IOutputWriter
    {
        public void Write(string text)
        {
            Console.Write(text);
        }
    }
}
