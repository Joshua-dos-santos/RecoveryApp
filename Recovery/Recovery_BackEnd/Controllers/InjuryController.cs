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
    [Route("api/injury")]
    public class InjuryController : Controller
    {
        private readonly InjuryData _injuryData;

        public InjuryController(RecoveryDBContext context)
        {
            _injuryData = new InjuryData(context);
        }

        [HttpPost("UpdateInjury")]
        public async Task<IActionResult> UpdateInjury([FromBody]InjuryModel injury,[FromQuery] string UserID)
        {
            RegisterModel user = await _injuryData.UpdateUserInjury(injury, Convert.ToInt32(UserID));
            return Ok(user);
        }
    }
}
