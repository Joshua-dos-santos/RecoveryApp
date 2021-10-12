using System.ComponentModel.DataAnnotations;

namespace Recovery_BackEnd.Models
{
    public class TrainingModel
    {
        [Key]
        public int Unique_ID { get; set; }
    }
}