using Dealership.Common.Enums;

namespace Dealership.Contracts
{
    public interface IUserFactory
    {
        IUser CreateUser(string username, string firstName, string lastName, string password, Role role);
    }
}
