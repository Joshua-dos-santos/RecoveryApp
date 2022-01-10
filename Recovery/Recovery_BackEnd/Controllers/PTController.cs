using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Recovery_Backend_Data;
using Recovery_Backend_Data.Data;
using Recovery_Models.Models;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Recovery_BackEnd.Controllers
{
    [Route("[controller]/[action]")]
    public class PTController : Controller
    {
        private readonly PTData _ptData;
        private IConfiguration _config;

        public PTController(RecoveryDBContext context, IConfiguration config)
        {
            _ptData = new PTData(context);
            _config = config;
        }
        [HttpPost("Login")]
        public async Task<IActionResult> LoginPT([FromBody] PTModel pTModel)
        {
            var currentPT = await _ptData.GetPTByLogin(pTModel.Email, pTModel.Password);

            if (currentPT != null)
            {
                var mySecret = "asdv234234^&%&^%&^hjsdfb2%%%";
                var mySecurityKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(mySecret));

                var tokenHandler = new JwtSecurityTokenHandler();
                var tokenDescriptor = new SecurityTokenDescriptor
                {
                    Subject = new ClaimsIdentity(new Claim[]
                    {
                        new Claim(ClaimTypes.NameIdentifier, currentPT.Unique_ID.ToString()),
                        new Claim(ClaimTypes.Name, currentPT.First_Name +" "+ currentPT.Last_Name),
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

        [HttpPost]
        public async Task<IActionResult> RegisterPT([FromBody]PTModel physical_therapist)
        {
            return Ok(await _ptData.RegisterPT(physical_therapist));
        }

        [HttpGet("GetPTByToken")]
        public async Task<ActionResult> GetPTByToken([FromQuery] string jtoken)
        {//Gets the user and company data by the token from the front end, used in the userinfo page to populate the data.
            var user = JWTTokenHelper.VerifyPTToken(jtoken, _config);
            if (user is null)
            {
                return NotFound();
            }
            int id = Convert.ToInt32(user.Claims.First().Value);
            PTModel userById = await _ptData.GetPTByID(id);

            return Ok(userById);
        }

        [HttpGet("GetAllUsers")]
        public async Task<IActionResult> GetAllUsers([FromQuery]string ptid)
        {
            var users = await _ptData.GetUsersByPT(Convert.ToInt32(ptid));
            return Ok(users);
        }
    }
}
