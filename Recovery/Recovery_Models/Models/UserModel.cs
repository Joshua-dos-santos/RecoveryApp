using System.ComponentModel.DataAnnotations;

namespace Recovery_Models.Models
{
    public class UserModel
    {
        [Key]
        public int Unique_ID { get; set; }
        public string First_Name { get; set; }
        public string Last_Name { get; set; }
        [DataType(DataType.Date)]
        public string Birthdate { get; set; }
        public int Height { get; set; }
        public int Weight { get; set; }
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
        [DataType(DataType.Password)]
        public string Password { get; set; }
        public string User_Key { get; set; }
        public PTModel Physical_Therapist { get; set; }
        public InjuryModel Injury { get; set; }
        public DietModel Diet { get; set; }
        public int? Training_Schedule { get; set; }
    }
}
