using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using NetChat.Models;
using NetChat.Services.Interfaces;
using NetChat.Services.Models.Dto;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace NetChat.Services.Security
{
    public class JwtService(IConfiguration configuration) : IJwtService
    {
        public string GenerateJwtToken(CreateJwtTokenDto dto)
        {
            var key = configuration["Jwt:Key"];
            if(key == null) throw new ArgumentNullException("JWT Key is not configured.");
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier, dto.Id.ToString()),
                new Claim(ClaimTypes.Email, dto.Email)
            };

            var token = new JwtSecurityToken(
                configuration["Jwt:Issuer"],
                configuration["Jwt:Audience"],
                claims,
                expires: DateTime.Now.AddMinutes(30),
                signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
