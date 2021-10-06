namespace Recovery_BackEnd.Models
{
    public class TrainingModel
    {
        public int Unique_ID { get; set; }
        public UserModel User { get; set; }
        public int Duration_in_Days { get; set; }
        public InjuryModel Injury { get; set; }
        public DietModel Diet { get; set; }
        public ExerciseModel Exercise { get; set; }
        public WeekModel Day_of_Week { get; set; }
    }
}