using Common.Models;
using Repository.Interface;
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
        IKronox _kronox;
        IStaffServices _staffServices;
        IScheduleServices _scheduleServices;

        public ScheduleController()
        {
            _kronox = new kronox();
            _staffServices = new StaffServices();
            _scheduleServices = new ScheduleServices();
        }

        /* POST: Api/Schedule            
         * Param: Date need format yyyy-mm-dd hh:mm:ss
         * Set staff to unavaible */
        [Authorize]
        public IHttpActionResult POST(ScheduleModel scheduleModel)
        {
            var identity = User.Identity as ClaimsIdentity;
            string authenticatedUser = identity.FindFirst("sub").Value;

            if (!ModelState.IsValid)
            {
                return BadRequest("Wrong input of fromDateAndTime");
            }

            try
            {
                string response = _scheduleServices.Post(scheduleModel, authenticatedUser);
                if (response == "")
                {
                    return Ok();
                }
                else
                {
                    return BadRequest(response);
                }
               
                


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
                StaffModels staffs = new StaffModels(_kronox.getSchedule(user.roomNr, date));
                StaffModel staff = _staffServices.Get(user.staffId);
                staff.schedules.AddRange(_scheduleServices.List(user.roomNr));
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