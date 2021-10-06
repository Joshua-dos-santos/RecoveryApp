using System;

namespace Recovery_BackEnd.Models
{
    public class PTModel
    {
        public int Unique_ID { get; set; }
        public string First_Name { get; set; }
        public string Last_Name { get; set; }
        public DateTime Birthdate { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string PT_Key { get; set; }

    }
}