using Microsoft.AspNetCore.Mvc;
using SEP4DataWarehouse.Models;
using SEP4DataWarehouse.Services;
using SEP4DataWarehouse.Services.Interfaces;
using SEP4DataWarehouse.Utilities;

namespace SEP4DataWarehouse.Controllers;

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



    
    [HttpGet]
    public async Task<ActionResult> AttachBoardToUser(int boardId, string userEmail)
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
    
    [HttpPost]
        public async Task<ActionResult<BoardDTO>> AddBoard([FromBody] BoardDTO boardDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            
            try
            {
                Board board = new Board()
                {
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
    public async Task<ActionResult> DeleteBoard(int id)
    {
        try
        {
            await _boardService.DeleteBoard(id);
            return Ok();
        }
        catch (Exception e)
        {
            return exceptionUtility.HandleException(e);
        }
    }
    
    
   
    
    
}