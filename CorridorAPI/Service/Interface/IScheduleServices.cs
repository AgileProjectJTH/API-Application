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

        /// <summary>
        /// Adds a list of tasks
        /// </summary>
        /// <param name="lSchedule">Tasks to add</param>
        /// <param name="username"></param>
        void Post(List<Schedule> lSchedule, string username);

        /// <summary>
        /// Adds a Schedule
        /// </summary>
        /// <param name="schedule"></param>
        /// <param name="username"></param>
        void Post(Schedule schedule, string username);
    }
}
