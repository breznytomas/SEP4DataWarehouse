using System.Text.Json;
using Microsoft.EntityFrameworkCore;
using SEP4DataWarehouse.DbContext;
using SEP4DataWarehouse.Models;
using SEP4DataWarehouse.Services.Interfaces;

namespace SEP4DataWarehouse.Services.Implementations;

public class DbBoardService : IBoardService
{
    private readonly DataWarehouseDbContext _context;


    public DbBoardService(DataWarehouseDbContext context)
    {
        _context = context;
    }


    public async Task AttachUserToBoard(int boardId, string userEmail)
    {
        var board =  _context.Boards.Include(b=> b.UserList).First(b => b.Id==boardId);
        var user = _context.Users.First(u => u.Email.Equals(userEmail));
        
        board.UserList?.Add(user);
        
        await _context.SaveChangesAsync();
       
    }

    public async Task<Board> AddBoardAsync(Board board)
    {
       var entity = await _context.Boards.AddAsync(board);
       await _context.SaveChangesAsync();
       return entity.Entity;

    }

    public async Task DeleteBoard(int boardId)
    {
        var boardToDelete = _context.Boards.Include(b => b.HumidityList).FirstOrDefault(board => board.Id == boardId);
        _context.Boards.Remove(boardToDelete);
        await _context.SaveChangesAsync();
    }
    
    
    
    
}