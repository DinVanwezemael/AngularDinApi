using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ProjectDin.Services;
using ProjectDin.Helpers;
using ProjectDin.Controllers;
using ProjectDin.Models;
using System.IdentityModel.Tokens.Jwt;
using System.IO;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;
using System.Security.Cryptography;
using System.Text;
using Microsoft.Extensions.Options;

namespace ProjectDin.Services
{
    public class UserService : IUserService
    {
        private readonly AppSettings _appSettings;
        private readonly ProjectContext _memberContext;

        public UserService(IOptions<AppSettings> appSettings, ProjectContext memberContext)
        {
            _appSettings = appSettings.Value;
            _memberContext = memberContext;
        }
        public User Authenticate(string username, string password)
        {
            var user = _memberContext.Users.SingleOrDefault(x => x.Username == username && x.Password == password);
            // return null if user not found

            if (user == null)
                return null;
            // authentication successful so generate jwttoken
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_appSettings.Secret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                    {
                    new Claim("UserID", user.UserID.ToString()),
                    new Claim("Email", user.Email),
                    new Claim("Username", user.Username)
                        }),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature
                    )
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            user.Token = tokenHandler.WriteToken(token);
            // remove password before returning
            user.Password = null;
            return user;
        }
    }
}
