using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NoAsthmaClientApp.Source.Models
{
    public class RepairJobModel
    {
        [DataNames("RepairJobID")]
        public int RepairJobID { get; set; }
        [DataNames("PartsID")]
        public int PartsID { get; set; }
        [DataNames("EmpUserName")]
        public string EmpUserName { get; set; }
        [DataNames("PartsQuantity")]
        public int PartsQuantity { get; set; }
    }
}
