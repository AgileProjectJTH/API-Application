using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CorridorAPI.Models
{
    public class Staff
    {
        public Staff(JToken JsonStaff)
        {
            schedules = new List<Schedule>();
            signature = (string)JsonStaff["Signature"];
            firstname = (string)JsonStaff["Firstname"];
            lastname = (string)JsonStaff["Lastname"];
            mobile = (string)JsonStaff["Mobile"];
            mail = (string)JsonStaff["Mail"];
            JArray jScheArr = (JArray)JsonStaff["Schedule"];
            for (int k = 0; k < jScheArr.Count; k++)
            {
                Schedule s = new Schedule();
                s.room = (string)jScheArr[k]["Room"];
                s.date = (string)jScheArr[k]["Date"];
                s.from = (string)jScheArr[k]["From"];
                s.to = (string)jScheArr[k]["To"];
                s.signatures = (string)jScheArr[k]["Signatures"];
                s.course = (string)jScheArr[k]["Course"];
                s.moment = (string)jScheArr[k]["Moment"];
                schedules.Add(s);
            }
        }
        public string signature { get; set; }
        public string firstname { get; set; }
        public string lastname { get; set; }
        public string mobile { get; set; }
        public string mail { get; set; }
        public List<Schedule> schedules { get; set; }
    }
}