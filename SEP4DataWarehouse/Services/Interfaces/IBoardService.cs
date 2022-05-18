using SEP4DataWarehouse.Models;

namespace SEP4DataWarehouse.Services;

public interface IBoardService
{


     Task AttachUserToBoard(string boardId, string userEmail);
     Task<Board> AddBoardAsync(Board board);
     Task DeleteBoard(string boardId);


}