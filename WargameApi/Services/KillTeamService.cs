using Microsoft.EntityFrameworkCore;
using WargameApi.Data;
using WargameApi.Models;

namespace WargameApi.Services;

public class KillTeamService
{
    private readonly KillTeamContext _killTeamContext;

    public KillTeamService(KillTeamContext killTeamContext)
    {
        _killTeamContext = killTeamContext;
    }

    public IEnumerable<KillTeam> GetAll()
    {
        return _killTeamContext.KillTeams
            .AsNoTracking()
            .ToList();
    }

    public KillTeam? GetById(int id)
    {
        return _killTeamContext.KillTeams
            .Include(team => team.Operatives)
            .AsNoTracking()
            .FirstOrDefault(team => team.Id == id);
    }
}