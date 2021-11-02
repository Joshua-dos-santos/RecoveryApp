using System.ComponentModel.DataAnnotations;

namespace Recovery_Models.Models
{
    public class InjuryModel
    {
        [Key]
        public int Unique_ID { get; set; }
        public int Part_of_Body { get; set; }
        public int Pain_Scale { get; set; }
        public string Description { get; set; }
    }
}