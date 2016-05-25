using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using Repository.Repositories;
using Common.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Security.Claims;
using Service.Interface;
using Service.Services;

namespace CorridorAPI.Controllers
{
    public class StaffController : ApiController
    {
        IStaffServices _staffServices;
        IScheduleServices _scheduleServices;

        public StaffController()
        {
            _scheduleServices = new ScheduleServices();
            _staffServices = new StaffServices();
        }
         
        /* GET: Api/Staff
        * Param: Date need format yyyy-mm-dd hh:mm:ss
          Returns: Returns bool True if staff is available */
        [Authorize]
        public IHttpActionResult GET(string dateAndTime, string username)
        {
            var identity = User.Identity as ClaimsIdentity;
            string authenticatedUser = identity.FindFirst("sub").Value;

            DateTime dt;
            if (!DateTime.TryParse(dateAndTime, out dt))
            {
                return BadRequest("wrong format on dateAndTime");
            }

            try
            {
                StaffModel user = _staffServices.Get(authenticatedUser);
                if (user.isAdmin)
                {
                    return Ok(_scheduleServices.Get(dateAndTime, username));
                }
                else
                {
                    return BadRequest("Action not allowed for current user");
                }
            }

            catch (Exception e)
            {

                return BadRequest(e.Message);
            }
        } 

        /* GET: Api/Staff
         * Param: Date need format yyyy-mm-dd hh:mm:ss
           Returns: Returns bool True if staff is available */
        [Authorize]
        public IHttpActionResult GET(string dateAndTime)
        {
            var identity = User.Identity as ClaimsIdentity;
            string authenticatedUser = identity.FindFirst("sub").Value;

            DateTime dt;
            if (!DateTime.TryParse(dateAndTime, out dt))
            {
                return BadRequest("wrong format on dateAndTime");
            }

            try
            {
                return Ok(_scheduleServices.Get(dateAndTime, authenticatedUser));
            }

            catch (Exception e)
            {

                return BadRequest(e.Message);
            }
        }
    }
}