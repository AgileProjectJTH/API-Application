using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Interface
{
    public interface IKronox
    {
        /// <summary>
        /// GET
        /// </summary>
        /// <param name="roomNr">number of staffs room ex E2420</param>
        /// <param name="date">Date of witch day to get schedule, yyyy-mm-dd ex 2016-04-25</param>
        /// <returns>Returns a json object with the schedule for the staff with the roomNr and Date (date may be null)</returns>
        string getSchedule(string roomNr, string date);
    }
}
