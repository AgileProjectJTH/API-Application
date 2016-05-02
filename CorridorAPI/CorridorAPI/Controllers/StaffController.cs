using CorridorAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using Repository.Repositories;
using Common.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace CorridorAPI.Controllers
{
    public class StaffController : ApiController
    {
       
        /* GET: Api/Staff
         * Param: Date need format yyyy-mm-dd hh:mm:ss
           Returns: Returns bool True if staff is avaible */
        public IHttpActionResult GET(string dateAndTime)
        {
            try
            {

                //TODO Get Loged in user
                //Mockup
                //-----------------------
                string roomNr = "E2404";
                //-----------------------

                string date = dateAndTime.Substring(0, 10);
                string time = dateAndTime.Substring(11, 5);
                bool isAvailable = true;
                List<Schedule> schedule = CustomMapper.MapTo.Schedules(TaskRepository.List(roomNr));
                for (int i = 0; i < schedule.Count; i++)
                {
                    string from = schedule[i].from;
                    string to = schedule[i].to;
                    if (Convert.ToInt32(from.Substring(0, 2)) <= Convert.ToInt32(time.Substring(0, 2)) &&
                        Convert.ToInt32(from.Substring(3, 2)) <= Convert.ToInt32(time.Substring(3, 2)))
                    {
                        if (Convert.ToInt32(to.Substring(0, 2)) >= Convert.ToInt32(time.Substring(0, 2)) &&
                            Convert.ToInt32(to.Substring(3, 2)) >= Convert.ToInt32(time.Substring(3, 2)))
                        {
                            isAvailable = false;
                        }
                    }
                }

                if (isAvailable)
                {
                    //checks with kronox schedule if current user is available or not   
                    StaffModels staffmodels = new StaffModels(kronox.getSchedule("E2420", date));

                    for (int i = 0; i < staffmodels.staffModels.Count; i++)
                    {
                        StaffModel staff = staffmodels.staffModels[i];
                        for (int k = 0; k < staff.schedules.Count; k++)
                        {
                            string from = staff.schedules[k].from;
                            string to = staff.schedules[k].to;
                            if (Convert.ToInt32(from.Substring(0, 2)) <= Convert.ToInt32(time.Substring(0, 2)) &&
                                Convert.ToInt32(from.Substring(3, 2)) <= Convert.ToInt32(time.Substring(3, 2)))
                            {
                                if (Convert.ToInt32(to.Substring(0, 2)) >= Convert.ToInt32(time.Substring(0, 2)) &&
                                    Convert.ToInt32(to.Substring(3, 2)) >= Convert.ToInt32(time.Substring(3, 2)))
                                {
                                    isAvailable = false;
                                }

                            }
                        }
                    }
                }
                return Json(isAvailable);                
            }

            catch (Exception e)
            {

                return BadRequest(e.Message);
            }
        }
    }
}