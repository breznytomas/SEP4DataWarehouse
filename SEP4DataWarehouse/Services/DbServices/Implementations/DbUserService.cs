using Microsoft.EntityFrameworkCore;
using SEP4DataWarehouse.Contexts.DbContext;
using SEP4DataWarehouse.Models.DbModels;

namespace SEP4DataWarehouse.Services.DbServices.Implementations;

public class DbUserService : IUserService
{
    
    private readonly GreenHouseDbContext _context;

    public DbUserService(GreenHouseDbContext context)
    {
        _context = context;
    }

    public async Task<User> LoginUser(string email, string password)
    {
        var user = _context.Users.Include(u=>u.BoardList).FirstOrDefault(u => u.Email == email && u.Password == password);
        if (user != null)
        {
            return user;
        }
        else
        {
            throw new Exception("wrong email or password");
        }
    }

    public async Task AddUser<User>(User user)
    {
        await _context.AddAsync(user);
        await _context.SaveChangesAsync();
    }

    public async Task RemoveUser(string userEmail, string password)
    {
        var user = _context.Users.First(u => u.Email.Equals(userEmail) && u.Password.Equals(password));
         _context.Users.Remove(user);
         await _context.SaveChangesAsync();
    }

   

    public async Task ChangePassword(string email, string oldPassword, string newPassword)
    {
        var user = _context.Users.First(u => u.Email.Equals(email) && u.Password.Equals(oldPassword));
        user.Password = newPassword;
        await _context.SaveChangesAsync();

    }
}