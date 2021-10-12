using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Recovery_BackEnd.Models
{
    public class UserModel
    {
        [Key]
        public int Unique_ID { get; set; }
        public string First_Name { get; set; }
        public string Last_Name { get; set; }
        public string Birthdate { get; set; }
        public int Height { get; set; }
        public int Weight { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public int? Physical_Therapist { get; set; }
        public int? Injury { get; set; }
        public int? Diet { get; set; }
        public int? Training_Schedule { get; set; }
    }
}
