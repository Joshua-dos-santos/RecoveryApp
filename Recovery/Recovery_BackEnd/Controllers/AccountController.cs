﻿using Microsoft.AspNetCore.Cryptography.KeyDerivation;
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
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Recovery_BackEnd.Controllers
{
    [Route("[controller]/[action]")]
    public class AccountController : Controller
    {
        private readonly AccountData _accountData;
        private IConfiguration _config;

        public AccountController(RecoveryDBContext context, IConfiguration config)
        {
            try
            {
                _accountData = new AccountData(context);
                _config = config;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }


        [HttpPost("Login")]
        public async Task<IActionResult> Login([FromBody] LoginModel user)
        {
            var currentUser = await _accountData.GetUserByLogin(user.Email, Utilities.HashPassword(user.Password));
            Console.WriteLine(currentUser);
            if (currentUser != null)
            {
                _config["Secret"] = "asdv234234^&%&^%&^hjsdfb2%%%";
                var mySecurityKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(_config["Secret"]));

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

        [HttpPost("Register")]
        public async Task<IActionResult> Register([FromBody] RegisterModel user)
        {
            return Ok(await _accountData.Register(user));
        }

        [HttpGet("GetUserByToken")]
        public async Task<IActionResult> GetUserByToken([FromQuery] string jtoken)
        {//Gets the user and company data by the token from the front end, used in the userinfo page to populate the data.
            var user = JWTTokenHelper.VerifyToken(jtoken, _config);
            if (user is null)
            {
                return NotFound();
            }
            int id = Convert.ToInt32(user.Claims.First().Value);
            RegisterModel userById = await _accountData.GetUserByID(id);

            return Ok(userById);
        }

        [HttpPost("UpdateUser")]
        public async Task<IActionResult> UpdateUser([FromBody] RegisterModel user)
        {
            int id = user.Unique_ID;
            RegisterModel userbyid = await _accountData.GetUserByID(id);

            if (userbyid is null)
            {
                return NotFound();
            }
                await _accountData.UpdateUser(user);
            await _accountData.SaveAsync();
            return Ok();
        }
    }
}


