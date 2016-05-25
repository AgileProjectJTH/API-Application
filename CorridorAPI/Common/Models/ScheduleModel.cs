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

        public string toDateAndTime { get; set; } //if null API sets time for you
        //roomnumber were staff will be
        public string roomNr { get; set; } //null from apps
        public string course { get; set; } //null from apps
        public string scheduleInfo { get; set; } //null from apps
        [Required]
        public bool available { get; set; }//True if you want to create a schedule that keeps you avaiable
    }
}
