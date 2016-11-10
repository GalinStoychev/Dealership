using Dealership.Contracts;
using Dealership.Engine;
using Dealership.Common;

namespace Dealership.CommandProcessors
{
    public class RemoveCommentCommandProcessor : CommandProcessor, ICommandProcessor
    {
        private const string NoSuchUser = "There is no user with username {0}!";
        private const string RemovedVehicleDoesNotExist = "Cannot remove comment! The vehicle does not exist!";
        private const string RemovedCommentDoesNotExist = "Cannot remove comment! The comment does not exist!";
        private const string CommentRemovedSuccessfully = "{0} removed comment successfully!";

        private const string RemoveCommentCommandName = "RemoveComment";

        private readonly IUserService userService;

        public RemoveCommentCommandProcessor(IUserService userService)
        {
            this.userService = userService;
        }

        protected override bool CanHandle(ICommand command)
        {
            var canHandle = command.Name == RemoveCommentCommandName;

            return canHandle;
        }

        protected override string Handle(ICommand command)
        {
            var vehicleIndex = int.Parse(command.Parameters[0]) - 1;
            var commentIndex = int.Parse(command.Parameters[1]) - 1;
            var username = command.Parameters[2];

            return this.RemoveComment(vehicleIndex, commentIndex, username);
        }

        private string RemoveComment(int vehicleIndex, int commentIndex, string username)
        {
            var user = this.userService.GetUser(username);

            if (user == null)
            {
                return string.Format(NoSuchUser, username);
            }

            Validator.ValidateIntRangeInclusive(vehicleIndex, 0, user.Vehicles.Count, RemovedVehicleDoesNotExist);
            Validator.ValidateIntRangeInclusive(commentIndex, 0, user.Vehicles[vehicleIndex].Comments.Count, RemovedCommentDoesNotExist);

            var vehicle = user.Vehicles[vehicleIndex];
            var comment = user.Vehicles[vehicleIndex].Comments[commentIndex];

            var loggedUser = this.userService.LoggedUser;
            loggedUser.RemoveComment(comment, vehicle);

            return string.Format(CommentRemovedSuccessfully, loggedUser.Username);
        }
    }
}
