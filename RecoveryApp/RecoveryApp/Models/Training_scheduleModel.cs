namespace RecoveryApp.Models
{
    public class Training_scheduleModel
    {
        public int Unique_ID { get; set; }
        public UserModel User { get; set; }
        public int Duration_in_Days { get; set; }
        public InjuryModel Injury { get; set; }
        public DietModel Diet { get; set; }
        public ExcersiseModel Excersise { get; set; }
        public Day_of_WeekModel Day_Of_Week { get; set; }
    }
}