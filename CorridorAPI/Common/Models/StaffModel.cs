﻿using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Models
{
    public class StaffModel
    {
        public StaffModel()
        {
            schedules = new List<Schedule>();
        }

        public StaffModel(JToken JsonStaff)
        {
            schedules = new List<Schedule>();
            signature = (string)JsonStaff["Signature"];
            firstname = (string)JsonStaff["Firstname"];
            lastname = (string)JsonStaff["Lastname"];
            mobile = (string)JsonStaff["Mobile"];
            email = (string)JsonStaff["Mail"];
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

        public string toJson(StaffModel staff)
        {
            return JsonConvert.SerializeObject(staff);
        }

        [Required(ErrorMessage = "StaffId is Required")]
        public int staffId { get; set; }
        [Required(ErrorMessage = "Username is Required")]
        public string username { get; set; }
        [Required(ErrorMessage = "RoomNr is Required")]
        public string roomNr { get; set; }
        [Required(ErrorMessage = "Signature is Required")]
        public string signature { get; set; }
        [Required(ErrorMessage = "Firstname is Required")]
        public string firstname { get; set; }
        [Required(ErrorMessage = "Lastname is Required")]
        public string lastname { get; set; }
        [Required(ErrorMessage = "Mobile is Required")]
        public string mobile { get; set; }
        [Required(ErrorMessage = "Email is Required")]
        public string email { get; set; }
        [Required(ErrorMessage = "IsAdmin is Required")]
        public bool isAdmin { get; set; }
        [Required(ErrorMessage = "Password is Required")]
        public string password { get; set; }

        public List<Schedule> schedules { get; set; }
    }
}

