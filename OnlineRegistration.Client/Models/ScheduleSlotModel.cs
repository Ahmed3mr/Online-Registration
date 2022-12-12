namespace OnlineRegistration.Client.Models
{
    public class ScheduleSlotModel
    {
        public int Id { get; set; }
        public string CourseName { get; set; }

        public int StudentId { get; set; }

        public int ClassroomId { get; set; }

        public int RegPeriodId { get; set; }

        public string LecturerName { get; set; }

        public int  SlotNumber { get; set; }
    }
}
