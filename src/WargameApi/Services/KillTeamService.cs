using Microsoft.EntityFrameworkCore;
using WargameApi.Data;
using WargameApi.Models;
using WargameApi.Models.Entities;

namespace WargameApi.Services;

public interface IKillTeamService
{
    IEnumerable<KillTeam> GetAll();
    KillTeam? GetById(int id);
}

public class KillTeamService : IKillTeamService
{
    private readonly KillTeamContext _killTeamContext;

    public KillTeamService(KillTeamContext killTeamContext)
    {
        _killTeamContext = killTeamContext;
    }

    public IEnumerable<KillTeam> GetAll()
    {
        return _killTeamContext.KillTeams
            .Include(team => team.Operatives)
                .ThenInclude(operative => operative.SpecialRules)
            .Include(team => team.Operatives)
                .ThenInclude(operative => operative.Weapons)
                    .ThenInclude(weapon => weapon.SpecialRules)
            .Include(team => team.Operatives)
                .ThenInclude(operative => operative.Weapons)
                    .ThenInclude(weapon => weapon.CriticalHitRules)
            .Include(team => team.Operatives)
                .ThenInclude(operative => operative.Keywords)
            .Include(team => team.Operatives)
                .ThenInclude(operative => operative.SpecialActions)
            .AsNoTracking()
            .ToList();
    }

    public KillTeam? GetById(int id)
    {
        return _killTeamContext.KillTeams
            .Include(team => team.Operatives)
                .ThenInclude(operative => operative.SpecialRules)
            .Include(team => team.Operatives)
                .ThenInclude(operative => operative.Weapons)
                    .ThenInclude(weapon => weapon.SpecialRules)
            .Include(team => team.Operatives)
                .ThenInclude(operative => operative.Weapons)
                    .ThenInclude(weapon => weapon.CriticalHitRules)
            .Include(team => team.Operatives)
                .ThenInclude(operative => operative.Keywords)
            .Include(team => team.Operatives)
                .ThenInclude(operative => operative.SpecialActions)
            .AsNoTracking()
            .FirstOrDefault(team => team.Id == id);
    }
}