using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using WargameApi.Models;
using WargameApi.Models.Entities;

namespace WargameApi.Data;

public class KillTeamContext : IdentityUserContext<IdentityUser>
{
    public KillTeamContext(DbContextOptions<KillTeamContext> options) : base(options)
    {
    }

    public DbSet<KillTeam> KillTeams => Set<KillTeam>();
    public DbSet<Operative> Operatives => Set<Operative>();
    public DbSet<Weapon> Weapons => Set<Weapon>();
    public DbSet<OperativeAction> Actions => Set<OperativeAction>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
    }
}