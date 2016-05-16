using Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Interface
{
    public interface IScheduleServices
    {
        /// <summary>
        /// Returns a list of Schedules for a specific staff with the roomNr = roomNr
        /// </summary>
        /// <param name="roomNr">Number of staffs room</param>
        /// <returns>a list of Schedules</returns>
        List<Schedule> List(string roomNr);
    }
}
