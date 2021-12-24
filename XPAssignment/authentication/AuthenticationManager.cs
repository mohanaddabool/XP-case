using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;

namespace XPAssignment.authentication;

public class AuthenticationManager: IAuthentication
{
    private readonly IDictionary<string, string> user = new Dictionary<string, string>
    {
        { "test@test.nl", "123" }
    };

    private readonly IConfiguration configuration;

    public AuthenticationManager(IConfiguration configuration)
    {
        this.configuration = configuration;
    }

    public string Authenticate(string? emailAddress, string? password)
    {
        var key = configuration.GetValue<string>("JwtToken:key");
        if (!user.Any(u => u.Key == emailAddress && u.Value == password))
        {
            return string.Empty;
        }

        var tokenHandler = new JwtSecurityTokenHandler();
        var tokenKey = Encoding.UTF8.GetBytes(key);
        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(new Claim[]
            {
                new Claim(ClaimTypes.Name, emailAddress!)
            }),
            Expires = DateTime.UtcNow.AddHours(1),
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(tokenKey),
                SecurityAlgorithms.HmacSha256Signature)
        };
        var token = tokenHandler.CreateToken(tokenDescriptor);
        return tokenHandler.WriteToken(token);
    }
}