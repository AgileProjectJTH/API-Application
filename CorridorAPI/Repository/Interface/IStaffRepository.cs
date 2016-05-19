using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Interface
{
    public interface IStaffRepository
    {
        /// <summary>
        /// returns a staff with staffId = staffId
        /// </summary>
        /// <param name="staffId"></param>
        /// <returns></returns>
        Staff Get(int staffId);

        /// <summary>
        /// Return user with username = username
        /// </summary>
        /// <param name="username"></param>
        /// <returns></returns>
        Repository.Staff Get(string username);

        /// <summary>
        /// returns a list of ALL users
        /// </summary>
        /// <returns>returns a list of ALL users</returns>
        List<Repository.Staff> List();

        /// <summary>
        /// Returns user from corridor with id = corridorId
        /// </summary>
        /// <param name="corridorId"></param>
        /// <returns>Returns user from corridor with id = corridorId</returns>
        List<Repository.Staff> List(int corridorId);

        /// <summary>
        /// Deletes staff with username = username
        /// </summary>
        /// <param name="username"></param>
        void Delete(string username);

        /// <summary>
        /// updates staff with username = updatedStaff.username
        /// </summary>
        /// <param name="updatedStaff"></param>
        void Update(Staff updatedStaff);
    }
}
