using Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Interface
{
    public interface IStaffServices
    {
        /// <summary>
        /// returns a StaffModel with staffId = staffId
        /// </summary>
        /// <param name="staffId"></param>
        /// <returns></returns>
        StaffModel Get(int staffId);

        /// <summary>
        /// Return StaffModel with username = username
        /// </summary>
        /// <param name="username"></param>
        /// <returns></returns>
        StaffModel Get(string username);

        /// <summary>
        /// returns a list of ALL StaffModels
        /// </summary>
        /// <returns></returns>
        List<StaffModel> List();

        /// <summary>
        /// Returns StaffModel from corridor with id = corridorId
        /// </summary>
        /// <param name="corridorId"></param>
        /// <returns>Returns StaffModel from corridor with id = corridorId</returns>
        List<StaffModel> List(int corridorId);

        /// <summary>
        /// Deletes staff with username = username
        /// </summary>
        /// <param name="username"></param>
        void Delete(string username);

        /// <summary>
        /// updates StaffModel with username = updatedStaff.username
        /// </summary>
        /// <param name="updatedStaff"></param>
        void Update(StaffModel updatedStaff);
    }
}
