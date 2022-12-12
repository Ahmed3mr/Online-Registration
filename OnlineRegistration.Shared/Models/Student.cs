using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineRegisteration.Shared.Models
{
    public class Student
    {
        [Key]
        public int RegId { get; set; }

        public string Name { get; set; }

        public int CreditHours { get; set; }

        public float GPA { get; set; }

        public Department TheDepartment { get; set; }

        public List<Schedule> TheSchedules { get; set; }



    }
}
