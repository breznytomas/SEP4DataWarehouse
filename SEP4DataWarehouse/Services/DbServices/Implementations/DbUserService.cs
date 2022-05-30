using System.Data;
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
        try
        {
            var user = _context.Users.Include(u=>u.BoardList).FirstOrDefault(u => u.Email == email && u.Password == password);
            return user;
            
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw new KeyNotFoundException("user not found");
        }
       
     
    }

    public async Task AddUser<User>(User user)
    {
        await _context.AddAsync(user);
        await _context.SaveChangesAsync();
    }

    public async Task RemoveUser(string userEmail, string password)
    {
        try
        {
            var user = _context.Users.First(u => u.Email.Equals(userEmail) && u.Password.Equals(password));
            _context.Users.Remove(user);
            await _context.SaveChangesAsync();
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw new KeyNotFoundException("user id not found");
        }
       
    }

   

    public async Task ChangePassword(string email, string oldPassword, string newPassword)
    {
        try
        {
            var user = _context.Users.First(u => u.Email.Equals(email) && u.Password.Equals(oldPassword));
            user.Password = newPassword;
            await _context.SaveChangesAsync();
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw new ConstraintException("Old and new password does not match or user not found");
        }
       

    }
}