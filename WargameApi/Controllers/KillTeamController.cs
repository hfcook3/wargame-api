using Microsoft.AspNetCore.Mvc;
using WargameApi.Data;

namespace WargameApi.Controllers;

[ApiController]
[Route("[controller]")]
public class KillTeamController : ControllerBase
{
    public KillTeamController(KillTeamContext context)
    {
        
    }
}