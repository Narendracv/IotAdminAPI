using IotAdminAPI.Models;
using IotAdminAPI.ViewModel;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;

namespace IotAdminAPI.Authentication
{
    public static class JwtAuthentication
    {
       
      
     
        
        public static AuthenticationResponse  GenerateToken(User user,string issuerSigningKey)
        {
            
     
            if (user == null) return null;

            // 1. Create Security Token Handler
            var tokenHandler = new JwtSecurityTokenHandler();

            // 2. Create Private Key to Encrypted
            var tokenKey = Encoding.ASCII.GetBytes(issuerSigningKey);

            //3. Create JETdescriptor
            var tokenDescriptor = new SecurityTokenDescriptor()
            {
                Subject = new ClaimsIdentity(
                    new Claim[]
                    {
                        new Claim(ClaimTypes.Name, user.Username),
                        new Claim(ClaimTypes.Role,user.UserRoles?.FirstOrDefault()?.Role?.Name)
                    }),
                Expires = DateTime.UtcNow.AddHours(1),
                SigningCredentials = new SigningCredentials(
                    new SymmetricSecurityKey(tokenKey), SecurityAlgorithms.HmacSha256Signature)
            };
            //4. Create Token
            var token = tokenHandler.CreateToken(tokenDescriptor);

            // 5. Return Token from method
            string generatedToken= tokenHandler.WriteToken(token);

            TimeSpan diff = DateTime.Now - tokenDescriptor.Expires.Value;
            return new AuthenticationResponse(generatedToken, "bearer", diff.TotalMilliseconds);
        }
    }
}
