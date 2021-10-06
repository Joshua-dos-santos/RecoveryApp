﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Recovery_BackEnd.Models
{
    public class UserModel
    {
        public int Unique_ID { get; set; }
        public string First_Name { get; set; }
        public string Last_Name { get; set; }
        public DateTime Birthdate { get; set; }
        public int Height { get; set; }
        public int Weight { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public PTModel Physical_Therapist { get; set; }
        public InjuryModel Injury { get; set; }
        public DietModel Diet { get; set; }
        public TrainingModel Training_Schedule { get; set; }
    }
}
