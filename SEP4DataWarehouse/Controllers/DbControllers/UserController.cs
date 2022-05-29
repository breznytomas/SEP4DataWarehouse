using Microsoft.AspNetCore.Mvc;
using SEP4DataWarehouse.DTO.DbDTO;
using SEP4DataWarehouse.Models.DbModels;
using SEP4DataWarehouse.Services.DbServices;

namespace SEP4DataWarehouse.Controllers.DbControllers;


[ApiController]
[Route("api/[controller]")]
public class UserController : ControllerBase
{
    private IUserService _userService;

    public UserController(IUserService userService)
    {
        _userService = userService;
    }


    [HttpPost]
    public async Task<ActionResult<UserDto>> AddUser([FromBody] UserDto userDTO)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        try
        {
            User user = new User()
            {
                Email = userDTO.Email,
                Password = userDTO.Password
            };
            await _userService.AddUser(user);
            return Ok();
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return StatusCode(500, e.Message);
        }
    }


    [Route("Login")]
    [HttpPost]
    public async Task<ActionResult<User>> LoginUser([FromBody] UserDto userDTO)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        try
        {
            var user = await _userService.LoginUser(userDTO.Email, userDTO.Password);
            return Ok(user);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return StatusCode(500, e.Message);
        }



    }


    [HttpPut]
    public async Task<ActionResult<User>> ChangePassword([FromBody] UserDto userDTO, string newPassword)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        try
        {


            await _userService.ChangePassword(userDTO.Email, userDTO.Password, newPassword);
            return Ok();
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return StatusCode(500, e.Message);
        }



    }

    [HttpDelete]
    public async Task<ActionResult<User>> DeleteUser([FromBody] UserDto userDTO)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        try
        {
            await _userService.RemoveUser(userDTO.Email, userDTO.Password);
            return Ok();
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return StatusCode(500, e.Message);
        }


    }

}