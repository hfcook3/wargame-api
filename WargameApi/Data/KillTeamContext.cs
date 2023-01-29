using Microsoft.EntityFrameworkCore;
using WargameApi.Models;

namespace WargameApi.Data;

public class KillTeamContext : DbContext
{
    public KillTeamContext(DbContextOptions<KillTeamContext> options) : base(options)
    {
    }

    public DbSet<KillTeam> KillTeams => Set<KillTeam>();
    public DbSet<Operative> Operatives => Set<Operative>();
    public DbSet<Weapon> Weapons => Set<Weapon>();
    public DbSet<OperativeAction> Actions => Set<OperativeAction>();
}