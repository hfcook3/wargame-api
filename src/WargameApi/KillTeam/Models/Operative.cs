using System.ComponentModel.DataAnnotations;

namespace WargameApi.KillTeam.Models;

public record Operative
{
    public int Id { get; set; }
    
    [Required]
    [MaxLength(100)]
    public string Name { get; set; } = "";
    
    [Required]
    public int MovementDistance { get; set; }
    
    [Required]
    public int MovementCount { get; set; }
    
    [Required]
    public int ActionPointLimit { get; set; }

    [Required] 
    public int GroupActivation { get; set; }
    
    [Required]
    public int Defense { get; set; }
    
    [Required]
    public int SaveThreshold { get; set; }
    
    [Required]
    public int Wounds { get; set; }
    
    public ICollection<Weapon> Weapons { get; set; }

    public ICollection<SpecialRule>? SpecialRules { get; set; }
    
    public ICollection<Keyword>? Keywords { get; set; }
    
    public ICollection<OperativeAction>? SpecialActions { get; set; }
}