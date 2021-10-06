using System;
using System.Collections.Generic;
using System.Text;

namespace Recovery_Backend_Data.DTO
{
    public class UserDTO
    {
        public int Unique_ID { get; set; }
        public string First_Name { get; set; }
        public string Last_Name { get; set; }
        public DateTime Birthdate { get; set; }
        public int Height { get; set; }
        public int Weight { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public PTDTO Physical_Therapist { get; set; }
        public InjuryDTO Injury { get; set; }
        public DietDTO Diet { get; set; }
        public TrainingDTO Training_Schedule { get; set; }
    }
}
