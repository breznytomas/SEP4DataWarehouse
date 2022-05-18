using Microsoft.EntityFrameworkCore;
using SEP4DataWarehouse.DbContext;
using SEP4DataWarehouse.Models;
using SEP4DataWarehouse.Services.Interfaces;

namespace SEP4DataWarehouse.Services.Implementations;

public class DbUserService : IUserService
{
    
    private GreenHouseDbContext _context;

    public DbUserService(GreenHouseDbContext context)
    {
        this._context = context;
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

    public async Task RemoveUser(string userEmail)
    {
        var user = _context.Users.Where(u => u.Email.Equals(userEmail));
        _context.Remove(user);
        await _context.SaveChangesAsync();
    }

   

    public Task ChangePassword(string email, string oldPassword, string newPassword)
    {
        throw new NotImplementedException();
    }
}