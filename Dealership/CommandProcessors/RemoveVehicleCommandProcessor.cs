using Dealership.Contracts;
using Dealership.Engine;
using Dealership.Common;

namespace Dealership.CommandProcessors
{
    public class RemoveVehicleCommandProcessor : CommandProcessor, ICommandProcessor
    {
        private const string VehicleRemovedSuccessfully = "{0} removed vehicle successfully!";
        private const string RemovedVehicleDoesNotExist = "Cannot remove comment! The vehicle does not exist!";

        private const string RemoveVehicleCommandName = "RemoveVehicle";

        private readonly IUserService userService;

        public RemoveVehicleCommandProcessor(IUserService userService)
        {
            this.userService = userService;
        }

        protected override bool CanHandle(ICommand command)
        {
            var canHandle = command.Name == RemoveVehicleCommandName;

            return canHandle;
        }

        protected override string Handle(ICommand command)
        {
            var vehicleIndex = int.Parse(command.Parameters[0]) - 1;

            return this.RemoveVehicle(vehicleIndex);
        }

        private string RemoveVehicle(int vehicleIndex)
        {
            var loggedUser = this.userService.LoggedUser;
            var vehicles = loggedUser.Vehicles;
            Validator.ValidateIntRangeInclusive(vehicleIndex, 0, vehicles.Count, RemovedVehicleDoesNotExist);

            var vehicle = vehicles[vehicleIndex];

            loggedUser.RemoveVehicle(vehicle);

            return string.Format(VehicleRemovedSuccessfully, loggedUser.Username);
        }
    }
}
