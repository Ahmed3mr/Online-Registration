using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineRegisteration.Shared.Models
{
    public class Lecturer
    {

        public int ID { get; set; }
        public string Name { get; set; }

        public List<Classroom> TheClassrooms { get; set; }
        
    }
}
