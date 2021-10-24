using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using FitnessCenterManagement.Api.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace FitnessCenterManagement.Api.JwtServices
{
    public class JwtTokenService
    {
        private const int DaysExpirationTerm = 50;
        private readonly JwtTokenSettings _settings;

        public JwtTokenService(IOptions<JwtTokenSettings> options)
        {
            if (options != null)
            {
                _settings = options.Value;
            }
        }

        public string GetToken(User user, IList<string> roles)
        {
            if (user is null)
            {
                throw new ArgumentNullException(nameof(user));
            }

            // Adding claims to the list of all claims inside the JWT token.
            var roleClaims = roles.Select(role => new Claim(ClaimTypes.Role, role));
            var userClaims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.UserName),
                new Claim("userId", user.Id),
                new Claim(ClaimTypes.NameIdentifier, user.UserName),
                new Claim(JwtRegisteredClaimNames.GivenName, user.FirstName),
            };
            userClaims.AddRange(roleClaims);
            var jwt = new JwtSecurityToken(
                issuer: JwtValues.Issuer,
                audience: JwtValues.Audience,
                signingCredentials: new SigningCredentials(
                    new SymmetricSecurityKey(Encoding.UTF8.GetBytes(JwtValues.Key)),
                    SecurityAlgorithms.HmacSha256),
                claims: userClaims,
                expires: DateTime.Now.AddDays(DaysExpirationTerm));

            var encodedJwt = new JwtSecurityTokenHandler().WriteToken(jwt);

            return encodedJwt;
        }

        public bool ValidateToken(string token)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            try
            {
                tokenHandler.ValidateToken(
                token,
                new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidIssuer = JwtValues.Issuer,
                    ValidateAudience = true,
                    ValidAudience = JwtValues.Audience,
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(JwtValues.Key)),
                    ValidateLifetime = true,
                    LifetimeValidator =
                        (before, exprise, localToken, parameters) =>
                        {
                            if (exprise.HasValue)
                            {
                                return exprise.Value > DateTime.UtcNow;
                            }

                            return true;
                        },
                    RequireExpirationTime = true,
                },
                out var _);
            }
            catch (SecurityTokenException)
            {
                return false;
            }
            catch (ArgumentException)
            {
                return false;
            }

            return true;
        }
    }
}
