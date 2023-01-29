using System.ComponentModel.DataAnnotations;

namespace WargameApi.Models;

public class Keyword
{
    public int Id { get; set; }
    
    [Required]
    public string Value { get; set; }
}