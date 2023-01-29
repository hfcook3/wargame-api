using System.ComponentModel.DataAnnotations;

namespace WargameApi.Models.Entities;

public record KillTeam
{
    public int Id { get; set; }
    
    [Required]
    [MaxLength(100)]
    public string? Name { get; set; }
    
    public ICollection<Operative> Operatives { get; set; }
}