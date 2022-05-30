using Microsoft.AspNetCore.Mvc;
using SEP4DataWarehouse.DTO.DbDTO;
using SEP4DataWarehouse.Models.DbModels;
using SEP4DataWarehouse.Services.DbServices;
using SEP4DataWarehouse.Utilities;

namespace SEP4DataWarehouse.Controllers.DbControllers;

[ApiController]
[Route("api/[controller]")]

public class BoardController : ControllerBase
{

    private readonly IBoardService _boardService;
    private readonly IExceptionUtilityService exceptionUtility;

    public BoardController(IBoardService boardService, IExceptionUtilityService exceptionUtility)
    {
        _boardService = boardService;
        this.exceptionUtility = exceptionUtility;
    }

    
    [HttpPut]
    public async Task<ActionResult> AttachBoardToUser(string boardId, string userEmail)
    {
        try
        {
            await _boardService.AttachUserToBoard(boardId, userEmail);
            return Ok();
        }
        catch (Exception e)
        {
            return exceptionUtility.HandleException(e);
        }
    }

    [HttpGet]
    public async Task<ActionResult<IList<Board>>> GetBoardsByUser(string userEmail)
    {
        try
        {
          var boards= await _boardService.GetBoardsByUser(userEmail);
          return Ok(boards);
        }
        catch (Exception e)
        {
            return exceptionUtility.HandleException(e);
        }
    }


        [HttpPost]
        public async Task<ActionResult<BoardDto>> AddBoard([FromBody] BoardDto boardDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            
            try
            {
                Board board = new Board()
                {
                    Id = boardDTO.Id,
                    Name = boardDTO.Name,
                    Description = boardDTO.Description

                };
                
                await _boardService.AddBoardAsync(board);
                return Ok(board);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return exceptionUtility.HandleException(e);
            }
        }
      
    
    
    [HttpDelete]
    public async Task<ActionResult> DeleteBoard(string boardId)
    {
        try
        {
            await _boardService.DeleteBoard(boardId);
            return Ok();
        }
        catch (Exception e)
        {
            return exceptionUtility.HandleException(e);
        }
    }
    
    
   
    
    
}