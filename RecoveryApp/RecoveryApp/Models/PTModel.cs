using System;

namespace RecoveryApp.Models
{
    public class PTModel
    {
        public int Unique_ID { get; set; }
        public string First_Name { get; set; }
        public string Last_Name { get; set; }
        public DateTime Birthday { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public int PT_number { get; set; }
    }
}