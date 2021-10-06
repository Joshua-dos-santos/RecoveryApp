namespace Recovery_Backend_Data.DTO
{
    public class TrainingDTO
    {
        public int Unique_ID { get; set; }
        public UserDTO User { get; set; }
        public int Duration_in_Days { get; set; }
        public InjuryDTO Injury { get; set; }
        public DietDTO Diet { get; set; }
        public ExerciseDTO Exercise { get; set; }
        public WeekDTO Day_of_Week { get; set; }
    }
}