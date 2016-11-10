using Dealership.Contracts;
using Dealership.Engine;

namespace Dealership.CommandProcessors
{
    public class LogoutCommandProcessor : CommandProcessor, ICommandProcessor
    {
        private const string UserLoggedOut = "You logged out!";

        private const string LogoutCommandName = "Logout";

        private readonly IUserService userService;

        public LogoutCommandProcessor(IUserService userService)
        {
            this.userService = userService;
        }

        protected override bool CanHandle(ICommand command)
        {
            var canHandle = command.Name == LogoutCommandName;

            return canHandle;
        }

        protected override string Handle(ICommand command)
        {
            this.userService.LogOut();

            return UserLoggedOut;
        }
    }
}
