using Dealership.ConsoleClient.Configuration;
using Dealership.Engine;
using Ninject;

namespace Dealership.ConsoleClient
{
    public class Startup
    {
        public static void Main()
        {
            var kernel = new StandardKernel(new DealershipModule());
            var engine = kernel.Get<IEngine>();

            engine.Start();
        }
    }
}
