using CorridorAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;

namespace CorridorAPI.Controllers
{
    public class StaffController : ApiController
    {
        /* POST: Api/Staff
           Set staff to unavaible */
        public ActionResult POST(string dateAndTime)
        {
            return null;
        }
        /* GET: Api/Staff
         * Param: Date need format yyyy-mm-dd-hh-mm-ss
           Returns: Returns bool True if staff is avaible */
        public ActionResult GET(string dateAndTime)
        {
            using (var db = new CorridorDB())
            {
                Task task = db.st
            }
                //checks with kronox schedule if current user is available or not
            string date = dateAndTime.Substring(0, 10);
            string time = dateAndTime.Substring(11, 16);
            bool isAvailable = false;
            Staffs staffs = new Staffs(kronox.getSchedule("E2420", date));

            for (int i = 0; i < staffs.staffs.Count ; i++)
            {
                Staff staff = staffs.staffs[i];
                for (int k = 0; k < staff.schedules.Count; i++)
                {
                    string from = staff.schedules[k].from;
                    string to = staff.schedules[k].to;
                    if (Convert.ToInt32(from.Substring(0, 2)) <= Convert.ToInt32(time.Substring(0, 2))&& 
                        Convert.ToInt32(from.Substring(3, 5)) <= Convert.ToInt32(time.Substring(3, 5)))
                    {
                        if (Convert.ToInt32(to.Substring(0, 2)) >= Convert.ToInt32(time.Substring(0, 2)) &&
                            Convert.ToInt32(to.Substring(3, 5)) >= Convert.ToInt32(time.Substring(3, 5)))
                        {
                            isAvailable = true;
                        }

                    }
                }
            }

            return null;
        }
    }
}