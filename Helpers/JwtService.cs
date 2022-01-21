using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Artizanalii.Helpers
{
    public class JwtService
    {
        private string _secureKey = "MirceaRaduVladGrupa451";
        public string Generate(int id)
        {
            var symmetricKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_secureKey));
            var credential = new SigningCredentials(symmetricKey, SecurityAlgorithms.HmacSha256Signature);
            var header = new JwtHeader(credential);

            var payload = new JwtPayload(id.ToString(), null, null, null,DateTime.Today.AddDays(1) );
            var securityToken = new JwtSecurityToken(header, payload);

            return new JwtSecurityTokenHandler().WriteToken(securityToken);
        }

        public JwtSecurityToken Verify(string jwt)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_secureKey);

            tokenHandler.ValidateToken(jwt, new TokenValidationParameters 
            {
               IssuerSigningKey = new SymmetricSecurityKey(key),
               ValidateIssuerSigningKey = true,
               ValidateIssuer = false,
               ValidateAudience = false,
            }, out SecurityToken validateToken);

            return (JwtSecurityToken) validateToken;
        }
    }
}
