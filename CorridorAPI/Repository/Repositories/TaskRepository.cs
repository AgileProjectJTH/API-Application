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
            using (var db = new CorridorDB())
            {
                Corridor corObj = new Corridor();


                corObj.name = "Test Corridor 1";

                db.Corridors.Add(corObj);
                db.SaveChanges();
            }
        }
     }
}
    

