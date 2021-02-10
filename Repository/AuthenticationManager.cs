using Contracts;
using Entities.DTO;
using Entities.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    public class AuthenticationManager : IAuthenticationManager
    {
        private readonly UserManager<User> _userManager;
        private readonly IConfiguration _configuration;
        private User _user;
        public string _message;
        public AuthenticationManager(UserManager<User> userManager, IConfiguration configuration)
        {
            _userManager = userManager;
            _configuration = configuration;
        }
        public async Task<bool> ValidateUser(LoginDTO userForAuth)
        {
            _user = await _userManager.FindByNameAsync(userForAuth.UserName);
            if (_user == null)
            {
                _message = "User not found";
                return false;
            }

            bool checkPass = await _userManager.CheckPasswordAsync(_user, userForAuth.Password);
            if (!checkPass)
            {
                await _userManager.AccessFailedAsync(_user);
                if (await _userManager.IsLockedOutAsync(_user))
                {
                    _message = $"Your account is locked out contact admin";
                    return false;
                }
                _message = "Invalid password.";
                return false;
            }
            await _userManager.ResetAccessFailedCountAsync(_user);
            await _userManager.SetLockoutEndDateAsync(_user, new DateTime(2000, 1, 1));
            return true;
        }
        public async Task<string> CreateToken()
        {
            var signingCredentials = GetSigningCredentials();
            var claims = await GetClaims();
            var tokenOptions = GenerateTokenOptions(signingCredentials, claims);
            return new JwtSecurityTokenHandler().WriteToken(tokenOptions);
        }
        private SigningCredentials GetSigningCredentials()
        {
            var key = Encoding.UTF8.GetBytes("thisismyscretkey");
            var secret = new SymmetricSecurityKey(key);
            return new SigningCredentials(secret, SecurityAlgorithms.HmacSha256);
        }
        private async Task<List<Claim>> GetClaims()
        {
            var claims = new List<Claim>
            {
               new Claim(ClaimTypes.Name, _user.UserName),
               new Claim(ClaimTypes.NameIdentifier, _user.Id)
            };



            var roles = await _userManager.GetRolesAsync(_user);
            foreach (var role in roles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role));
            }
            return claims;
        }
        private JwtSecurityToken GenerateTokenOptions(SigningCredentials
       signingCredentials, List<Claim> claims)
        {
            // var jwtSettings = _configuration.GetSection("JwtSettings");
            var tokenOptions = new JwtSecurityToken
            (
                issuer: "GrapeCity",
                audience: "GrapeCity",
                claims: claims,
                expires: DateTime.Now.AddDays(30),
                signingCredentials: signingCredentials
            );
            return tokenOptions;
        }
        public string GetMessage()
        {
            return _message;
        }

        public User getUser()
        {
            return this._user;
        }
    }
}
