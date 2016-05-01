using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Repositories
{
    public class Staff_Schedule
    {
        public int id { get; set; }

        [ForeignKey("Staff")]
        public int staffId { get; set; }
        public Staff Staffs { get; set; }

        [ForeignKey("Task")]
        public int taskId { get; set; }
        public Task Tasks { get; set; }
    }
}
