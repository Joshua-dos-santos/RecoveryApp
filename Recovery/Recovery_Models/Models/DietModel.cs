using System.ComponentModel.DataAnnotations;

namespace Recovery_Models.Models
{
    public class DietModel
    {   
        [Key]
        public int Unique_ID { get; set; }
        public string Meal { get; set; }
        public decimal Protein { get; set; }
        public decimal Fats { get; set; }
        public decimal Carbohydrates { get; set; }
        public decimal calories { get; set; }
        public decimal fibers { get; set; }
    }
}