namespace RecoveryApp.Models
{
    public class InjuryModel
    {
        public BodyPartModel Part_of_Body { get; set; }
        public int Pain_Scale { get; set; }
        public string Description { get; set; }

    }
}