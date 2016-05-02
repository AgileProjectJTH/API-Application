using Common.Models;
using CorridorAPI.Models;
using Repository.Repositories;
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
         * Param: Date need format yyyy-mm-dd hh:mm:ss
         * Set staff to unavaible */
        public IHttpActionResult POST(string fromDateAndTime, string toDateAndTime)
        {
            try
            {
                //TODO Get Loged in user
                //Mockup
                //-----------------------
                string roomNr = "E2404";
                //-----------------------
                int numberOfDays = (Convert.ToInt32(Math.Floor(( DateTime.Parse(toDateAndTime)  -  DateTime.Parse(fromDateAndTime) ).TotalDays))+1);

                //If more the one day
                if (numberOfDays > 1)
                {
                    List<Schedule> schedules = new List<Schedule>();
                    DateTime startDay = DateTime.Parse(fromDateAndTime);
                    for (int i = 0; i < numberOfDays; i++)
                    {
                        string date = startDay.AddDays(i).ToString().Substring(0, 10);
                        schedules.Add(new Schedule(roomNr, date, "07-00", "17-00"));
                    }
                    schedules.Add(new Schedule(roomNr, toDateAndTime.Substring(0, 10), "07-00", toDateAndTime.Substring(11, 5)));
                    TaskRepository.Post(CustomMapper.MapTo.Task(schedules));
                }
                //if only one day
                else
                {                    
                    string to = toDateAndTime.Substring(11, 5);
                    string fromDate = fromDateAndTime.Substring(0, 10);
                    string from = fromDateAndTime.Substring(11, 5);

                    Schedule schedule = new Schedule(roomNr, fromDate, from, to);
                    TaskRepository.Post(CustomMapper.MapTo.Task(schedule));
                }

                return Ok();
            }
            catch (Exception e)
            {

                return BadRequest(e.Message);
            }
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
            StaffModels staffs = new StaffModels(kronox.getSchedule("E2420", date));

            //TODO
            //get schedule from db and convert it to staffs

            return Json(staffs);
        }
    }
}