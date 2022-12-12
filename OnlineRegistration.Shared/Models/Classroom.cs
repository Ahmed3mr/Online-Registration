using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineRegisteration.Shared.Models
{
    public class Classroom
    {

        public int Id { get; set; }

        public int SlotName { get; set; }

        public int Capacity { get; set; }

        public int CourseID { get; set; }

        public Course TheCourse { get; set; }

        public int LecturerId { get; set; }

        public Lecturer TheLecturer { get; set; }
        public List<Schedule> TheSchedules { get; set; }

    }
}
