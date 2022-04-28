using SEP4DataWarehouse.Models;

namespace SEP4DataWarehouse.Services.Interfaces;

public interface IUserService
{
    Task<User> LoginUser(string email, string password);
    Task AddUser<User>(User user);
    
    Task RemoveUser(string userEmail);

    Task ChangePassword(string email, string oldPassword, string newPassword);
}