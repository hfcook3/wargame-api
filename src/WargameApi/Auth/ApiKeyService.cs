using System.Security.Cryptography;
using Microsoft.AspNetCore.Identity;
using WargameApi.Auth.Models;
using WargameApi.Data;

namespace WargameApi.Auth;

public class ApiKeyService
{
    private readonly KillTeamContext _context;

    public ApiKeyService(KillTeamContext context)
    {
        _context = context;
    }

    public UserApiKey CreateApiKey(IdentityUser user)
    {
        var apiKeyValue = GenerateApiKeyValue();
        var apiKeyToStore = new UserApiKey
        {
            User = user,
            Value = HashUtils.ToSha512(apiKeyValue)
        };

        _context.UserApiKeys.Add(apiKeyToStore);
        _context.SaveChanges();

        return new UserApiKey
        {
            User = user,
            Value = apiKeyValue
        };
    }

    private string GenerateApiKeyValue() => Convert.ToBase64String(RandomNumberGenerator.GetBytes(64));
}