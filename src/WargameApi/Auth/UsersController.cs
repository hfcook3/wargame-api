using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using WargameApi.Auth.Models;

namespace WargameApi.Auth;

[ApiController]
[Route("[controller]")]
public class UsersController: ControllerBase
{
    private readonly UserManager<IdentityUser> _userManager;
    private readonly JwtService _jwtService;
    private readonly ApiKeyService _apiKeyService;

    public UsersController(UserManager<IdentityUser> userManager, JwtService jwtService, ApiKeyService apiKeyService)
    {
        _userManager = userManager;
        _jwtService = jwtService;
        _apiKeyService = apiKeyService;
    }
    
    [HttpPost]
    [Route("addNew")]
    public async Task<ActionResult<User>> PostUser(User user)
    {
        var result = await _userManager.CreateAsync(
            new IdentityUser() { UserName = user.UserName, Email = user.Email },
            user.Password
        );

        if (!result.Succeeded)
        {
            return BadRequest(result.Errors);
        }

        user.Password = null;
        return CreatedAtAction("GetUser", new { username = user.UserName }, user);
    }

    [HttpGet]
    [Route("getByName")]
    public async Task<ActionResult<User>> GetUser(string username)
    {
        var user = await _userManager.FindByNameAsync(username);

        if (user == null)
        {
            return NotFound($"User with name {username} not found");
        }

        return new User
        {
            UserName = user.UserName,
            Email = user.Email
        };
    }

    [HttpPost("bearerToken")]
    public async Task<ActionResult<AuthResponse>> CreateBearerToken(AuthRequest request)
    {
        var user = await _userManager.FindByNameAsync(request.UserName);

        if (user == null)
        {
            return BadRequest("Incorrect username and/or password");
        }

        var pwIsValid = await _userManager.CheckPasswordAsync(user, request.Password);

        if (!pwIsValid)
        {
            return BadRequest("Incorrect username and/or password");
        }

        return Ok(_jwtService.CreateToken(user));
    }

    [HttpPost("apiKey")]
    public async Task<ActionResult<UserApiKey>> CreateApiKey(AuthRequest request)
    {
        var user = await _userManager.FindByNameAsync(request.UserName);

        if (user == null)
        {
            return BadRequest("Incorrect username and/or password");
        }

        var pwIsValid = await _userManager.CheckPasswordAsync(user, request.Password);

        
        if (!pwIsValid)
        {
            return BadRequest("Incorrect username and/or password");
        }

        return Ok(_apiKeyService.CreateApiKey(user));
    }
}