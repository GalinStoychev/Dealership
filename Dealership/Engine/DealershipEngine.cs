using Dealership.Contracts;
using System;
using System.Collections.Generic;

namespace Dealership.Engine
{
    public sealed class DealershipEngine : IEngine
    {
        private readonly ICommandProcessor commandProcessor;
        private readonly ICommandReader commandReader;
        private readonly IReportPrinter reportPrinter;
        private readonly IUserService userService;

        public DealershipEngine(
            ICommandReader commandReader, 
            ICommandProcessor commandProcessor, 
            IReportPrinter reportPrinter, 
            IUserService userService)
        {
            this.commandReader = commandReader;
            this.commandProcessor = commandProcessor;
            this.reportPrinter = reportPrinter;
            this.userService = userService;
        }

        public void Start()
        {
            var commands = this.commandReader.ReadCommands();
            var commandResult = this.ProcessCommands(commands);
            this.reportPrinter.PrintReports(commandResult);
        }

        public void Reset()
        {
            this.userService.DeleteAllUsers();
            var commandResult = new List<string>();
            this.reportPrinter.PrintReports(commandResult);
        }

        private IEnumerable<string> ProcessCommands(IEnumerable<ICommand> commands)
        {
            var reports = new List<string>();

            foreach (var command in commands)
            {
                try
                {
                    var report = this.commandProcessor.Process(command);
                    reports.Add(report);
                }
                catch (Exception ex)
                {
                    reports.Add(ex.Message);
                }
            }

            return reports;
        }
    }
}
