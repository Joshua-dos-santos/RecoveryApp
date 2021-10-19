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
        private readonly PTData _ptData;

        public UserModelsController(RecoveryDBContext context)
        {
            _accountData = new AccountData(context);
            _ptData = new PTData(context);
        }


        [HttpGet]
        public async Task<IActionResult> GetAllUsers(UserModel user)
        {
            string userID = _accountData.GetUserID(user.Email, user.Password).Result;
            return Ok(await _accountData.GetUser(userID));
        }

        //[HttpGet]
        //public IActionResult Physical_Therapists()
        //{
        //    return Ok(_accountData.);
        //}

        [HttpPost]
        public UserModel AddUser(UserModel user)
        {
            var newUser = new UserModel()
            {
                First_Name = user.First_Name,
                Last_Name = user.Last_Name,
                Birthdate = user.Birthdate,
                Height = user.Height,
                Weight = user.Weight,
                Email = user.Email,
                Password = user.Password,
                Physical_Therapist = _ptData.GetPT(user.Physical_Therapist.Unique_ID).Result[0],
                Injury = user.Injury,
                Diet = user.Diet,
                Training_Schedule = user.Training_Schedule
            };
            return _accountData.AddUser(newUser);
        }
    }
}

