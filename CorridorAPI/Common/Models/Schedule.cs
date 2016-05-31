using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Models
{
    public class Schedule
    {
        public Schedule() { }
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="newRoom">roomNr</param>
        /// <param name="newDate">Date format yyyy-mm-dd-hh-mm-ss</param>
        /// <param name="NewFrom">Time format hh-mm</param>
        /// <param name="NewTo">Time format hh-mm</param>
        public Schedule(string newRoom, string newDate, string NewFrom, string newTo, bool newIsAvailable)
        {
            room = newRoom;
            date = newDate;
            from = NewFrom;
            to = newTo;
            isAvailable = newIsAvailable;
        }
        public int taskId { get; set; }
        public string room { get; set; }
        public string date { get; set; }
        public string from { get; set; }
        public string to { get; set; }
        public string signatures { get; set; }
        public string course { get; set; }
        public string moment { get; set; }
        public bool isAvailable { get; set; }
    }
}
