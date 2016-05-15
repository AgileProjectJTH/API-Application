using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Models
{
    public class CorridorModel
    {
        public int corridorId { get; set; }
        [Required]
        public string corridorName { get; set; }
        public string eventInfo { get; set; }
    }
}
