using Dealership.Contracts;
using System.Text;
using Dealership.Engine;
using Dealership.Common.Enums;

namespace Dealership.CommandProcessors
{
    public class ShowUsersCommandProcessor : CommandProcessor, ICommandProcessor
    {
        private const string YouAreNotAnAdmin = "You are not an admin!";

        private const string ShowUsersCommandName = "ShowUsers";

        private readonly IUserService userService;

        public ShowUsersCommandProcessor(IUserService userService)
        {
            this.userService = userService;
        }

        protected override bool CanHandle(ICommand command)
        {
            var canHandle = command.Name == ShowUsersCommandName;

            return canHandle;
        }

        protected override string Handle(ICommand command)
        {
            return this.ShowAllUsers();
        }

        private string ShowAllUsers()
        {
            if (this.userService.LoggedUser.Role != Role.Admin)
            {
                return YouAreNotAnAdmin;
            }

            var builder = new StringBuilder();
            builder.AppendLine("--USERS--");
            var counter = 1;
            foreach (var user in this.userService.GetAllUsers())
            {
                builder.AppendLine(string.Format("{0}. {1}", counter, user.ToString()));
                counter++;
            }

            return builder.ToString().Trim();
        }
    }
}
