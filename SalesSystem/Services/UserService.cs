using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using SalesSystem.Models;
using SalesSystem.Models.Common;
using SalesSystem.Models.Request;
using SalesSystem.Models.Response;
using SalesSystem.Tools;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace SalesSystem.Services
{
    public class UserService : IUserService
    {

        private readonly AppSettings _appSettings;

        public UserService(IOptions<AppSettings> appSettings)
        {
            _appSettings = appSettings.Value;
        }

        public UserResponse Auth(AuthReq req)

        {
            UserResponse oUserResponse = new();

            using (SalesSystemContext db = new())
            {
                string spa = Encrypt.GetSHA256(req.Password);
                var user = db.Users.Where(u => u.Email == req.Email && u.Password == spa).FirstOrDefault();

                if (user == null) return null;

                oUserResponse.Email = user.Email;
                oUserResponse.Token = GetToken(user);

                return oUserResponse;
            }
        }


        private string GetToken(User user)
        {
            var tokenHandler = new JwtSecurityTokenHandler();

            var key = Encoding.ASCII.GetBytes(_appSettings.Secret);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(
                    new Claim[] {
                    new Claim (ClaimTypes.NameIdentifier, user.Id.ToString()),
                    new Claim (ClaimTypes.Email, user.Email)
                    }
                    ),

                Expires = DateTime.UtcNow.AddDays(60),
                SigningCredentials = new SigningCredentials(
                    new SymmetricSecurityKey(key),
                    SecurityAlgorithms.HmacSha256Signature
                    )
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(token);
        }
    }
}
