using System;
using System.Text;
using FEMR.WebAPI.Options;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;

namespace FEMR.WebAPI.Middleware
{
    public static class FemrJwtMiddleware
    {
        public static void AddFemrJwt(this IServiceCollection services, IConfigurationRoot configuration)
        {
            var configurationSection = configuration.GetSection("Jwt");

            var signingKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(configurationSection.GetValue<string>("SecretKey")));

            services.Configure<JwtOptions>(options =>
            {
                options.Audience = configurationSection.GetValue<string>("Audience");
                options.Issuer = configurationSection.GetValue<string>("Issuer");
                options.SigningCredentials = new SigningCredentials(signingKey, SecurityAlgorithms.HmacSha256);
            });
        }

        public static void UseFemrJwtBearerVerification(this IApplicationBuilder app, IConfigurationRoot configuration)
        {
            var jwtAppSettingOptions = configuration.GetSection("Jwt");
            var tokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidIssuer = jwtAppSettingOptions.GetValue<string>("Issuer"),

                ValidateAudience = true,
                ValidAudience = jwtAppSettingOptions.GetValue<string>("Audience"),

                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(jwtAppSettingOptions.GetValue<string>("SecretKey"))),

                RequireExpirationTime = false,
                ValidateLifetime = false,

                ClockSkew = TimeSpan.Zero
            };

            app.UseJwtBearerAuthentication(new JwtBearerOptions
            {
                AutomaticAuthenticate = true,
                AutomaticChallenge = true,
                TokenValidationParameters = tokenValidationParameters
            });
        }
    }
}
