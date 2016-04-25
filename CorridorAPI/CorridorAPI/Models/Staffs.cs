using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CorridorAPI.Models
{
    public class Staffs
    {
        /// <summary>
        /// Konverts Json to List of Staffs
        /// </summary>
        /// <param name="json"></param>
        public Staffs(string json)
        {
            JObject jStaff = JObject.Parse(json);
            JArray jStaffArr = (JArray)jStaff["Staff"];
            staffs = new List<Staff>();
            for (int i = 0; i < jStaffArr.Count; i++)
            {
                Staff staff = new Staff(jStaffArr[i]);
                staffs.Add(staff);
            }
        }
        List<Staff> staffs { get; set; }
    }
}