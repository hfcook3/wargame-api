using System.ComponentModel.DataAnnotations;

namespace WargameApi.Models.Entities;

public class SpecialRule
{
    public int Id { get; set; }
    
    [Required]
    public string Value { get; set; }
}