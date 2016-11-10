using Dealership.Contracts;
using System;
using Dealership.Engine;
using Dealership.Common.Enums;

namespace Dealership.CommandProcessors
{
    public class RegisterCommandProcessor : CommandProcessor, ICommandProcessor
    {
        private const string UserAlreadyExist = "User {0} already exist. Choose a different username!";
        private const string UserLoggedInAlready = "User {0} is logged in! Please log out first!";
        private const string UserRegisterеd = "User {0} registered successfully!";

        private const string RegisterUserCommandName = "RegisterUser";

        private readonly IUserFactory userFactory;
        private readonly IUserService userService;

        public RegisterCommandProcessor(IUserService userService, IUserFactory userFactory)
            : base()
        {
            this.userService = userService;
            this.userFactory = userFactory;
        }

        protected override bool CanHandle(ICommand command)
        {
            var canHandle = command.Name == RegisterUserCommandName;

            return canHandle;
        }

        protected override string Handle(ICommand command)
        {
            var username = command.Parameters[0];
            var firstName = command.Parameters[1];
            var lastName = command.Parameters[2];
            var password = command.Parameters[3];

            var role = Role.Normal;

            if (command.Parameters.Count > 4)
            {
                role = (Role)Enum.Parse(typeof(Role), command.Parameters[4]);
            }

            return this.RegisterUser(username, firstName, lastName, password, role);
        }

        private string RegisterUser(string username, string firstName, string lastName, string password, Role role)
        {
            var loggedUser = this.userService.LoggedUser;
            if (loggedUser != null)
            {
                return string.Format(UserLoggedInAlready, loggedUser.Username);
            }

            if (this.userService.ContainsUser(username))
            {
                return string.Format(UserAlreadyExist, username);
            }

            var user = this.userFactory.CreateUser(username, firstName, lastName, password, role);
            this.userService.Register(user);
            this.userService.LogIn(user);

            return string.Format(UserRegisterеd, username);
        }
    }
}
