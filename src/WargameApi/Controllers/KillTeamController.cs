using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WargameApi.Models;
using WargameApi.Models.Entities;
using WargameApi.Services;

namespace WargameApi.Controllers;

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
    public ActionResult<IEnumerable<KillTeam>> GetKillTeams()
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
    public ActionResult<KillTeam> GetKillTeamById([Required] [FromQuery] int id)
    {
        var result = _killTeamService.GetFullKillTeamById(id);
        if (result == null)
        {
            return NotFound();
        }
        
        return Ok(result);
    }

    [HttpPost]
    [Authorize]
    [Route("addKillTeam")]
    public ActionResult<KillTeam> AddKillTeam([Required] string name)
    {
        var newKillTeam = _killTeamService.AddKillTeam(name);

        return CreatedAtAction("GetKillTeamById", new {id = newKillTeam.Id});
    }
}