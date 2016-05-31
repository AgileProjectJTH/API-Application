using Common.Models;
using Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Service.CustomMapper
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
            schedule.taskId = task.taskId;
            schedule.course = task.course;
            schedule.date = task.date;
            schedule.from = task.fromTime;
            schedule.to = task.toTime;
            schedule.moment = task.moment;
            schedule.room = task.room;
            schedule.isAvailable = Convert.ToBoolean(task.isAailable);
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
        /// Maps a List of schedules to a list of Entity Tasks
        /// </summary>
        /// <param name="schedules">List of schedules</param>
        /// <returns>Tasks</returns>
        internal static List<Task> Task(List<Schedule> schedules)
        {
            List<Task> t = new List<Task>();
            foreach (Schedule s in schedules)
            {
                t.Add(Task(s));
            }
            return t;
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
            t.isAailable = schedule.isAvailable;
            if (schedule.taskId != 0)
            {
                t.taskId = schedule.taskId;
            }            
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

        /// <summary>
        /// maps repository staff to common.model staff
        /// </summary>
        /// <param name="staff"></param>
        /// <returns>common.model staff</returns>
        internal static StaffModel StaffModel(Repository.Staff staff)
        {
            StaffModel staffModel = new StaffModel();
            staffModel.staffId = staff.staffId;
            staffModel.username = staff.username;
            staffModel.firstname = staff.firstname;
            staffModel.lastname = staff.lastname;
            staffModel.email = staff.email;
            staffModel.mobile = staff.mobile;
            staffModel.staffId = staff.staffId;
            staffModel.roomNr = staff.roomNr;
            staffModel.isAdmin = Convert.ToBoolean(staff.isAdmin);

            return staffModel;
        }

        /// <summary>
        /// maps usermodel to repository staff
        /// </summary>
        /// <param name="userModel"></param>
        /// <returns>repository staff</returns>
        internal static Staff Staff(UserModel userModel)
        {
            Staff s = new Staff();
            s.email = userModel.email;
            s.firstname = userModel.firstname;
            s.lastname = userModel.lastname;
            s.mobile = userModel.mobile;
            s.roomNr = userModel.roomNr;
            s.username = userModel.UserName;
            s.isAdmin = userModel.isAdmin;
            s.roomNr = userModel.roomNr;
            return s;
        }

        /// <summary>
        /// maps common.model staffmodel to repository staff
        /// </summary>
        /// <param name="staffModel"></param>
        /// <returns>repository staff</returns>
        internal static Staff Staff(StaffModel staffModel)
        {
            Staff s = new Staff();
            s.staffId = staffModel.staffId;
            s.email = staffModel.email;
            s.firstname = staffModel.firstname;
            s.lastname = staffModel.lastname;
            s.mobile = staffModel.mobile;
            s.roomNr = staffModel.roomNr;
            s.username = staffModel.username;
            s.isAdmin = staffModel.isAdmin;
            return s;
        }

        /// <summary>
        /// maps a list of repository staffs to a list of common.model staffmodel
        /// </summary>
        /// <param name="LStaff"></param>
        /// <returns>common.model staffmodel</returns>
        internal static List<StaffModel> StaffModel(List<Repository.Staff> LStaff)
        {
            List<StaffModel> LStaffModel = new List<StaffModel>();
            foreach (Staff s in LStaff)
            {
                LStaffModel.Add(StaffModel(s));
            }
            return LStaffModel;
        }

        /// <summary>
        /// maps a common.model corridormodel to a repository corridor
        /// </summary>
        /// <param name="corridor"></param>
        /// <returns></returns>
        internal static CorridorModel corridorModel(Corridor corridor)
        {
            return new CorridorModel {corridorName = corridor.name, corridorId = corridor.corridorId};
        }

        /// <summary>
        /// maps a common.model corridormodel to a repository corridor
        /// </summary>
        /// <param name="corridor"></param>
        /// <returns></returns>
        internal static Corridor corridor(CorridorModel corridorModel)
        {
            return new Corridor { name = corridorModel.corridorName, corridorId = corridorModel.corridorId, eventInfo = corridorModel.eventInfo};
        }

        /// <summary>
        /// maps a list of repository corridors to a list of common.model corridormodels
        /// </summary>
        /// <param name="lCorridor"></param>
        /// <returns></returns>
        internal static List<CorridorModel> corridorModel(List<Corridor> lCorridor)
        {
            List<CorridorModel> lCorridorModels = new List<CorridorModel>();
            foreach (Corridor c in lCorridor)
            {
                lCorridorModels.Add(corridorModel(c));
            }
            return lCorridorModels;
        }
    }
}