using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Recovery_Backend_Data.Data
{
    public class JWTTokenHelper
    {
        public static JwtSecurityToken VerifyToken(string jwt, IConfiguration config)
        {
            try
            {
                var tokenHandler = new JwtSecurityTokenHandler();
                var key = Encoding.ASCII.GetBytes(config["Secret"]);

                tokenHandler.ValidateToken(jwt, new TokenValidationParameters
                {
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuerSigningKey = true,
                    ValidateIssuer = false,
                    ValidateAudience = false

                }, out SecurityToken validatedToken);

                return (JwtSecurityToken)validatedToken;
            }

            catch (Exception ex)
            {
                throw new Exception($"Couldn't validate the token. {ex.Message}");
            }
        }
        public static JwtSecurityToken VerifyPTToken(string jwt, IConfiguration config)
        {
            try
            {
                var tokenHandler = new JwtSecurityTokenHandler();
                var key = Encoding.ASCII.GetBytes(config["Secret"]);

                tokenHandler.ValidateToken(jwt, new TokenValidationParameters
                {
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuerSigningKey = true,
                    ValidateIssuer = false,
                    ValidateAudience = false

                }, out SecurityToken validatedToken);

                return (JwtSecurityToken)validatedToken;
            }

            catch (Exception ex)
            {
                throw new Exception($"Couldn't validate the token. {ex.Message}");
            }
        }
    }
}
