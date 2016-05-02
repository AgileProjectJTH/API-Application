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
        /* POST: Api/Staff
           Set staff to unavaible */
        public IHttpActionResult POST(string dateAndTime)
        {
            string date = dateAndTime.Substring(0, 10);
            string time = dateAndTime.Substring(11, 5);
            string from = time.Substring(0, 2);
            string to = time.Substring(3, 2);
            return null;
        }
        /* GET: Api/Staff
         * Param: Date need format yyyy-mm-dd-hh-mm-ss
           Returns: Returns bool True if staff is avaible */
        public IHttpActionResult GET(string dateAndTime)
        {
            TaskRepository.test();
            string date = dateAndTime.Substring(0, 10);
            string time = dateAndTime.Substring(11, 5);

            //TODO 
            //Get Db info

            bool isAvailable = true;
            //checks with kronox schedule if current user is available or not   
            StaffModels staffmodels = new StaffModels(kronox.getSchedule("E2420", date));

            for (int i = 0; i < staffmodels.staffModels.Count ; i++)
            {
                StaffModel staff = staffmodels.staffModels[i];
                for (int k = 0; k < staff.schedules.Count; k++)
                {
                    string from = staff.schedules[k].from;
                    string to = staff.schedules[k].to;
                    if (Convert.ToInt32(from.Substring(0, 2)) <= Convert.ToInt32(time.Substring(0, 2))&& 
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

            return Json(isAvailable);
        }
    }
}