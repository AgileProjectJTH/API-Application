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
        /// Returns a list of task for a specific staff with the roomNr = roomNr
        /// </summary>
        /// <param name="roomNr">Number of staffs room</param>
        /// <param name="date">date of day to return tasks</param>
        /// <returns>a list of Tasks</returns>
        List<Task> List(string roomNr, string date);

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

        /// <summary>
        /// updates tasks to and from time with Id = taskId
        /// </summary>
        /// <param name="updatedTask">task to update</param>
        void updateTask(Task updatedTask);

        /// <summary>
        /// Deletes task with Id == taskId
        /// </summary>
        /// <param name="taskId"></param>
        void Delete(int taskId);
    }
}
