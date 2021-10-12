using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Recovery_BackEnd.Data;
using Recovery_BackEnd.Models;

namespace Recovery_BackEnd.Controllers
{
    [Route("[controller]/[action]")]
    public class UserModelsController : Controller
    {
        private readonly Recovery_BackEndContext _context;

        public UserModelsController(Recovery_BackEndContext context)
        {
            _context = context;
        }

        // GET: UserModels
        [HttpGet]
        public IActionResult Index()
        {
            return Ok(_context.usermodel);
        } 
    } 
}

