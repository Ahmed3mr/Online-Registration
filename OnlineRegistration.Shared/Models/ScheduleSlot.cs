using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineRegistration.Shared.Models
{
    public class ScheduleSlot
    {
        public int Id { get; set; }

        public int SlotNumber { get; set; }

        public string? CourseName { get; set; }

        public string? LecturerName { get; set; }

        public int ClassroomId { get; set; }
    }
}
