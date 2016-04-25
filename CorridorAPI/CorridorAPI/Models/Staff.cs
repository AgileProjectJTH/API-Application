using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CorridorAPI.Models
{
    public class Staff
    {
        public string signature { get; set; }
        public string firstname { get; set; }
        public string lastname { get; set; }
        public string mobile { get; set; }
        public string mail { get; set; }
        public List<Schedule> schedules { get; set; }
    }
}