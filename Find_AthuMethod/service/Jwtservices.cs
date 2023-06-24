using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Find_AthuMethod.service
{
    public class Jwtservices
    {
        IConfiguration _configuration;
        public Jwtservices(IConfiguration config) {
            _configuration = config;
        }
        public string CreateToken(User user)
        {
            List<Claim> Claims = new List<Claim>()
            {
                new Claim(ClaimTypes.Email,user.Email),
                new Claim("Password",user.Password)
            };
            var Key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(
                _configuration["Jwt:Key"]!));
           var Cre = new SigningCredentials(Key, SecurityAlgorithms.HmacSha256);

            var Token = new JwtSecurityToken(
               issuer: _configuration["JWT:Issuer"],
               audience: _configuration["JWT:Audience"],
               claims: Claims,
               expires: DateTime.Now.AddMinutes(5),
               signingCredentials: Cre
                );
            return new JwtSecurityTokenHandler().WriteToken(Token);
        }
    }
}
