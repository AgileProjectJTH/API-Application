using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Repositories
{
    public class TaskRepository
    {
        public static void test()
        {
            using (var db = new CorridorDBEntities())
            {
                Corridor corObj = new Corridor();


                corObj.name = "Test Corridor 11";

                db.Corridors.Add(corObj);
                db.SaveChanges();
            }
        }
     }
}
    

