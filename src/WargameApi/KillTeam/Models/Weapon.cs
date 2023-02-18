using System.ComponentModel.DataAnnotations;

namespace WargameApi.KillTeam.Models;

public record Weapon
{
    public int Id { get; set; }
    
    [Required]
    public WeaponType Type { get; set; }

    [Required] public string Name { get; set; } = "";
    
    [Required]
    public int Attacks { get; set; }
    
    [Required]
    public int SkillThreshold { get; set; }
    
    [Required]
    public int NormalDamage { get; set; }
    
    [Required]
    public int CritDamage { get; set; }
    
    public ICollection<SpecialRule>? SpecialRules { get; set; }
    
    public ICollection<CriticalHitRule>? CriticalHitRules { get; set; }
    
    public ICollection<Operative>? Operatives { get; set; }
}