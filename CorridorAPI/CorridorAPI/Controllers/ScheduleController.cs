using Common.Models;
using CorridorAPI.Models;
using Repository.Repositories;
using Service.Interface;
using Service.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Web;
using System.Web.Http;


namespace CorridorAPI.Controllers
{
    public class ScheduleController : ApiController
    {

        IStaffServices _staffServices;
        public ScheduleController()
        {
            _staffServices = new StaffServices();
        }

        /* POST: Api/Schedule            
         * Param: Date need format yyyy-mm-dd hh:mm:ss
         * Set staff to unavaible */
        [Authorize]
        public IHttpActionResult POST(ScheduleModel scheduleModel)
        {
            var identity = User.Identity as ClaimsIdentity;
            string authenticatedUser = identity.FindFirst("sub").Value;

            try
            {
                StaffModel user = _staffServices.Get(authenticatedUser);
                    

                if (scheduleModel.toDateAndTime == null)
                {
                    List<Schedule> lSchedule = CustomMapper.MapTo.Schedules(Repository.Repositories.TaskRepository.List(user.roomNr)).OrderBy(x=> x.from).ToList();
                    if (lSchedule.Count != 0)
                    {
                        foreach (Schedule s in lSchedule)
                        {
                            if (Convert.ToInt32(scheduleModel.fromDateAndTime.Substring(12, 2)) < Convert.ToInt32(s.from.Substring(12, 2)))
                            {
                                if (scheduleModel.toDateAndTime == null)
                                {
                                    scheduleModel.toDateAndTime = s.from;
                                }
                                else if (Convert.ToInt32(scheduleModel.toDateAndTime.Substring(12, 2)) > Convert.ToInt32(s.from.Substring(12, 2)))
                                {
                                    scheduleModel.toDateAndTime = s.from;
                                }
                            }
                        }
                    }
                    else
                    {
                        scheduleModel.toDateAndTime = scheduleModel.fromDateAndTime.Substring(0, 11) + "17:00:00";
                    }
                }
                int numberOfDays = (Convert.ToInt32(Math.Floor(( DateTime.Parse(scheduleModel.toDateAndTime)  -  DateTime.Parse(scheduleModel.fromDateAndTime) ).TotalDays))+1);

                //If more the one day but not more then 100
                if (numberOfDays > 1 && numberOfDays < 100)
                {
                    List<Schedule> schedules = new List<Schedule>();
                    DateTime startDay = DateTime.Parse(scheduleModel.fromDateAndTime);
                    for (int i = 0; i < numberOfDays; i++)
                    {
                        string date = startDay.AddDays(i).ToString().Substring(0, 10);
                        schedules.Add(new Schedule(scheduleModel.roomNr, date, "07-00", "17-00"));
                    }
                    schedules.Add(new Schedule(scheduleModel.roomNr, scheduleModel.toDateAndTime.Substring(0, 10), "07-00", scheduleModel.toDateAndTime.Substring(11, 5)));
                    TaskRepository.Post(CustomMapper.MapTo.Task(schedules), authenticatedUser);

                    return Ok();
                }

                //if only one day
                else if(numberOfDays == 1)
                {                    
                    string to = scheduleModel.toDateAndTime.Substring(11, 5);
                    string fromDate = scheduleModel.fromDateAndTime.Substring(0, 10);
                    string from = scheduleModel.fromDateAndTime.Substring(11, 5);

                    Schedule schedule = new Schedule(scheduleModel.roomNr, fromDate, from, to);
                    if (scheduleModel.scheduleInfo != null)
                    {
                        schedule.moment = scheduleModel.scheduleInfo;
                    }
                    if (scheduleModel.course != null)
                    {
                        schedule.course = scheduleModel.course;
                    }
                    TaskRepository.Post(CustomMapper.MapTo.Task(schedule), authenticatedUser);
                    return Ok();
                }

                return BadRequest("Wrong input of date, numberOfDays may not be negative or larger then 100");

            }
            catch (Exception e)
            {

                return BadRequest(e.Message);
            }
        }

        /* DELETE: Api/Schedule
         * Param: Date need format yyyy-mm-dd hh:mm:ss
         * Delete Task that start with dateAndTime and ends with toDateAndTime */
        [Authorize]
        public IHttpActionResult DELETE(string dateAndTime, string toDateAndTime)
        {

            //DO WE NEED IT???
            return null;
        }

        /* GET: Api/Schedule
           Returns: Returns schedule of given date for current user */
        [Authorize]
        public IHttpActionResult GET(string dateAndTime)
        {
            var identity = User.Identity as ClaimsIdentity;
            string authenticatedUser = identity.FindFirst("sub").Value;

            DateTime dt;
            if (DateTime.TryParse(dateAndTime, out dt))
            {
                return BadRequest("wrong format on dateAndTime");
            }

            try
            {
                StaffModel user = _staffServices.Get(authenticatedUser);               
                string date = dateAndTime.Substring(0, 10);
                StaffModels staffs = new StaffModels(kronox.getSchedule(user.roomNr, date));
                StaffModel staff = _staffServices.Get(user.staffId); 
                staff.schedules.AddRange(CustomMapper.MapTo.Schedules(TaskRepository.List(user.roomNr)));
                staffs.staffModels.Add(staff);
                return Json(staffs);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}