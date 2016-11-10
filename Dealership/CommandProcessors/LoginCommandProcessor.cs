using Dealership.Contracts;
using Dealership.Engine;

namespace Dealership.CommandProcessors
{
    public class LoginCommandProcessor : CommandProcessor, ICommandProcessor
    {
        private const string UserLoggedInAlready = "User {0} is logged in! Please log out first!";
        private const string UserLoggedIn = "User {0} successfully logged in!";
        private const string WrongUsernameOrPassword = "Wrong username or password!";

        private const string LoginCommandName = "Login";

        private readonly IUserService userService;

        public LoginCommandProcessor(IUserService userService)
            : base()
        {
            this.userService = userService;
        }

        protected override bool CanHandle(ICommand command)
        {
            var canHandle = command.Name == LoginCommandName;

            return canHandle;
        }

        protected override string Handle(ICommand command)
        {
            var username = command.Parameters[0];
            var password = command.Parameters[1];

            return this.Login(username, password);
        }

        private string Login(string username, string password)
        {
            var loggedUser = this.userService.LoggedUser;
            if (loggedUser != null)
            {
                return string.Format(UserLoggedInAlready, loggedUser.Username);
            }

            var userFound = this.userService.GetUser(username);

            if (userFound != null && userFound.Password == password)
            {
                this.userService.LogIn(userFound);
                return string.Format(UserLoggedIn, username);
            }

            return WrongUsernameOrPassword;
        }
    }
}
