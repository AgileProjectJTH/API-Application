using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Models
{
    public class Schedule
    {
        public string room { get; set; }
        public string date { get; set; }
        public string from { get; set; }
        public string to { get; set; }
        public string signatures { get; set; }
        public string course { get; set; }
        public string moment { get; set; }
    }
}
