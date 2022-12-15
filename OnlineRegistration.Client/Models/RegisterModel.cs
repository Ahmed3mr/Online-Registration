namespace OnlineRegistration.Client.Models
{
    public class RegisterModel
    {
        public int studentId { get; set; }
        public int regperiodId { get; set; }

        public List<int> classroomIds { get; set; }

    }
}
