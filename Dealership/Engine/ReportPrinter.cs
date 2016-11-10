using System.Collections.Generic;
using Dealership.Contracts;
using System.Text;

namespace Dealership.Engine
{
    public class ReportPrinter : IReportPrinter
    {
        private readonly IOutputWriter outputWriter;

        public ReportPrinter(IOutputWriter outputWriter)
        {
            this.outputWriter = outputWriter;
        }

        public void PrintReports(IEnumerable<string> reports)
        {
            var output = new StringBuilder();

            foreach (var report in reports)
            {
                output.AppendLine(report);
                output.AppendLine(new string('#', 20));
            }

            this.outputWriter.Write(output.ToString());
        }
    }
}
