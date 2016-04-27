using CorridorAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;

namespace CorridorAPI.Controllers
{
    public class ScheduleController : ApiController
    {
        /* POST: Api/Schedule
           Set Unavaible for current time span */
        public IHttpActionResult POST()
        {
            return null;
        }

        /* DELETE: Api/Schedule
           Set avaiable for the current time span */
        public IHttpActionResult DELETE(string dateAndTime)
        {
            return null;
        }

        /* GET: Api/Schedule
           Returns: Returns schedule of given date for current user */
        public IHttpActionResult GET(string dateAndTime)
        {
            string date = dateAndTime.Substring(0, 10);
            string time = dateAndTime.Substring(11, 5);
            Staffs staffs = new Staffs(kronox.getSchedule("E2420", date));

            //TODO
            //get schedule from db and convert it to staffs

            return Json(staffs);
        }
    }
}