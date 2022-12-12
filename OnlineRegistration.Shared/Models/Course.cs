using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineRegisteration.Shared.Models
{
    public class Course
    {
        public int CourseId { get; set; }

        public string Name { get; set; }

        public int CreditHours { get; set; }

        public int? CourseDependenceId { get; set; }

        public  Course? DependantCourse { get; set; }

        
        public  Department TheDepartment { get; set; }


    }


}