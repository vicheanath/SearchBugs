using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace SearchBugs.Infrastructure.Authentication;

public class JwtBearerOptionsSetup : IConfigureOptions<JwtBearerOptions>
{
    private readonly JwtOptions _jwtOptions;

    public JwtBearerOptionsSetup(IOptions<JwtOptions> jwtOptions)
    {
        _jwtOptions = jwtOptions.Value;
    }
    public void Configure(JwtBearerOptions options)
    {
        options.TokenValidationParameters = new()
        {
            ValidateIssuer = true,
            ValidIssuer = _jwtOptions.Issuer,
            ValidateAudience = true,
            ValidAudience = _jwtOptions.Audience,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtOptions.Secret))
        };
    }
}
