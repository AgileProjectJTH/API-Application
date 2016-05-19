using Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Interface
{
    public interface ICorridorServices
    {
        /// <summary>
        /// adds a new corridor to database with name = corridorName
        /// </summary>
        /// <param name="corridorName"></param>
        void Post(string corridorName);

        /// <summary>
        /// Adds user with staffId = username.StaffId to corridor with corridorId = newCorridorId
        /// </summary>
        /// <param name="newCorridorId"></param>
        /// <param name="username"></param>
        void Post(int newCorridorId, string username);

        /// <summary>
        /// Removes user with staffId= newStaffId from corridro with corridorIt username.corridorId
        /// </summary>
        /// <param name="newCorridorId"></param>
        /// <param name="username"></param>
        void Delete(int newCorridorId, string username);

        /// <summary>
        /// removes corridor with corridorId = corridorId
        /// </summary>
        /// <param name="corridorId"></param>
        void Delete(int corridorId);

        /// <summary>
        /// updates CorridorModel name with it updatedcorridor.corridorId
        /// </summary>
        /// <param name="updatedCorridor"></param>
        void Update(CorridorModel updatedCorridor);

        /// <summary>
        /// returns a list of all CorridorModels
        /// </summary>
        /// <returns>returns a list of all CorridorModels</returns>
        List<CorridorModel> List();
    }
}
