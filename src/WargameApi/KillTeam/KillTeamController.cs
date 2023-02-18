using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WargameApi.KillTeam;

[ApiController]
[Route("[controller]")]
public class KillTeamController : ControllerBase
{
    private readonly IKillTeamService _killTeamService;

    public KillTeamController(IKillTeamService killTeamService)
    {
        _killTeamService = killTeamService;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    [HttpGet]
    [AllowAnonymous]
    [Route("getAll")]
    public ActionResult<IEnumerable<KillTeam.Models.KillTeam>> GetKillTeams()
    {
        return Ok(_killTeamService.GetAll());
    }
    
    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    [HttpGet]
    [AllowAnonymous]
    [Route("getById")]
    public ActionResult<KillTeam.Models.KillTeam> GetKillTeamById([Required] [FromQuery] int id)
    {
        var result = _killTeamService.GetFullKillTeamById(id);
        if (result == null)
        {
            return NotFound();
        }
        
        return Ok(result);
    }

    [HttpPost]
    [Authorize(AuthenticationSchemes = $"{JwtBearerDefaults.AuthenticationScheme},ApiKey")]
    [Route("addKillTeam")]
    public ActionResult<KillTeam.Models.KillTeam> AddKillTeam([Required] string name)
    {
        var newKillTeam = _killTeamService.AddKillTeam(name);

        return CreatedAtAction("GetKillTeamById", new {id = newKillTeam.Id});
    }
}