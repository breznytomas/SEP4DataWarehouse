using SEP4DataWarehouse.Models.DbModels;

namespace SEP4DataWarehouse.Services.DbServices;

public interface IUserService
{
    Task<User> LoginUser(string email, string password);
    Task AddUser<User>(User user);
    
    Task RemoveUser(string userEmail ,string password);

    Task ChangePassword(string email, string oldPassword, string newPassword);
}