using Microsoft.AspNetCore.Mvc;
using SEP4DataWarehouse.DTO.DbDTO;
using SEP4DataWarehouse.Models.DbModels;
using SEP4DataWarehouse.Services.DbServices;
using SEP4DataWarehouse.Utilities;

namespace SEP4DataWarehouse.Controllers.DbControllers;


[ApiController]
[Route("api/[controller]")]
public class UserController : ControllerBase
{
    private IUserService _userService;
    private IExceptionUtilityService _exceptionUtility;

    public UserController(IUserService userService, IExceptionUtilityService exceptionUtility)
    {
        _userService = userService;
        _exceptionUtility = exceptionUtility;
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
            return _exceptionUtility.HandleException(e);
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
            return _exceptionUtility.HandleException(e);
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
            return _exceptionUtility.HandleException(e);
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
            return _exceptionUtility.HandleException(e);
        }
    }

}