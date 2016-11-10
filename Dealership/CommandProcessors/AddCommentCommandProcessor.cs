using Dealership.Contracts;
using Dealership.Engine;
using Dealership.Common;

namespace Dealership.CommandProcessors
{
    public class AddCommentCommandProcessor : CommandProcessor, ICommandProcessor
    {
        private const string CommentAddedSuccessfully = "{0} added comment successfully!";
        private const string NoSuchUser = "There is no user with username {0}!";
        private const string VehicleDoesNotExist = "The vehicle does not exist!";

        private const string AddCommentCommandName = "AddComment";

        private readonly ICommentFactory commentFactory;
        private readonly IUserService userService;

        public AddCommentCommandProcessor(IUserService userService, ICommentFactory commentFactory)
        {
            this.userService = userService;
            this.commentFactory = commentFactory;
        }

        protected override bool CanHandle(ICommand command)
        {
            var canHandle = command.Name == AddCommentCommandName;

            return canHandle;
        }

        protected override string Handle(ICommand command)
        {
            var content = command.Parameters[0];
            var author = command.Parameters[1];
            var vehicleIndex = int.Parse(command.Parameters[2]) - 1;

            return this.AddComment(content, vehicleIndex, author);
        }

        private string AddComment(string content, int vehicleIndex, string author)
        {
            var comment = this.commentFactory.CreateComment(content);
            var loggedUser = this.userService.LoggedUser;
            var username = loggedUser.Username;
            
            comment.Author = username;
            var user = this.userService.GetUser(author);

            if (user == null)
            {
                return string.Format(NoSuchUser, author);
            }

            Validator.ValidateIntRangeInclusive(vehicleIndex, 0, user.Vehicles.Count, VehicleDoesNotExist);

            var vehicle = user.Vehicles[vehicleIndex];

            loggedUser.AddComment(comment, vehicle);

            return string.Format(CommentAddedSuccessfully, username);
        }
    }
}
