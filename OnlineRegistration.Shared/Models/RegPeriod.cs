using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineRegisteration.Shared.Models
{
    public class RegPeriod
    {
        public int ID { get; set; }
        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public  Admin TheAdmin { get; set; }

        public  List<Schedule> TheSchedules { get; set; }


    }
}
