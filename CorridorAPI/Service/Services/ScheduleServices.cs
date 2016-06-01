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
                            if (Convert.ToInt32(to.Substring(0, 2)) == Convert.ToInt32(time.Substring(0, 2)) &&
                                Convert.ToInt32(to.Substring(3, 2)) >= Convert.ToInt32(time.Substring(3, 2)))
                            {
                                isAvailable = false;
                            }
                            if (Convert.ToInt32(to.Substring(0, 2)) > Convert.ToInt32(time.Substring(0, 2)))
                            {
                                isAvailable = false;
                            }

                        }
                    }

                }

                //Check Database schadules
                List<Schedule> schedule = CustomMapper.MapTo.Schedules(_taskRepository.List(user.roomNr));
                for (int i = 0; i < schedule.Count; i++)
                {
                    string from = schedule[i].from;
                    string to = schedule[i].to;
                    if (Convert.ToInt32(from.Substring(0, 2)) <= Convert.ToInt32(time.Substring(0, 2)) &&
                        Convert.ToInt32(from.Substring(3, 2)) <= Convert.ToInt32(time.Substring(3, 2)))
                    {
                        if (Convert.ToInt32(to.Substring(0, 2)) == Convert.ToInt32(time.Substring(0, 2)) &&
                            Convert.ToInt32(to.Substring(3, 2)) >= Convert.ToInt32(time.Substring(3, 2)))
                        {
                            isAvailable = schedule[i].isAvailable;
                        }
                        else if (Convert.ToInt32(to.Substring(0, 2)) > Convert.ToInt32(time.Substring(0, 2)))
                        {
                            isAvailable = schedule[i].isAvailable;
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

        //string setToDateAndTime(ScheduleModel scheduleModel, string username)
        public string Post(ScheduleModel scheduleModel, string username)
        {
            StaffModel user = _staffServices.Get(username);
            //Set toDateAndTime if null start ----------------------------------->
            if (scheduleModel.toDateAndTime == null)
            {
                //Check with database
                List<Schedule> lSchedule = CustomMapper.MapTo.Schedules(_taskRepository.List(user.roomNr)).OrderBy(x => x.from).ToList();
                if (lSchedule.Count != 0)
                {
                    foreach (Schedule s in lSchedule)
                    {
                        if (Convert.ToInt32(scheduleModel.fromDateAndTime.Substring(11, 2)) == Convert.ToInt32(s.from.Substring(0, 2)))                         
                        {
                            if (scheduleModel.toDateAndTime == null)
                            {
                                scheduleModel.toDateAndTime = scheduleModel.fromDateAndTime.Substring(0, 11) + s.to + ":59";
                            }

                            else if (Convert.ToInt32(scheduleModel.toDateAndTime.Substring(11, 2)) == Convert.ToInt32(s.from.Substring(0, 2)) 
                                && Convert.ToInt32(scheduleModel.toDateAndTime.Substring(14, 2)) > Convert.ToInt32(s.from.Substring(3, 2)))
                            {
                                scheduleModel.toDateAndTime = scheduleModel.fromDateAndTime.Substring(0, 11) + s.from + ":59";
                            }

                            else if (Convert.ToInt32(scheduleModel.toDateAndTime.Substring(11, 2)) > Convert.ToInt32(s.from.Substring(0, 2)))
                            {
                                scheduleModel.toDateAndTime = scheduleModel.fromDateAndTime.Substring(0, 11) + s.from + ":59";
                            }

                        }

                        else if (Convert.ToInt32(scheduleModel.fromDateAndTime.Substring(11, 2)) < Convert.ToInt32(s.from.Substring(0, 2)))
                        {
                            if (scheduleModel.toDateAndTime == null)
                            {
                                scheduleModel.toDateAndTime = scheduleModel.fromDateAndTime.Substring(0, 11) + s.from + ":59";
                            }

                            else if (Convert.ToInt32(scheduleModel.toDateAndTime.Substring(11, 2)) == Convert.ToInt32(s.from.Substring(0, 2)) 
                                && Convert.ToInt32(scheduleModel.toDateAndTime.Substring(14, 2)) > Convert.ToInt32(s.from.Substring(3, 2)))
                            {
                                scheduleModel.toDateAndTime = scheduleModel.fromDateAndTime.Substring(0, 11) + s.from + ":59";
                            }

                            else if (Convert.ToInt32(scheduleModel.toDateAndTime.Substring(11, 2)) > Convert.ToInt32(s.from.Substring(0, 2)))
                            {
                                scheduleModel.toDateAndTime = scheduleModel.fromDateAndTime.Substring(0, 11) + s.from + ":59";
                            }
                        }
                    }
                }
                //Check with database
                StaffModels staffmodels = new StaffModels(_kronox.getSchedule(user.roomNr, scheduleModel.fromDateAndTime.Substring(0,10)));
                if (staffmodels.staffModels.Count != 0)
                {
                    foreach (StaffModel sM in staffmodels.staffModels)
                    {
                        foreach (Schedule s in sM.schedules)
                        {
                            if (Convert.ToInt32(scheduleModel.fromDateAndTime.Substring(11, 2)) == Convert.ToInt32(s.from.Substring(0, 2)) 
                                && Convert.ToInt32(scheduleModel.fromDateAndTime.Substring(14, 2)) < Convert.ToInt32(s.from.Substring(3, 2)))
                            {
                                if (scheduleModel.toDateAndTime == null)
                                {
                                    scheduleModel.toDateAndTime = scheduleModel.fromDateAndTime.Substring(0, 11) + s.from + ":59";
                                }

                                else if (Convert.ToInt32(scheduleModel.toDateAndTime.Substring(11, 2)) == Convert.ToInt32(s.from.Substring(0, 2)) 
                                    && Convert.ToInt32(scheduleModel.toDateAndTime.Substring(14, 2)) > Convert.ToInt32(s.from.Substring(3, 2)))
                                {
                                    scheduleModel.toDateAndTime = scheduleModel.fromDateAndTime.Substring(0, 11) + s.from + ":59";
                                }

                                else if (Convert.ToInt32(scheduleModel.toDateAndTime.Substring(11, 2)) > Convert.ToInt32(s.from.Substring(0, 2)))
                                {
                                    scheduleModel.toDateAndTime = scheduleModel.fromDateAndTime.Substring(0, 11) + s.from + ":59";
                                }

                            }

                            else if (Convert.ToInt32(scheduleModel.fromDateAndTime.Substring(11, 2)) < Convert.ToInt32(s.from.Substring(0, 2)))
                            {
                                if (scheduleModel.toDateAndTime == null)
                                {
                                    scheduleModel.toDateAndTime = scheduleModel.fromDateAndTime.Substring(0, 11) + s.from + ":59";
                                }

                                else if (Convert.ToInt32(scheduleModel.toDateAndTime.Substring(11, 2)) == Convert.ToInt32(s.from.Substring(0, 2)) 
                                    && Convert.ToInt32(scheduleModel.toDateAndTime.Substring(14, 2)) > Convert.ToInt32(s.from.Substring(3, 2)))
                                {
                                    scheduleModel.toDateAndTime = scheduleModel.fromDateAndTime.Substring(0, 11) + s.from + ":59";
                                }

                                else if (Convert.ToInt32(scheduleModel.toDateAndTime.Substring(11, 2)) > Convert.ToInt32(s.from.Substring(0, 2)))
                                {
                                    scheduleModel.toDateAndTime = scheduleModel.fromDateAndTime.Substring(0, 11) + s.from + ":59";
                                }
                            }
                        }
                    }
                }

                else if(scheduleModel.toDateAndTime == null)
                {
                    scheduleModel.toDateAndTime = scheduleModel.fromDateAndTime.Substring(0, 11) + "23:59:59";
                }
            }
            //Set toDateAndTime if null end ----------------------------------->

            //Get number of days
            int numberOfDays = (Convert.ToInt32(Math.Floor((DateTime.Parse(scheduleModel.toDateAndTime) - DateTime.Parse(scheduleModel.fromDateAndTime)).TotalDays)) + 1);

            //If more then one day but less then 100
            if (numberOfDays > 1 && numberOfDays < 100)
            {
                List<Schedule> schedules = new List<Schedule>();
                DateTime startDay = DateTime.Parse(scheduleModel.fromDateAndTime);
                for (int i = 0; i < numberOfDays-1; i++)
                {
                    string date = startDay.AddDays(i).ToString().Substring(0, 10);
                    schedules.Add(new Schedule(scheduleModel.roomNr, date, "07:00", "23:59", scheduleModel.available));
                }

                foreach (Schedule s in schedules)
                {
                    updateSchedule(s, user);
                }

                schedules.Add(new Schedule(scheduleModel.roomNr, scheduleModel.toDateAndTime.Substring(0, 10), "07:00", scheduleModel.toDateAndTime.Substring(11, 5), scheduleModel.available));
                _taskRepository.Post(CustomMapper.MapTo.Task(schedules), username);

                return "";
            }

            //if only one day
            else if (numberOfDays == 1)
            {
                string to = scheduleModel.toDateAndTime.Substring(11, 5);
                string fromDate = scheduleModel.fromDateAndTime.Substring(0, 10);
                string from = scheduleModel.fromDateAndTime.Substring(11, 5);

                Schedule schedule = new Schedule(scheduleModel.roomNr, fromDate, from, to, scheduleModel.available);
                if (scheduleModel.scheduleInfo != null)
                {
                    schedule.moment = scheduleModel.scheduleInfo;
                }
                if (scheduleModel.course != null)
                {
                    schedule.course = scheduleModel.course;
                }
                updateSchedule(schedule, user);
                _taskRepository.Post(CustomMapper.MapTo.Task(schedule), username);                
                return "";
            }

            return "Wrong input of date, numberOfDays may not be negative or larger then 100";

        }

        /// <summary>
        /// makes sure there is no overlapping schedules(Tasks) in the database
        /// </summary>
        /// <param name="schedule"></param>
        /// <param name="user"></param>
        void updateSchedule(Schedule schedule, StaffModel user)
        {
            int sFrom = Convert.ToInt32(schedule.from.Substring(0, 2) + schedule.from.Substring(3, 2));
            int sTo = Convert.ToInt32(schedule.to.Substring(0, 2) + schedule.to.Substring(3, 2));

            List<Schedule> lSchedule = CustomMapper.MapTo.Schedules(_taskRepository.List(user.roomNr, schedule.date)).OrderBy(x => x.from).ToList();
            if (lSchedule.Count != 0)
            {
                foreach (Schedule s in lSchedule)
                {
                    int from = Convert.ToInt32(s.from.Substring(0, 2) + s.from.Substring(3, 2));
                    int to = Convert.ToInt32(s.to.Substring(0, 2) + s.to.Substring(3, 2));

                    //if schedule s is to long and needs to be shortend or removed
                    if (from < sFrom && to >= sFrom)
                    {
                        if (to <= sTo)
                        {
                            if (to - from > 1)
                            {
                                if (schedule.from.Substring(3, 2) != "00")
                                {
                                    string newFrom = (100 + Convert.ToInt32(schedule.from.Substring(3, 2)) - 1).ToString();
                                    s.to = schedule.from.Substring(0, 3) + newFrom.Substring(1,2);
                                    _taskRepository.updateTask(CustomMapper.MapTo.Task(s));
                                    return;
                                }
                                else
                                {
                                    string newFrom = (100 + Convert.ToInt32(schedule.from.Substring(0, 2)) - 1).ToString() + ":59";
                                    s.to = newFrom.Substring(1,5);
                                    _taskRepository.updateTask(CustomMapper.MapTo.Task(s));
                                    return;
                                }
                            }
                            else
                            {
                                _taskRepository.Delete(s.taskId);
                                return;
                            }
                        }

                        if (to > sTo)
                        {
                            if (sFrom - from > 1)
                            {
                                if (schedule.from.Substring(3, 2) != "00")
                                {
                                    if (schedule.to.Substring(3, 2) != "59")
                                    {
                                        string newFrom = (schedule.to.Substring(0, 3) + (100 + Convert.ToInt32(schedule.to.Substring(3, 2)) + 1).ToString());
                                        newSchedule(s, user.username, schedule.to.Substring(0, 3) + newFrom.Substring(1,2));
                                        string tempTo = (100 + Convert.ToInt32(schedule.from.Substring(3, 2)) - 1).ToString();
                                        s.to = schedule.from.Substring(0, 3) + tempTo.Substring(1,2);
                                        _taskRepository.updateTask(CustomMapper.MapTo.Task(s));
                                        return;
                                    }

                                    else
                                    {
                                        string newFrom = (100 + Convert.ToInt32(schedule.to.Substring(0, 2)) + 1).ToString() + ":00";
                                        newSchedule(s, user.username, newFrom.Substring(1,4));
                                        string tempTo = (100 + Convert.ToInt32(schedule.from.Substring(3, 2)) - 1).ToString();
                                        s.to = schedule.from.Substring(0, 3) + tempTo.Substring(1, 2);
                                        _taskRepository.updateTask(CustomMapper.MapTo.Task(s));
                                        return;
                                    }
                                }
                                else
                                {
                                    if (schedule.to.Substring(3, 2) != "59")
                                    {
                                        string newFrom = (100 + Convert.ToInt32(schedule.to.Substring(3, 2)) + 1).ToString();
                                        newSchedule(s, user.username, (schedule.to.Substring(0, 3) + newFrom.Substring(1,2)));
                                        string tempTo = (100 + Convert.ToInt32(schedule.from.Substring(0, 2)) - 1).ToString() + ":59";
                                        s.to = tempTo.Substring(1, 5);
                                        _taskRepository.updateTask(CustomMapper.MapTo.Task(s));
                                        return;
                                    }

                                    else
                                    {
                                        string newFrom = (100 + Convert.ToInt32(schedule.to.Substring(0, 2)) + 1).ToString() + ":00";
                                        newSchedule(s, user.username, newFrom.Substring(1,5));
                                        string tempTo = (100 + Convert.ToInt32(schedule.from.Substring(0, 2)) - 1).ToString() + ":59";
                                        s.to = tempTo.Substring(1, 5);
                                        _taskRepository.updateTask(CustomMapper.MapTo.Task(s));
                                        return;
                                    }
                                }
                            }
                            else
                            {
                                if (schedule.to.Substring(3, 2) != "59")
                                {
                                    string newFrom = (100 + Convert.ToInt32(schedule.to.Substring(3, 2) + 1).ToString());
                                    newSchedule(s, user.username, (schedule.to.Substring(0, 3) + newFrom.Substring(1,2)));
                                    _taskRepository.Delete(s.taskId);
                                    return;
                                }

                                else
                                {
                                    string newFrom = (100 + Convert.ToInt32(schedule.to.Substring(0, 2)) + 1).ToString() + ":00";
                                    newSchedule(s, user.username, newFrom.Substring(1,5));
                                    _taskRepository.Delete(s.taskId);
                                    return;
                                }
                            }
                        }
                    }


                    else if (from >= sFrom && from <= sTo)
                    {
                        if (to - sTo > 1)
                        {
                            if (schedule.to.Substring(3, 2) != "59")
                            {
                                string newFrom = (100 + Convert.ToInt32(schedule.to.Substring(3, 2)) + 1).ToString();
                                s.from = schedule.to.Substring(0, 3) + newFrom.Substring(1,2);
                                _taskRepository.updateTask(CustomMapper.MapTo.Task(s));
                                return;
                            }
                            else
                            {
                                string newFrom = (100 + Convert.ToInt32(schedule.to.Substring(0, 2)) + 1).ToString() + ":00";
                                s.to = newFrom.Substring(1,5);
                                _taskRepository.updateTask(CustomMapper.MapTo.Task(s));
                                return;
                            }
                        }
                        else
                        {
                            _taskRepository.Delete(s.taskId);
                            return;
                        }
                    }
                }
            }
        }

        /// <summary>
        /// creates a new schedule
        /// </summary>
        void newSchedule(Schedule s, string username, string newFrom)
        {
            Schedule newSchedule = new Schedule(s.room, s.date, newFrom, s.to, s.isAvailable);
            if (s.moment != null)
            {
                newSchedule.moment = s.moment;
            }
            if (s.course != null)
            {
                newSchedule.course = s.course;
            }
            _taskRepository.Post(CustomMapper.MapTo.Task(newSchedule), username);

        }

        /// <summary>
        /// Extra functionen needed to make Automatic avaibility work
        /// </summary>
        /// <param name="scheduleModel"></param>
        /// <param name="user"></param>
        private void setAutomaticAvaibility(ScheduleModel scheduleModel, StaffModel user)
        {
            string tempToDate = scheduleModel.toDateAndTime;
            scheduleModel.fromDateAndTime = scheduleModel.toDateAndTime;

            if (scheduleModel.available == true)
            {
                scheduleModel.available = false;
            }
            else
            {
                scheduleModel.available = true;
            }

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
                scheduleModel.toDateAndTime = scheduleModel.fromDateAndTime.Substring(0, 11) + "23:59:59";
            }

            string to = scheduleModel.toDateAndTime.Substring(11, 5);
            string fromDate = scheduleModel.fromDateAndTime.Substring(0, 10);
            string from = scheduleModel.fromDateAndTime.Substring(11, 5);

            Schedule schedule = new Schedule(scheduleModel.roomNr, fromDate, from, to, scheduleModel.available);
            if (scheduleModel.scheduleInfo != null)
            {
                schedule.moment = scheduleModel.scheduleInfo;
            }
            if (scheduleModel.course != null)
            {
                schedule.course = scheduleModel.course;
            }
            _taskRepository.Post(CustomMapper.MapTo.Task(schedule), user.username);

        }
       
    }
}
