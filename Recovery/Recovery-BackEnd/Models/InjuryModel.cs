namespace Recovery_BackEnd.Models
{
    public class InjuryModel
    {
        public int Unique_ID { get; set; }
        public BodypartModel Part_of_Body { get; set; }
        public int Pain_Scale { get; set; }
        public string Description { get; set; }
    }
}