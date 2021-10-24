using System.Text;
using FitnessCenterManagement.Api.JwtServices;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;

namespace FitnessCenterManagement.Api
{
    public partial class Startup
    {
        public IConfiguration Configuration { get; }

        public void ConfigureAuthentication(IServiceCollection services)
        {
            var tokenSettings = Configuration.GetSection("JwtTokenSettings");

            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
                .AddJwtBearer(options =>
                {
                    options.RequireHttpsMetadata = false;
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidIssuer = JwtValues.Issuer,
                        ValidateAudience = true,
                        ValidAudience = JwtValues.Audience,
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(JwtValues.Key)),
                        ValidateLifetime = false,
                    };
                });

            services.Configure<JwtTokenSettings>(tokenSettings);
            services.AddScoped<JwtTokenService>();
        }
    }
}
