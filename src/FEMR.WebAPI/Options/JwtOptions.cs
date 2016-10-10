using Microsoft.IdentityModel.Tokens;
using System;
using System.Threading.Tasks;

namespace FEMR.WebAPI.Options
{
  public class JwtOptions
  {
    public string Issuer { get; set; } = string.Empty;
    public string Subject { get; set; } = string.Empty;
    public string Audience { get; set; } = string.Empty;
    public DateTime NotBefore { get; set; } = DateTime.UtcNow;
    public DateTime IssuedAt { get; set; } = DateTime.UtcNow;
    public Func<Task<string>> JtiGenerator => () => Task.FromResult(Guid.NewGuid().ToString());
    public SigningCredentials SigningCredentials { get; set; }
  }
}
