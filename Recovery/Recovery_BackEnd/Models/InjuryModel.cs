using System.ComponentModel.DataAnnotations;

namespace Recovery_BackEnd.Models
{
    public class InjuryModel
    {
        [Key]
        public int Unique_ID { get; set; }
    }
}