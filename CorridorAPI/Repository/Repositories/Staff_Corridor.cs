using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Repositories
{
    public class Staff_Corridor
    {
        public int id { get; set; }

        [ForeignKey("Staff")]
        public int staffId { get; set; }
        public Staff Staffs { get; set; }

        [ForeignKey("Corridor")]
        public int corridorId { get; set; }
        public Corridor Corridors { get; set; }
    }
}
