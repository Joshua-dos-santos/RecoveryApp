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
    [Route("api/diets")]
    public class DietController : Controller
    {
        private readonly DietData _dietData;

        public DietController(RecoveryDBContext context)
        {
            _dietData = new DietData(context);
        }
        [HttpGet("DietList")]
        public async Task<IActionResult> ShowDietList()
        {
            return Ok(await _dietData.GetDietList());
        }

        [HttpPost("UpdateMeal")]
        public async Task<IActionResult> UpdateMeal([FromQuery]string MealID,[FromQuery] string UserID)
        {
            DietModel diet = await _dietData.GetDiet(Convert.ToInt32(MealID));
            RegisterModel user = await _dietData.UpdateUserDiet(diet, Convert.ToInt32(UserID));
            return Ok(user);
        }

        [HttpPost("StoreMeals")]
        public async Task<IActionResult> StoreMeals([FromBody]List<DietModel> diets)
        {   
            return Ok(await _dietData.StoreDiets(diets));
        }
    }
}
