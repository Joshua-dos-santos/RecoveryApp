using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Recovery_Backend_Data;
using Recovery_Backend_Data.Data;
using Recovery_Models.Models;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Recovery_BackEnd.Controllers
{
    [Route("[controller]/[action]")]
    public class UserModelsController : Controller
    {
        private readonly AccountData _accountData;
        private readonly PTData _ptData;

        public UserModelsController(RecoveryDBContext context)
        {
            _accountData = new AccountData(context);
            _ptData = new PTData(context);
        }


        [HttpPost("Login")]
        public async Task<IActionResult> Login([FromBody] UserModel user)
        {
            var currentUser = _accountData.GetUser(user.Email, user.Password);

            if (currentUser != null)
            {
                var mySecret = "asdv234234^&%&^%&^hjsdfb2%%%";
                var mySecurityKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(mySecret));

                var tokenHandler = new JwtSecurityTokenHandler();
                var tokenDescriptor = new SecurityTokenDescriptor
                {
                    Subject = new ClaimsIdentity(new Claim[]
                    {
                        new Claim(ClaimTypes.NameIdentifier, currentUser.Unique_ID.ToString()),
                        new Claim(ClaimTypes.Name, currentUser.First_Name +" "+ currentUser.Last_Name),
                    }),
                    Expires = DateTime.UtcNow.AddDays(7),
                    SigningCredentials = new SigningCredentials(mySecurityKey, SecurityAlgorithms.HmacSha256Signature)
                };

                var token = tokenHandler.CreateToken(tokenDescriptor);
                return Ok(tokenHandler.WriteToken(token));
            }
            else
            {
                return StatusCode(StatusCodes.Status401Unauthorized);
            }
        }

        //[HttpGet]
        //public IActionResult Physical_Therapists()
        //{
        //    return Ok(_accountData.);
        //}

        [HttpPost]
        public UserModel AddUser(UserModel user)
        {
            return _accountData.AddUser(user);
        }


    }
}

