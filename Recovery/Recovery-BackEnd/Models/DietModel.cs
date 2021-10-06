namespace Recovery_BackEnd.Models
{
    public class DietModel
    {
        public int Unique_ID { get; set; }
        public string MealName { get; set; }
        public int Protein { get; set; }
        public int Fats { get; set; }
        public int Carbohydrates { get; set; }
        public int Vitamins { get; set; }
        public int Minerals { get; set; }
    }
}