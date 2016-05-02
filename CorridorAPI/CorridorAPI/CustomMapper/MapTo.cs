using Common.Models;
using Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CorridorAPI.CustomMapper
{
    public class MapTo
    {
        /// <summary>
        /// Mapps a db entity Task to common.model Schedule
        /// </summary>
        /// <param name="task"></param>
        /// <returns>Schedule</returns>
        internal static Schedule Schedule(Task task)
        {
            Schedule schedule = new Schedule();
            schedule.course = task.course;
            schedule.date = task.date;
            schedule.from = task.fromTime;
            schedule.to = task.toTime;
            schedule.moment = task.moment;
            schedule.room = task.room;
            return schedule;
        }
        /// <summary>
        /// Mapps a List of db entity Task to a List of common.model Schedules
        /// </summary>
        /// <param name="task">List of Tasks</param>
        /// <returns>Schedules</returns>
        internal static List<Schedule> Schedules(List<Task> tasks)
        {
            List<Schedule> schedules = new List<Schedule>();
            foreach (Task t in tasks)
            {
                schedules.Add(Schedule(t));
            }
            return schedules;
        }

        /// <summary>
        /// Mapps a schedule to a entity Task
        /// </summary>
        /// <param name="schedule"></param>
        /// <returns>a Task</returns>
        internal static Task Task(Schedule schedule)
        {
            Task t = new Task();
            t.date = schedule.date;
            t.fromTime = schedule.from;
            t.toTime = schedule.to;
            t.room = schedule.room;
            if (schedule.moment != null)
            {
                t.moment = schedule.moment;
            }
            if (schedule.course != null)
            {
                t.course = schedule.course;
            }
            return t;
            
        }
    }
}