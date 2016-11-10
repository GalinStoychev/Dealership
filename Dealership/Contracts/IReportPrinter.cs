using System.Collections.Generic;

namespace Dealership.Contracts
{
    public interface IReportPrinter
    {
        void PrintReports(IEnumerable<string> reports);
    }
}
