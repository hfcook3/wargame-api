using System.ComponentModel.DataAnnotations;

namespace WargameApi.Models.Entities;

public record OperativeAction
{
    public int Id { get; set; }
    
    [Required]
    public int ActionPointCost { get; set; }
    
    [Required]
    public string? Rule { get; set; }
}