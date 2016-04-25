using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CorridorAPI.Models
{
    public class Staffs
    {
        public Staffs(string json)
        {
            JObject jStaff = JObject.Parse(json);
            JArray jStaffArr = (JArray)jStaff["Staff"];
            staffs = new List<Staff>();
            for (int i = 0; i < jStaffArr.Count; i++)
            {
                Staff staff = new Staff();

                staff.schedules = new List<Schedule>();
                staff.signature = (string)jStaffArr[i]["Signature"];
                staff.firstname = (string)jStaffArr[i]["Firstname"];
                staff.lastname = (string)jStaffArr[i]["Lastname"];
                staff.mobile = (string)jStaffArr[i]["Mobile"];
                staff.mail = (string)jStaffArr[i]["Mail"];
                JArray jScheArr = (JArray)jStaffArr[i]["Schedule"];
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
                    staff.schedules.Add(s);
                }
                staffs.Add(staff);
            }
        }
        List<Staff> staffs { get; set; }
    }
}