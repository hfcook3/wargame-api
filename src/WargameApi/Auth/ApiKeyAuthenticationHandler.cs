using System.Security.Claims;
using System.Text.Encodings.Web;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using WargameApi.Data;

namespace WargameApi.Auth;

public class ApiKeyAuthenticationHandler : AuthenticationHandler<AuthenticationSchemeOptions>
{
    private const string API_KEY_HEADER = "Api-Key";
    
    private readonly KillTeamContext _context;

    public ApiKeyAuthenticationHandler(IOptionsMonitor<AuthenticationSchemeOptions> options, ILoggerFactory logger,
        UrlEncoder encoder, ISystemClock clock, KillTeamContext context) : base(options, logger, encoder, clock)
    {
        _context = context;
    }

    protected override async Task<AuthenticateResult> HandleAuthenticateAsync()
    {
        if (!Request.Headers.ContainsKey(API_KEY_HEADER))
        {
            return AuthenticateResult.Fail("API key header not found");
        }
        
        string? apiKeyToValidate = Request.Headers[API_KEY_HEADER];

        if (string.IsNullOrEmpty(apiKeyToValidate))
        {
            return AuthenticateResult.Fail("Empty API key value given");
        }
        
        var apiKey = await _context.UserApiKeys
            .Include(uak => uak.User)
            .SingleOrDefaultAsync(uak => uak.Value == HashUtils.ToSha512(apiKeyToValidate));

        if (apiKey == null)
        {
            return AuthenticateResult.Fail("Invalid key");
        }

        return AuthenticateResult.Success(CreateTicket(apiKey.User));
    }

    private AuthenticationTicket CreateTicket(IdentityUser user)
    {
        var claims = new[]
        {
            new Claim(ClaimTypes.NameIdentifier, user.Id),
            new Claim(ClaimTypes.Name, user.UserName ?? "Unknown"),
            new Claim(ClaimTypes.Email, user.Email ?? "Unknown")
        };

        var identity = new ClaimsIdentity(claims, Scheme.Name);
        var principal = new ClaimsPrincipal(identity);
        var ticket = new AuthenticationTicket(principal, Scheme.Name);

        return ticket;
    }
}