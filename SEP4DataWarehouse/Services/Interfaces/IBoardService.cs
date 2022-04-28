using SEP4DataWarehouse.Models;

namespace SEP4DataWarehouse.Services;

public interface IBoardService
{


     Task AttachUserToBoard(int boardId, string userEmail);
     Task<Board> AddBoardAsync(Board board);
     Task DeleteBoard(int boardId);


}