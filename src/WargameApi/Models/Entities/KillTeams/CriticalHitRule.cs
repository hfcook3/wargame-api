using System.ComponentModel.DataAnnotations;

namespace WargameApi.Models.Entities.KillTeams;

public class CriticalHitRule
{
    public int Id { get; set; }
    
    [Required]
    public string Value { get; set; }
}