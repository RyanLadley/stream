using ReelStream.auth.Models.Buisness;
using ReelStream.auth.Settings;
using ReelStream.data.Models.Entities;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using System.Collections.Generic;
using System.Linq;

namespace ReelStream.auth.Logic
{
    public class TokenManager
    {
        /// <summary>
        /// Gets that user id from the collection of claims provided.
        /// </summary>
        public static long ExtractUserId(IEnumerable<Claim> claims)
        {
            string userId = claims.FirstOrDefault(claim => claim.Type == "UserId").Value;

            return Int64.Parse(userId);
        }

        /// <summary>
        /// Using the options provided, this returns SigningCredentials for a toke.
        /// </summary>
        /// <returns></returns>
        public SigningCredentials GetSigningCredentials(TokenOptions options)
        {
            var signingCredentials = new SigningCredentials(
                options.SigningKey,
                SecurityAlgorithms.HmacSha512);

            return signingCredentials;
        }

        /// <summary>
        /// Generates a JWT Token tailored to the proved user
        /// </summary>
        /// <returns>JWT Token Response</returns>
        public TokenResponse CreateToken(User user, TokenOptions options)
        {
            var now = DateTime.UtcNow;

            var claims = new Claim[]
            {
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Iat, now.ToString(), ClaimValueTypes.Integer64),
                new Claim("UserId", user.UserId.ToString()),
                new Claim("Role", "General")
            };

            var signingCredentials = GetSigningCredentials(options);

            var jwt = new JwtSecurityToken(
                issuer: options.Issuer,
                audience: options.Audience,
                claims: claims,
                notBefore: now,
                expires: now.Add(options.Expiration),
                signingCredentials: signingCredentials);

            var encodedJwt = new JwtSecurityTokenHandler().WriteToken(jwt);

            return new TokenResponse() { AccessToken = encodedJwt, ExpiresIn = (int)options.Expiration.TotalSeconds };
        }
    }
}
