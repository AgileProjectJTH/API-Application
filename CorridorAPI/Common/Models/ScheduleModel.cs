using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Models
{
    public class ScheduleModel
    {
        [Required]
        public string fromDateAndTime { get; set; }
        [Required]
        public string toDateAndTime { get; set; }
        //roomnumber were staff will be
        public string roomNr { get; set; }
        public string course { get; set; }
        public string scheduleInfo { get; set; }
    }
}
