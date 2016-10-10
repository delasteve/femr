using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using FEMR.Core;
using FEMR.DomainModels;
using FEMR.Queries;
using FEMR.WebAPI.Extensions;
using FEMR.WebAPI.Models;
using FEMR.WebAPI.Options;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace FEMR.WebAPI
{
    [Route("api/[controller]/token")]
    public class AuthController
    {
        private readonly JwtOptions _jwtOptions;
        private readonly IQueryProcessor _queryProcessor;
        private readonly IPasswordEncryptor _passwordEncryptor;

        public AuthController(IOptions<JwtOptions> jwtOptions, IPasswordEncryptor passwordEncryptor, IQueryProcessor queryProcessor)
        {
            _jwtOptions = jwtOptions.Value;
            _passwordEncryptor = passwordEncryptor;
            _queryProcessor = queryProcessor;
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] AuthPostModel authPostModel)
        {
            var errorResult = new JsonResult(new { message = "Invalid email or password." }) { StatusCode = 401 };

            var user = await _queryProcessor.Process(new GetAuthUser(authPostModel.Email));
            if (user == null) { return errorResult; }

            var verified = _passwordEncryptor.VerifyPassword(authPostModel.Password, user.Password);
            if (!verified) { return errorResult; }

            var token = await CreateJwt(user);
            var signedToken = new JwtSecurityTokenHandler().WriteToken(token);

            return new JsonResult(new { accessToken = signedToken });
        }

        private async Task<JwtSecurityToken> CreateJwt(User user)
        {
            var claims = await MakeClaims(user);

            return new JwtSecurityToken(
                issuer: _jwtOptions.Issuer,
                audience: _jwtOptions.Audience,
                claims: claims.ToArray(),
                notBefore: _jwtOptions.NotBefore,
                signingCredentials: _jwtOptions.SigningCredentials);
        }

        private async Task<IList<Claim>> MakeClaims(User user)
        {
            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.Email),
                new Claim(JwtRegisteredClaimNames.Jti, await _jwtOptions.JtiGenerator()),
                new Claim(JwtRegisteredClaimNames.Iat, _jwtOptions.IssuedAt.ToUnixTime().ToString(), ClaimValueTypes.Integer64),
                new Claim("firstName", user.FirstName),
                new Claim("lastName", user.LastName)
            };

            var scopes = new List<string>
            {
                "users:read",
                "users:create"
            };

            scopes.ForEach(role => claims.Add(new Claim("scopes", role)));

            return claims;
        }
    }
}
