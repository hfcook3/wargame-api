using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Npgsql;
using Respawn;
using WargameApi.Data;
using WargameApi.Models.Entities;

namespace IntegrationTests;

public class TestDatabaseFixture
{
    private const string ConnectionString = @"Host=localhost;Port=5431;Database=integration-test;User ID=postgres;Password=sillylocalpassword";

    private static readonly object _lock = new();
    private static bool _databaseInitialized;

    public TestDatabaseFixture()
    {
        lock (_lock)
        {
            if (_databaseInitialized) return;

            using var context = CreateContext();
            context.Database.EnsureDeleted();
            context.Database.EnsureCreated();

            var kommandoBoy = new Operative
            {
                Name = "Kommando Boy",
                MovementDistance = 2,
                MovementCount = 3,
                ActionPointLimit = 2,
                GroupActivation = 1,
                Defense = 3,
                SaveThreshold = 5,
                Wounds = 10,
                Weapons = new List<Weapon>
                {
                    new()
                    {
                        Type = WeaponType.Ballistic,
                        Name = "Slugga",
                        Attacks = 4,
                        SkillThreshold = 4,
                        NormalDamage = 3,
                        CritDamage = 4,
                        SpecialRules = new List<SpecialRule>
                        {
                            new()
                            {
                                Value = "Rng 6"
                            }
                        }
                    },
                    new()
                    {
                        Type = WeaponType.Melee,
                        Name = "Choppa",
                        Attacks = 4,
                        SkillThreshold = 3,
                        NormalDamage = 4,
                        CritDamage = 5
                    }
                },
                Keywords = new List<Keyword>
                {
                    new ()
                    {
                        Value = "Kommando"
                    },
                    new ()
                    {
                        Value = "Ork"
                    }
                }
            };

            var kommandoKillTeam = new KillTeam
            {
                Name = "Kommandos",
                Operatives = new List<Operative>
                {
                    kommandoBoy
                }
            };
                
            
            var guardsman = new Operative
            {
                Name = "Trooper Veteran",
                MovementDistance = 2,
                MovementCount = 3,
                ActionPointLimit = 2,
                GroupActivation = 2,
                Defense = 3,
                SaveThreshold = 5,
                Wounds = 7,
                Weapons = new List<Weapon>
                {
                    new()
                    {
                        Type = WeaponType.Ballistic,
                        Name = "Lasgun",
                        Attacks = 4,
                        SkillThreshold = 4,
                        NormalDamage = 2,
                        CritDamage = 3
                    },
                    new()
                    {
                        Type = WeaponType.Melee,
                        Name = "Bayonet",
                        Attacks = 3,
                        SkillThreshold = 4,
                        NormalDamage = 2,
                        CritDamage = 3
                    }
                },
                Keywords = new List<Keyword>
                {
                    new ()
                    {
                        Value = "Veteran Guardsman"
                    },
                    new ()
                    {
                        Value = "Imperium"
                    },
                    new ()
                    {
                        Value = "Astra Militarum"
                    }
                }
            };

            var guardsmenKillTeam = new KillTeam
            {
                Name = "Veteran Guardsmen",
                Operatives = new List<Operative>
                {
                    guardsman
                }
            };
                
            context.KillTeams.AddRange(kommandoKillTeam, guardsmenKillTeam);
            context.SaveChanges();

            _databaseInitialized = true;
        }
    }

    public KillTeamContext CreateContext()
        => new(
            new DbContextOptionsBuilder<KillTeamContext>()
                .UseNpgsql(ConnectionString)
                .Options);

    public async Task CleanUp()
    {
        await using var connection = new NpgsqlConnection(ConnectionString);
        connection.Open();
        
        var respawner = await Respawner.CreateAsync(connection, new RespawnerOptions
        {
            SchemasToInclude = new []
            {
                "public"
            },
            DbAdapter = DbAdapter.Postgres
        });

        await respawner.ResetAsync(connection);
    }
}