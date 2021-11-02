using System.ComponentModel.DataAnnotations;

namespace Recovery_Models.Models
{
    public class InjuryModel
    {
        [Key]
        public int Unique_ID { get; set; }
        public string Ijury_Name { get; set; }
        public BodyPartModel Part_of_Body { get; set; }
        public int Pain_Scale { get; set; }
    }
}