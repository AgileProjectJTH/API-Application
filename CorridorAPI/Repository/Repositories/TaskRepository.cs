using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Repositories
{
    public class TaskRepository
    {
        /// <summary>
        /// Returns a list of task for a specific staff with the roomNr
        /// </summary>
        /// <param name="roomNr">Number of staffs room</param>
        /// <returns>a list of Tasks</returns>
        public static List<Task> List(string roomNr)
        {
            List<Task> tasks;
            using (var db = new CorridorDBEntities())
            {
                tasks = db.Tasks.Where(x => x.room == roomNr).ToList();
            }

            return tasks;
        }

        /// <summary>
        /// Adds a list of tasks
        /// </summary>
        /// <param name="tasks">Tasks to add</param>
        public static void Post(List<Task> tasks)
        {
            try
            {
                using (var db = new CorridorDBEntities())
                {
                    db.Tasks.AddRange(tasks);
                    db.SaveChanges();
                }
            }
            catch (Exception e)
            {

                throw e;
            }
        }

        /// <summary>
        /// Adds a task
        /// </summary>
        /// <param name="task">task to add</param>
        public static void Post(Task task)
        {
            try
            {
                using (var db = new CorridorDBEntities())
                {
                    db.Tasks.Add(task);
                    db.SaveChanges();
                }
            }
            catch (Exception e)
            {
                throw e;
            }
            
        }

    }
}
    

