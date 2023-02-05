using System.ComponentModel.DataAnnotations;

namespace WargameApi.Models.Entities.Identity;

public class AuthRequest
{
    [Required]
    public string UserName { get; set; }
    [Required]
    public string Password { get; set; }
}