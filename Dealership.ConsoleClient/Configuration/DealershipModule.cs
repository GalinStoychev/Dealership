using Dealership.CommandProcessors;
using Dealership.ConsoleClient.Common;
using Dealership.ConsoleClient.Interceptors;
using Dealership.Contracts;
using Dealership.Engine;
using Dealership.Models;
using Ninject;
using Ninject.Extensions.Conventions;
using Ninject.Extensions.Factory;
using Ninject.Extensions.Interception.Infrastructure.Language;
using Ninject.Modules;
using Ninject.Parameters;
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;

namespace Dealership.ConsoleClient.Configuration
{
    public class DealershipModule : NinjectModule
    {
        private const string AddCommentCommandProcessorName = "AddCommentCommandProcessor";
        private const string AddVehicleCommandProcessorName = "AddVehicleCommandProcessor";
        private const string LoginCommandProcessorName = "LoginCommandProcessor";
        private const string LogoutCommandProcessorName = "LogoutCommandProcessor";
        private const string RegisterCommandProcessorName = "RegisterCommandProcessor";
        private const string RemoveCommentCommandProcessorName = "RemoveCommentCommandProcessor";
        private const string RemoveVehicleCommandProcessorName = "RemoveVehicleCommandProcessor";
        private const string ShowUsersCommandProcessorName = "ShowUsersCommandProcessor";
        private const string ShowVehiclesCommandProcessorName = "ShowVehiclesCommandProcessorName";
        private const string CarName = "Car";
        private const string MotorcycleName = "Motorcycle";
        private const string TruckName = "Truck";

        public override void Load()
        {
            var typesToExclude = new List<Type>() { typeof(UserService) };
            Kernel.Bind(x =>
            {
                x.FromAssembliesInPath(Path.GetDirectoryName(Assembly.GetAssembly(typeof(IEngine)).Location))
                .SelectAllClasses()
                .Excluding(typesToExclude)
                .BindDefaultInterface();
            });

            Bind<IEngine>().To<DealershipEngine>().InSingletonScope();

            Bind<IUserService>().To<UserService>().InSingletonScope();

            Bind<IInputReader>().To<ConsoleInputReader>();
            Bind<IOutputWriter>().To<ConsoleOutputWriter>();

            Bind<IUserFactory>().ToFactory().InSingletonScope();
            Bind<IVehicleFactory>().ToFactory().InSingletonScope();
            Bind<ICommentFactory>().ToFactory().InSingletonScope();
            Bind<ICommandFactory>().ToFactory().InSingletonScope();

            Bind<IVehicle>().To<Car>().Named(CarName);
            Bind<IVehicle>().To<Motorcycle>().Named(MotorcycleName);
            Bind<IVehicle>().To<Truck>().Named(TruckName);

            Bind<ICommandProcessor>().To<AddCommentCommandProcessor>().Named(AddCommentCommandProcessorName).Intercept().With<UserAuthorizationInterceptor>();
            Bind<ICommandProcessor>().To<AddVehicleCommandProcessor>().Named(AddVehicleCommandProcessorName).Intercept().With<UserAuthorizationInterceptor>();
            Bind<ICommandProcessor>().To<LoginCommandProcessor>().Named(LoginCommandProcessorName);
            Bind<ICommandProcessor>().To<LogoutCommandProcessor>().Named(LogoutCommandProcessorName).Intercept().With<UserAuthorizationInterceptor>();
            Bind<ICommandProcessor>().To<RegisterCommandProcessor>().Named(RegisterCommandProcessorName);
            Bind<ICommandProcessor>().To<RemoveCommentCommandProcessor>().Named(RemoveCommentCommandProcessorName).Intercept().With<UserAuthorizationInterceptor>();
            Bind<ICommandProcessor>().To<RemoveVehicleCommandProcessor>().Named(RemoveVehicleCommandProcessorName).Intercept().With<UserAuthorizationInterceptor>();
            Bind<ICommandProcessor>().To<ShowUsersCommandProcessor>().Named(ShowUsersCommandProcessorName).Intercept().With<UserAuthorizationInterceptor>();
            Bind<ICommandProcessor>().To<ShowVehiclesCommandProcessor>().Named(ShowVehiclesCommandProcessorName).Intercept().With<UserAuthorizationInterceptor>();
            Bind<ICommandProcessor>().ToMethod(context =>
            {
                ICommandProcessor registerCommandProcessor = context.Kernel.Get<ICommandProcessor>(RegisterCommandProcessorName);
                ICommandProcessor loginCommandProcessor = context.Kernel.Get<ICommandProcessor>(LoginCommandProcessorName);
                ICommandProcessor logoutCommandProcessor = context.Kernel.Get<ICommandProcessor>(LogoutCommandProcessorName);
                ICommandProcessor addVehicleCommandProcessor = context.Kernel.Get<ICommandProcessor>(AddVehicleCommandProcessorName);
                ICommandProcessor removeVehicleCommandProcessor = context.Kernel.Get<ICommandProcessor>(RemoveVehicleCommandProcessorName);
                ICommandProcessor addCommentCommandProcessor = context.Kernel.Get<ICommandProcessor>(AddCommentCommandProcessorName);
                ICommandProcessor removeCommentCommandProcessor = context.Kernel.Get<ICommandProcessor>(RemoveCommentCommandProcessorName);
                ICommandProcessor showUsersCommandProcessor = context.Kernel.Get<ICommandProcessor>(ShowUsersCommandProcessorName);
                ICommandProcessor showVehiclesCommandProcessor = context.Kernel.Get<ICommandProcessor>(ShowVehiclesCommandProcessorName);

                registerCommandProcessor.Successor = loginCommandProcessor;
                loginCommandProcessor.Successor = logoutCommandProcessor;
                logoutCommandProcessor.Successor = addVehicleCommandProcessor;
                addVehicleCommandProcessor.Successor = removeVehicleCommandProcessor;
                removeVehicleCommandProcessor.Successor = addCommentCommandProcessor;
                addCommentCommandProcessor.Successor = removeCommentCommandProcessor;
                removeCommentCommandProcessor.Successor = showUsersCommandProcessor;
                showUsersCommandProcessor.Successor = showVehiclesCommandProcessor;
                showVehiclesCommandProcessor.Successor = null;

                return registerCommandProcessor;
            }).WhenInjectedInto<DealershipEngine>();
        }
    }
}
