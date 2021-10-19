using Microsoft.AspNetCore.Mvc;
using Recovery_Models.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Recovery_FrontEnd.Controllers
{
    public class AccountController : Controller
    {
        public IActionResult Index()
        {
            Recovery_BackEnd.Data.AccountContainer accountContainer = new Recovery_BackEnd.Data.AccountContainer();
            UserModel user = accountContainer.AddUser();
            return View();
        }
    }
}
