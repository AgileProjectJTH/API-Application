using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CorridorAPI.Models
{
    public class Task
    {
        public int taskId { get; set; }
        public string room { get; set; }
        public string date { get; set; }
        public string fromTime { get; set; }
        public string toTime { get; set; }
        public string course { get; set; }
        public string moment { get; set; }

        public ICollection<Staff_Task> Staff_Task { get; set; }
    }
}