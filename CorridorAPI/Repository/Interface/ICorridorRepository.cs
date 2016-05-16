using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Interface
{
    public interface ICorridorRepository
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
        /// <param name="newStaffId"></param>
        void Post(int newCorridorId, string username);

        /// <summary>
        /// Removes user with staffId= newStaffId from corridro with corridorIt username.corridorId
        /// </summary>
        /// <param name="newCorridorId"></param>
        /// <param name="newStaffId"></param>
        void Delete(int newCorridorId, string username);

        /// <summary>
        /// removes corridor with corridorId = corridorId
        /// </summary>
        /// <param name="corridorId"></param>
        void Delete(int corridorId);

        /// <summary>
        /// updates corridor name with it updatedcorridor.corridorId
        /// </summary>
        /// <param name="updatedCorridor"></param>
        void Update(Corridor updatedCorridor);

        /// <summary>
        /// returns a list of all corridors
        /// </summary>
        /// <returns>returns a list of all corridors</returns>
        List<Corridor> List();
    }
}
