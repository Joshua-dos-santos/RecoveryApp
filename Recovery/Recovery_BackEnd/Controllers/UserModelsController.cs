using Microsoft.AspNetCore.Mvc;
using Recovery_Backend_Data;
using Recovery_Backend_Data.Data;
using Recovery_Models.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Recovery_BackEnd.Controllers
{
    [Route("[controller]/[action]")]
    public class UserModelsController : Controller
    {
        private readonly AccountData _accountData;

        public UserModelsController(RecoveryDBContext context)
        {
            _accountData = new AccountData(context);
        }

        [HttpGet]
        public async Task<IActionResult> GetAllUsers()
        {
            string userID = _accountData.GetUserID("test@test").Result;
            return Ok(await _accountData.GetUser(userID));
        }

        //[HttpGet]
        //public IActionResult Physical_Therapists()
        //{
        //    return Ok(_accountData.);
        //}

        [HttpPost]
        public IActionResult AddUser()
        {
            var newUser = new UserModel()
            {
                First_Name = "Joost",
                Last_Name = "Arens",
                Birthdate = "2004-05-09",
                Height = 174,
                Weight = 64,
                Email = "joost@arens",
                Password = "joost2004"
            };
            return Ok( _accountData.AddUser(newUser));
        }
    }
}

