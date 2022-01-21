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
        private readonly AccountData _accountData;

        public InjuryController(RecoveryDBContext context)
        {
            _injuryData = new InjuryData(context);
            _accountData = new AccountData(context);
        }

        [HttpPost("UpdateInjury")]
        public async Task<IActionResult> UpdateInjury([FromBody]InjuryModel injurydata,[FromQuery] string userID)
        {
            RegisterModel user = await _injuryData.UpdateUserInjury(injurydata, Convert.ToInt32(userID));
            return Ok(user);
        }

        [HttpGet("GetInjuryByUser")]
        public async Task<IActionResult> GetInjuryByUser([FromQuery] string userID)
        {
            RegisterModel user = await _accountData.GetUserByID(Convert.ToInt32(userID));
            InjuryModel injury = await _injuryData.GetInjury(user.Injury);
            return Ok(injury);
        }
    }
}
