using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Recovery_Models.Models
{
    public class ExerciseModel
    {
        [Key]
        public int Unique_ID { get; set; }
        public string Name { get; set; }
        public string BodyPart { get; set; }
        public string Equipment { get; set; }
        public string Target { get; set; }
        public string GifUrl { get; set; }
    }
}
