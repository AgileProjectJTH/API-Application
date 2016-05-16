using Common.Models;
using Repository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Repositories
{
    public class UserRepository: IUserRepository
    {
        /// <summary>
        /// Adds a new user
        /// </summary>
        /// <param name="user"></param>
        public void Post(Staff staff)
        {
            using (var db = new CorridorDBEntities())
            {
                db.Staffs.Add(new Staff { username = staff.username, firstname = staff.firstname,
                                          lastname = staff.lastname, email = staff.email,
                                          isAdmin = staff.isAdmin, mobile = staff.mobile, roomNr = staff.roomNr });
                db.SaveChanges();
            }
        }

        
    }
}
