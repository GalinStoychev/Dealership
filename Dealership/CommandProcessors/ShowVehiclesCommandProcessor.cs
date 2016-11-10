using Dealership.Contracts;
using Dealership.Engine;

namespace Dealership.CommandProcessors
{
    public class ShowVehiclesCommandProcessor : CommandProcessor, ICommandProcessor
    {
        private const string NoSuchUser = "There is no user with username {0}!";

        private const string ShowVehiclesCommandName = "ShowVehicles";

        private readonly IUserService userService;

        public ShowVehiclesCommandProcessor(IUserService userService)
        {
            this.userService = userService;
        }

        protected override bool CanHandle(ICommand command)
        {
            var canHandle = command.Name == ShowVehiclesCommandName;

            return canHandle;
        }

        protected override string Handle(ICommand command)
        {
            var username = command.Parameters[0];

            return this.ShowUserVehicles(username);
        }

        private string ShowUserVehicles(string username)
        {
            var user = this.userService.GetUser(username);

            if (user == null)
            {
                return string.Format(NoSuchUser, username);
            }

            return user.PrintVehicles();
        }
    }
}
