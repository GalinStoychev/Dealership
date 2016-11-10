using Dealership.ConsoleClient.Configuration;
using Dealership.Contracts;
using Dealership.Engine;
using Ninject;
using Ninject.Extensions.Interception;

namespace Dealership.ConsoleClient.Interceptors
{
    public class UserAuthorizationInterceptor : IInterceptor
    {
        private const string UserNotLogged = "You are not logged! Please login first!";

        public void Intercept(IInvocation invocation)
        {
            //var kernel = new StandardKernel(new DealershipModule());
            //var userService = kernel.Get<IUserService>();
            //if (userService.LoggedUser == null)
            //{
            //    invocation.ReturnValue = UserNotLogged;
            //    return;
            //}

            invocation.Proceed();
            System.Console.WriteLine("Yessssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssss");
        }
    }
}
