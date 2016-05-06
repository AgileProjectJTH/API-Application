using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Repositories
{
    public class CorridorRepository
    {
        /// <summary>
        /// adds a new corridor to database with name = corridorName
        /// </summary>
        /// <param name="corridorName"></param>
        public static void Post(string corridorName)
        {
            try
            {
                using (var db = new CorridorDBEntities())
                {
                    Corridor corridor = new Corridor {name = corridorName };
                    db.Corridors.Add(corridor);
                    db.SaveChanges();
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        /// <summary>
        /// Adds user with staffId = username.StaffId to corridor with corridorId = newCorridorId
        /// </summary>
        /// <param name="newCorridorId"></param>
        /// <param name="newStaffId"></param>
        public static void Post(int newCorridorId, string username)
        {
            try
            {
                using (var db = new CorridorDBEntities())
                {
                    Staff staff = db.Staffs.Where(x => x.username == username).First();
                    Staff_Corridor sc = new Staff_Corridor { staffId = staff.staffId, corridorId = newCorridorId };
                    //Staff staffs = db.Staffs.Find(newStaffId);
                    //staffs.Staff_Corridor.Add(sc);
                    db.Staff_Corridor.Add(sc);
                    db.SaveChanges();
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        /// <summary>
        /// Removes user with staffId= newStaffId from corridro with corridorIt newCorridorId
        /// </summary>
        /// <param name="newCorridorId"></param>
        /// <param name="newStaffId"></param>
        public static void Delete(int newCorridorId, int newStaffId)
        {
            try
            {
                using (var db = new CorridorDBEntities())
                {

                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        /// <summary>
        /// removes corridor with corridorId = corridorId
        /// </summary>
        /// <param name="corridorId"></param>
        public static void Delete(int corridorId)
        {
            try
            {
                using (var db = new CorridorDBEntities())
                {
                    Corridor corridor = db.Corridors.Find(corridorId);
                    db.Corridors.Attach(corridor);
                    db.Corridors.Remove(corridor);
                    db.SaveChanges();
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// returns a list of all corridors
        /// </summary>
        /// <returns>returns a list of all corridors</returns>
        public static List<Corridor> List()
        {
            try
            {
                List<Corridor> lCorridor = new List<Corridor>();
                using (var db = new CorridorDBEntities())
                {
                    lCorridor.AddRange(db.Corridors.ToList());
                }
                return lCorridor;
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
