using Dealership.Contracts;
using System;
using Dealership.Engine;
using Dealership.Common.Enums;

namespace Dealership.CommandProcessors
{
    public class AddVehicleCommandProcessor : CommandProcessor, ICommandProcessor
    {
        private const string VehicleAddedSuccessfully = "{0} added vehicle successfully!";

        private const string AddVehicleCommandName = "AddVehicle";

        private readonly IUserService userService;
        private readonly IVehicleFactory vehicleFactory;

        public AddVehicleCommandProcessor(IVehicleFactory vehicleFactory, IUserService userService)
        {
            this.vehicleFactory = vehicleFactory;
            this.userService = userService;
        }

        protected override bool CanHandle(ICommand command)
        {
            var canHandle = command.Name == AddVehicleCommandName;

            return canHandle;
        }

        protected override string Handle(ICommand command)
        {
            var type = command.Parameters[0];
            var make = command.Parameters[1];
            var model = command.Parameters[2];
            var price = decimal.Parse(command.Parameters[3]);
            var additionalParam = command.Parameters[4];

            var typeEnum = (VehicleType)Enum.Parse(typeof(VehicleType), type, true);

            return this.AddVehicle(typeEnum, make, model, price, additionalParam);
        }

        private string AddVehicle(VehicleType type, string make, string model, decimal price, string additionalParam)
        {
            IVehicle vehicle = null;

            if (type == VehicleType.Car)
            {
                vehicle = this.vehicleFactory.GetCar(make, model, price, int.Parse(additionalParam));
            }
            else if (type == VehicleType.Motorcycle)
            {
                vehicle = this.vehicleFactory.GetMotorcycle(make, model, price, additionalParam);
            }
            else if (type == VehicleType.Truck)
            {
                vehicle = this.vehicleFactory.GetTruck(make, model, price, int.Parse(additionalParam));
            }

            var loggedUser = this.userService.LoggedUser;
            loggedUser.AddVehicle(vehicle);

            return string.Format(VehicleAddedSuccessfully, loggedUser.Username);
        }
    }
}
