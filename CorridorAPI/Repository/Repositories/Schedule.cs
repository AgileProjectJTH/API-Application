using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Repositories
{
    public class Schedule
    {
        public int taskId { get; set; }
        public string room { get; set; }
        public string date { get; set; }
        public string fromTime { get; set; }
        public string toTime { get; set; }
        public string course { get; set; }
        public string moment { get; set; }

        public ICollection<Staff_Schedule> Staff_Schedule { get; set; }
    }
}
