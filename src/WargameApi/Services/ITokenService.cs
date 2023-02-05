using Microsoft.AspNetCore.Identity;
using WargameApi.Models.Entities.Identity;

namespace WargameApi.Services;

public interface ITokenService
{
    AuthResponse CreateToken(IdentityUser user);
}