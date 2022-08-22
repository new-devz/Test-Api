using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace API_Aplication.Model
{
    public class Student
    {
        [Key]
        public int id { get; set; }
        public string Name { get; set; }
        public string Gender { get; set; }
        public string Class { get; set; }
    }
}
