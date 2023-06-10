using Microsoft.AspNetCore.Mvc;
using TaskManagerServer.Models;
using TaskManagerServer.Repositories.Interfaces;

namespace TaskManagerServer.Controllers;
[Route("api/[controller]")]
[ApiController]

public class UserController : ControllerBase
{
    private readonly IUserRepository _userRepository;

    public UserController(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    [HttpGet]
    public async Task<ActionResult<List<UserModel>>> SearchAllUsers()
    {
        List<UserModel> users = await _userRepository.SearchAllUsers();
        return Ok(users);
    }
        
    [HttpGet("{id:int}")]
    public async Task<ActionResult<UserModel>> SearchUser(int id)
    {
        UserModel user = await _userRepository.SearchById(id);
        return Ok(user);
    }
        
    [HttpPost]
    public async Task<ActionResult<UserModel>> Register([FromBody] UserModel userModel)
    {
        UserModel user = await _userRepository.Add(userModel);
        return Ok(user);
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<UserModel>> Refresh([FromBody] UserModel userModel, int id)
    {
        userModel.Id = id;
        UserModel user = await _userRepository.Update(userModel, id);
        return Ok(user);
    }
        
    [HttpDelete("{id}")]
    public async Task<ActionResult<UserModel>> Delete([FromBody] int id)
    {
        bool deleted = await _userRepository.Delete(id);
        return Ok(deleted);
    }
    
}