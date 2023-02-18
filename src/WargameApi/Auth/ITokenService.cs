using Microsoft.AspNetCore.Identity;
using WargameApi.Auth.Models;

namespace WargameApi.Auth;

public interface ITokenService
{
    AuthResponse CreateToken(IdentityUser user);
}