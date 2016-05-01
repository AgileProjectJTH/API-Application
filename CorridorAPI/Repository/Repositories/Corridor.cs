using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Repositories
{
    public class Corridor
    {
        public int corridorId { get; set; }
        public string name { get; set; }

        public ICollection<Staff_Corridor> Staff_Corridor { get; set; }
    }
}
