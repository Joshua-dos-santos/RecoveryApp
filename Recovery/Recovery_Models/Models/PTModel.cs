using System.ComponentModel.DataAnnotations;

namespace Recovery_Models.Models
{
    public class PTModel
    {
        [Key]
        public int Unique_ID { get; set; }
        public string First_Name { get; set; }
        public string Last_Name { get; set; }
        public string Birthday { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string PT_Key { get; set; }
    }
}