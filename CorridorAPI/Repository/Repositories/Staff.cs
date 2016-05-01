using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Repositories
{
    public class Staff
    {
        public int staffId { get; set; }
        public string signature { get; set; }
        public string firstname { get; set; }
        public string lastname { get; set; }
        public string mobile { get; set; }
        public string email { get; set; }
        public bool isAvailable { get; set; }
        public bool isAdmin { get; set; }

        public ICollection<Staff_Corridor> Staff_Corridor { get; set; }
        public ICollection<Staff_Schedule> Staff_Schedule { get; set; }
    }
}
