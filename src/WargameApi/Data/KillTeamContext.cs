using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using WargameApi.Auth.Models;
using WargameApi.KillTeam.Models;

namespace WargameApi.Data;

public class KillTeamContext : IdentityUserContext<IdentityUser>
{
    public KillTeamContext(DbContextOptions<KillTeamContext> options) : base(options)
    {
    }

    public DbSet<KillTeam.Models.KillTeam> KillTeams => Set<KillTeam.Models.KillTeam>();
    public DbSet<Operative> Operatives => Set<Operative>();
    public DbSet<Weapon> Weapons => Set<Weapon>();
    public DbSet<OperativeAction> Actions => Set<OperativeAction>();
    public DbSet<UserApiKey> UserApiKeys => Set<UserApiKey>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
    }
}