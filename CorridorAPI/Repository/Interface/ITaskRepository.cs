using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Interface
{
    public interface ITaskRepository
    {
        /// <summary>
        /// Returns a list of task for a specific staff with the roomNr = roomNr
        /// </summary>
        /// <param name="roomNr">Number of staffs room</param>
        /// <returns>a list of Tasks</returns>
        List<Task> List(string roomNr);

        /// <summary>
        /// Adds a list of tasks
        /// </summary>
        /// <param name="tasks">Tasks to add</param>
        void Post(List<Task> tasks, string username);

        /// <summary>
        /// Adds a task
        /// </summary>
        /// <param name="task">task to add</param>
        void Post(Task task, string username);
    }
}
