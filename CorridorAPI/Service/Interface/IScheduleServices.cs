﻿using Common.Models;
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
        /// Returns users avaibility
        /// </summary>
        /// <param name="dateAndTime"></param>
        /// <returns></returns>
        bool Get(string dateAndTime, string username);

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
        string Post(ScheduleModel scheduleModel, string username);

       
    }
}
