using Microsoft.AspNetCore.Mvc;
using Recovery_Backend_Data;
using Recovery_Backend_Data.Data;
using Recovery_Models.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Recovery_BackEnd.Controllers
{
    [Route("api/exercises")]
    public class ExerciseController : Controller
    {
        private readonly ExerciseData _exerciseData;
        private readonly AccountData _accountData;

        public ExerciseController(RecoveryDBContext context)
        {
            _exerciseData = new ExerciseData(context);
            _accountData = new AccountData(context);

        }
        [HttpPost("StoreExercises")]
        public async Task<IActionResult> StoreExercises([FromBody] List<ExerciseModel> exercises)
        {
            return Ok(await _exerciseData.StoreExercises(exercises));
        }

        [HttpPost("UpdateExercise")]
        public async Task<IActionResult> UpdateExercise([FromQuery] string exerciseID, [FromQuery] string UserID)
        {
            ExerciseModel exercise = await _exerciseData.GetExercise(Convert.ToInt32(exerciseID));
            RegisterModel user = await _exerciseData.UpdateUserExercise(exercise, Convert.ToInt32(UserID));
            return Ok(user);
        }

        [HttpGet("GetExerciseByUser")]
        public async Task<IActionResult> GetExerciseByUser([FromQuery] string userID)
        {
            RegisterModel user = await _accountData.GetUserByID(Convert.ToInt32(userID));
            ExerciseModel exercise = await _exerciseData.GetExercise(user.Exercise);
            return Ok(exercise);
        }
    }
}
