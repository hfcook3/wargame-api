using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace WargameApi.Auth.Models;

[Index(nameof(Value), IsUnique = true)]
public class UserApiKey
{
    [JsonIgnore]
    public int Id { get; set; }

    [Required]
    public string Value { get; set; }

    [JsonIgnore]
    [Required]
    public string UserId { get; set; }

    [JsonIgnore]
    public IdentityUser User { get; set; }
}