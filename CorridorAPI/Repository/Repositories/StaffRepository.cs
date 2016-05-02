using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Repositories
{
    public class StaffRepository
    {
        public static Staff Get(int staffId)
        {
            try
            {
                Staff staff = new Staff();
                using (var db = new CorridorDBEntities())
                {
                    staff = db.Staffs.Find(staffId);
                }
                    return staff;
            }
            catch (Exception e)
            {
                throw e;
            }            
        }
    }
}
