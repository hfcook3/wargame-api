using WargameApi.KillTeam.Models;

namespace WargameApi.Data;

public class DbInitializer
{
    public static void Initialize(KillTeamContext context)
    {
        if (context.KillTeams.Any() || context.Operatives.Any())
        {
            return;
        }

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

        var kommandoKillTeam = new KillTeam.Models.KillTeam
        {
            Name = "Kommandos",
            Operatives = new List<Operative>
            {
                kommandoBoy
            }
        };

        context.KillTeams.Add(kommandoKillTeam);
        context.SaveChanges();
    }
}