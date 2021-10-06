namespace Recovery_Backend_Data.DTO
{
    public class InjuryDTO
    {
        public int Unique_ID { get; set; }
        public BodypartDTO Part_of_Body { get; set; }
        public int Pain_Scale { get; set; }
        public string Description { get; set; }
    }
}