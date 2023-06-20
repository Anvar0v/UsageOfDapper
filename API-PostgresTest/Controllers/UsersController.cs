using API_PostgresTest.Models;
using API_PostgresTest.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Npgsql;

namespace API_PostgresTest.Controllers;

[ApiController]
[Route("[controller]")]
public class UsersController : ControllerBase
{
    private readonly UsersService _service;
    private readonly IConfiguration _configuration;
    private readonly NpgsqlConnection _connection;

    public UsersController(IConfiguration configuration, UsersService service)
    {
        _service = service;
        _configuration = configuration;
        _connection = new NpgsqlConnection(_configuration.GetConnectionString("Default"));
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<User>>> GetAllUsers()
    {
        return Ok(await _service.GetAllUsersAsync(_connection));
    }

    [HttpGet("{userId:int}")]
    public async Task<ActionResult<User>> GetUserbyId(int userId)
    {
        return Ok(await _service.GetUserByIdAsync(_connection, userId));
    }

    [HttpPost]
    public async Task<IActionResult> AddUser(User user)
    {
        var addUserResult = await _service.AddUserAsync(_connection, user);
        if (addUserResult is false)
            return BadRequest();

        return Ok();
    }

    [HttpDelete("{userId:int}")]
    public async Task<IActionResult> DeleteUser(int userId)
    {
        await _service.DeleteUserByIdAsync(_connection, userId);
        return Ok();
    }

    [HttpPut("{userId:int}")]
    public async Task<IActionResult> UpdateUser(User user, int userId)
    {
        var updateUserResult = await _service.UpdateUserAsync(_connection, user, userId);

        if (updateUserResult is not false)
            return Ok();

        return BadRequest();
    }

}