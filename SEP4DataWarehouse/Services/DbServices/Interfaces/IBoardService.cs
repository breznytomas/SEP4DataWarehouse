using SEP4DataWarehouse.Models.DbModels;

namespace SEP4DataWarehouse.Services.DbServices;

public interface IBoardService
{


     Task AttachUserToBoard(string boardId, string userEmail);
     Task<ICollection<Board>> GetBoardsByUser(string userEmail);
     Task<Board> AddBoardAsync(Board board);
     Task DeleteBoard(string boardId);


}