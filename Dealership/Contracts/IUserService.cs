using System.Collections.Generic;

namespace Dealership.Contracts
{
    public interface IUserService
    {
        IUser LoggedUser { get; }

        void LogIn(IUser user);

        void LogOut();

        void Register(IUser user);

        bool ContainsUser(string username);

        IUser GetUser(string username);

        IEnumerable<IUser> GetAllUsers();

        void DeleteAllUsers();
    }
}
