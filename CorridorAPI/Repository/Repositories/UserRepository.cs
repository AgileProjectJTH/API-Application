using Common.Models;
using CorridorAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Repositories
{
    public class UserRepository
    {
        /// <summary>
        /// Adds a new user
        /// </summary>
        /// <param name="user"></param>
        public static void Post(UserModel user)
        {
            using (var db = new CorridorDBEntities())
            {
                db.Staffs.Add(new Staff { firstname = user.UserName, lastname = user.lastname, email = user.email, isAdmin = user.isAdmin, mobile = user.mobile });
                db.SaveChanges();
            }
        }

        /// <summary>
        /// Return user with username = username
        /// </summary>
        /// <param name="username"></param>
        /// <returns></returns>
        public static Repository.Staff Get(string username)
        {
            try
            {
                Repository.Staff staff = new Staff();
                using (var db = new CorridorDBEntities())
                {
                    staff = db.Staffs.Where(x => x.username == username).First();
                }
                return staff;
            }
            catch (Exception e)
            {

                throw e;
            }
        }

        /// <summary>
        /// returns a list of ALL users
        /// </summary>
        /// <returns>returns a list of ALL users</returns>
        public static List<Repository.Staff> List()
        {
            try
            {
                List<Repository.Staff> LStaff = new List<Staff>();
                using (var db = new CorridorDBEntities())
                {
                    LStaff.AddRange(db.Staffs.ToList());
                }
                    return LStaff; 
            }
            catch (Exception e)
            {

                throw e;
            }
        }

        /// <summary>
        /// Returns user from corridor with id = corridorId
        /// </summary>
        /// <param name="corridorId"></param>
        /// <returns>Returns user from corridor with id = corridorId</returns>
        public static List<Repository.Staff> List(int corridorId)
        {
            try
            {
                List<Repository.Staff> LStaff = new List<Staff>();
                using (var db = new CorridorDBEntities())
                {
                    List<Staff_Corridor> LStaffCorridor = db.Staff_Corridor.Where(x => x.corridorId == corridorId).ToList();
                    foreach (Staff_Corridor sc in LStaffCorridor)
                    {
                        LStaff.Add(db.Staffs.Where(x => x.staffId == sc.staffId).First());
                    }
                }
                    return LStaff;
            }
            catch (Exception e)
            {

                throw e;
            }
        }
    }
}
