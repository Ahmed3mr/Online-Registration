namespace OnlineRegistration.Client.Models
{
    public class RegPeriodModel
    {
        public int ID { get; set; }
        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }
        public int TheAdminId { get; set; }
    }
}
