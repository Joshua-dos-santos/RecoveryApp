using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RecoveryApp.Models
{
    public class UserModel
    {
        public string First_name { get; set; }
        public string Last_name { get; set; }
        public DateTime Birthday { get; set; }
        public int Height { get; set; }
        public int Weight { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public InjuryModel Injury { get; set; }
        public Training_schedule Training { get; set; }
        public DietModel UserDiet { get; set; }
        public PTModel Physical_Therapist { get; set; }

    }
}