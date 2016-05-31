using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Repository.Interface;

namespace Repository.Repositories
{
    public class TaskRepository: ITaskRepository
    {
        /// <summary>
        /// Returns a list of task for a specific staff with the roomNr = roomNr
        /// </summary>
        /// <param name="roomNr">Number of staffs room</param>
        /// <returns>a list of Tasks</returns>
        public List<Task> List(string roomNr)
        {
            List<Task> tasks = new List<Task>();
            using (var db = new CorridorDBEntities())
            {
                Staff staff = db.Staffs.Where(x => x.roomNr == roomNr).First();
                foreach (var sT in db.Staff_Task.Where(x => x.staffId == staff.staffId))
                {
                    tasks.Add(db.Tasks.Where(x => x.taskId == sT.taskId).First());
                }
                
            }

            return tasks;
        }

        /// <summary>
        /// Returns a list of task for a specific staff with the roomNr = roomNr
        /// </summary>
        /// <param name="roomNr">Number of staffs room</param>
        /// <param name="date">date of day to return tasks</param>
        /// <returns>a list of Tasks</returns>
        public List<Task> List(string roomNr , string date)
        {
            List<Task> tasks = new List<Task>();
            using (var db = new CorridorDBEntities())
            {
                Staff staff = db.Staffs.Where(x => x.roomNr == roomNr).First();
                foreach (var sT in db.Staff_Task.Where(x => x.staffId == staff.staffId))
                {
                    Task t = db.Tasks.Where(x => x.taskId == sT.taskId).First();
                    if (t.date == date)
                    {
                        tasks.Add(db.Tasks.Where(x => x.taskId == sT.taskId).First());
                    }                    
                }

            }

            return tasks;
        }

        /// <summary>
        /// Adds a list of tasks
        /// </summary>
        /// <param name="tasks">Tasks to add</param>
        public void Post(List<Task> tasks, string username)
        {
            try
            {
                using (var db = new CorridorDBEntities())
                {
                    Staff staff = db.Staffs.Where(x => x.username == username).First();
                    List<int> idlist = new List<int>();
                    foreach (Task t in tasks)
                    {                        
                        db.Tasks.Add(t);
                        db.SaveChanges();
                        db.Staff_Task.Add(new Staff_Task { staffId = staff.staffId, taskId = t.taskId });
                    }
                    db.SaveChanges();
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        /// <summary>
        /// Adds a task
        /// </summary>
        /// <param name="task">task to add</param>
        public void Post(Task task, string username)
        {
            try
            {
                using (var db = new CorridorDBEntities())
                {
                    Staff staff = db.Staffs.Where(x => x.username == username).First();
                    db.Tasks.Add(task);
                    db.SaveChanges();                    
                    Staff_Task sT = new Staff_Task { staffId = staff.staffId, taskId = task.taskId};
                    db.Staff_Task.Add(sT);
                    db.SaveChanges();
                }
            }
            catch (Exception)
            {
                throw;
            }
            
        }

        /// <summary>
        /// updates tasks to and from time with Id = taskId
        /// </summary>
        /// <param name="updatedTask">task to update</param>
        public void updateTask(Task updatedTask)
        {
            try
            {
                using (var db = new CorridorDBEntities())
                {
                    Task task = db.Tasks.Where(x => x.taskId == updatedTask.taskId).First();
                    task.fromTime = updatedTask.fromTime;
                    task.toTime = updatedTask.toTime;
                    db.SaveChanges();
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Deletes task with Id == taskId
        /// </summary>
        /// <param name="taskId"></param>
        public void Delete(int taskId)
        {
            try
            {
                using (var db = new CorridorDBEntities())
                {
                    Staff_Task sT = db.Staff_Task.Where(x => x.taskId == taskId).First();
                    db.Staff_Task.Attach(sT);
                    db.Staff_Task.Remove(sT);
                    Task task = db.Tasks.Where(x => x.taskId == taskId).First();
                    db.Tasks.Attach(task);
                    db.Tasks.Remove(task);
                    db.SaveChanges();
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

    }
}
    

