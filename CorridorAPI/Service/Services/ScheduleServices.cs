using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Repository.Interface;
using Repository.Repositories;
using Common.Models;
using Service.Interface;

namespace Service.Services
{
    public class ScheduleServices : IScheduleServices
    {
        IKronox _kronox;
        ITaskRepository _taskRepository;
        IStaffServices _staffServices;

        public ScheduleServices()
        {
            _staffServices = new StaffServices();
            _taskRepository = new TaskRepository();
            _kronox = new kronox();
        }

        /// <summary>
        /// Returns users avaibility
        /// </summary>
        /// <param name="dateAndTime"> Date need format yyyy-mm-dd hh:mm:ss</param>
        /// <returns></returns>
        public bool Get(string dateAndTime, string username)
        {
            try
            {
                string date = dateAndTime.Substring(0, 10);
                string time = dateAndTime.Substring(11, 5);
                bool isAvailable = true;
                //set false if Lunchtime
                if (Convert.ToInt32(dateAndTime.Substring(12, 1)) == 2)
                {
                    isAvailable = false;
                }
                StaffModel user = _staffServices.Get(username);

                //Check Database schadules
                List<Schedule> schedule = CustomMapper.MapTo.Schedules(_taskRepository.List(user.roomNr));
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
                            isAvailable = schedule[i].isAvailable;
                        }
                    }
                }

                //checks with kronox schedule if current user is available or not   
                StaffModels staffmodels = new StaffModels(_kronox.getSchedule(user.roomNr, date));

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

                return isAvailable;
            }
            catch (Exception)
            {

                throw;
            }
        }

        /// <summary>
        /// Returns a list of Schedules for a specific staff with the roomNr = roomNr
        /// </summary>
        /// <param name="roomNr">Number of staffs room</param>
        /// <returns>a list of Schedules</returns>
        public List<Schedule> List(string roomNr)
        {
            return CustomMapper.MapTo.Schedules(_taskRepository.List(roomNr));
        }

        /// <summary>
        /// Adds a list of tasks
        /// </summary>
        /// <param name="lSchedule">Tasks to add</param>
        /// <param name="username"></param>
        public string Post(ScheduleModel scheduleModel, string username)
        {
            StaffModel user = _staffServices.Get(username);
            //Set toDateAndTime if null
            if (scheduleModel.toDateAndTime == null)
            {
                List<Schedule> lSchedule = CustomMapper.MapTo.Schedules(_taskRepository.List(user.roomNr)).OrderBy(x => x.from).ToList();
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

            //get number of days
            int numberOfDays = (Convert.ToInt32(Math.Floor((DateTime.Parse(scheduleModel.toDateAndTime) - DateTime.Parse(scheduleModel.fromDateAndTime)).TotalDays)) + 1);

            //If more then one day but less then 100
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
                _taskRepository.Post(CustomMapper.MapTo.Task(schedules), username);

                return "";
            }

            //if only one day
            else if (numberOfDays == 1)
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
                _taskRepository.Post(CustomMapper.MapTo.Task(schedule), username);
                return "";
            }

            return "Wrong input of date, numberOfDays may not be negative or larger then 100";
        }

        /// <summary>
        /// Adds a Schedule
        /// </summary>
        /// <param name="schedule"></param>
        /// <param name="username"></param>
        public void Post(Schedule schedule, string username)
        {

        }
    }
}
