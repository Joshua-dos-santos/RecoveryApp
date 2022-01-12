using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Recovery_Models.Models
{
    public class UserListModel
    {
        [Key]
        public int Unique_ID { get; set; }
        public string Name { get; set; }
        public string Birthdate { get; set; }
        public string Height { get; set; }
        public string Weight { get; set; }
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
        public string User_Key { get; set; }
        public string Injury { get; set; }
        public string Diet { get; set; }
        public string Exercise { get; set; }
    }
}
