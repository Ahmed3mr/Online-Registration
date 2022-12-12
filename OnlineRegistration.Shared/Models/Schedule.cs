using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineRegisteration.Shared.Models
{
    public class Schedule
    {

        public int Id { get; set; }

        public int StudentId { get; set; }

        public Student TheStudent { get; set; }

        public int ClassroomId { get; set; }

        public Classroom TheClassroom { get; set; }

        public int RegPeriodId { get; set; }

        public RegPeriod RegPeriod { get; set;}

    }
}
