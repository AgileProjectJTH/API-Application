using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Models
{
    public class StaffModels
    {
        public StaffModels() { }

        /// <summary>
        /// Konverts Json to List of Staffs
        /// </summary>
        /// <param name="json"></param>
        public StaffModels(string json)
        {
            JObject jStaff = JObject.Parse(json);
            JArray jStaffArr = (JArray)jStaff["Staff"];
            staffModels = new List<StaffModel>();
            for (int i = 0; i < jStaffArr.Count; i++)
            {
                StaffModel staff = new StaffModel(jStaffArr[i]);
                staffModels.Add(staff);
            }
        }

        public List<StaffModel> staffModels { get; set; }
    }
}
