namespace WargameApi.Auth.Models;

public class AuthResponse
{
    public string Token { get; set; }
    public DateTime Expiration { get; set; }
}