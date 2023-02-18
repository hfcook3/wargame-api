using Microsoft.EntityFrameworkCore;
using WargameApi.Data;

namespace WargameApi.KillTeam;

public interface IKillTeamService
{
    IEnumerable<KillTeam.Models.KillTeam> GetAll();
    KillTeam.Models.KillTeam? GetFullKillTeamById(int id);
    KillTeam.Models.KillTeam AddKillTeam(string name);
}

public class KillTeamService : IKillTeamService
{
    private readonly KillTeamContext _killTeamContext;

    public KillTeamService(KillTeamContext killTeamContext)
    {
        _killTeamContext = killTeamContext;
    }

    public IEnumerable<KillTeam.Models.KillTeam> GetAll()
    {
        return _killTeamContext.KillTeams
            .AsNoTracking()
            .ToList();
    }

    public KillTeam.Models.KillTeam? GetFullKillTeamById(int id)
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

    public KillTeam.Models.KillTeam AddKillTeam(string name)
    {
        var newKillTeam = _killTeamContext.KillTeams.Add(new KillTeam.Models.KillTeam { Name = name });
        _killTeamContext.SaveChanges();

        return newKillTeam.Entity;
    }
}