using System.ComponentModel.DataAnnotations;

namespace Recovery_Models.Models
{
    public class DietModel
    {   
        [Key]
        public int Unique_ID { get; set; }
        public string Food_Name { get; set; }
        public int Protein { get; set; }
        public int Fats { get; set; }
        public int Carbohydrates { get; set; }
        public int Vitamins { get; set; }
        public int Minerals { get; set; }
    }
}