using System.Linq;
using System.Threading.Tasks;
using WargameApi.Models.Entities;
using WargameApi.Services;
using Xunit;

namespace IntegrationTests.Contexts.KillTeamServiceTests;

[Collection("TestCollection")]
public class ReadTests
{
    public ReadTests(TestDatabaseFixture fixture)
        => Fixture = fixture;
    
    private TestDatabaseFixture Fixture { get; }

    [Fact]
    public void GetAll()
    {
        var context = Fixture.CreateContext();

        var service = new KillTeamService(context);
        var killTeams = service.GetAll().ToArray();
        
        Assert.Collection(killTeams, team =>
        {
            Assert.Equal("Kommandos", team.Name);
        }, team =>
        {
            Assert.Equal("Veteran Guardsmen", team.Name);
        });
    }
    
    [Fact]
    public void GetFullKillTeamById()
    {
        var context = Fixture.CreateContext();

        var service = new KillTeamService(context);
        var killTeam = service.GetFullKillTeamById(1);
        
        Assert.NotNull(killTeam);
        Assert.Equal("Kommandos", killTeam.Name);
        Assert.Collection(killTeam.Operatives, operative =>
            {
                Assert.Equal("Kommando Boy", operative.Name);
                Assert.Collection(operative.Weapons, weapon =>
                {
                    Assert.Equal(WeaponType.Ballistic, weapon.Type);
                    Assert.Equal("Slugga", weapon.Name);
                }, weapon =>
                {
                    Assert.Equal(WeaponType.Melee, weapon.Type);
                    Assert.Equal("Choppa", weapon.Name);
                });
            });
    }
}