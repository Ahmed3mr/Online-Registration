using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineRegisteration.Shared.Models
{
    public class Department
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public  List<Course> TheCourses { get; set; }

        public List<Student> TheStudents { get; set; }
    }
}
