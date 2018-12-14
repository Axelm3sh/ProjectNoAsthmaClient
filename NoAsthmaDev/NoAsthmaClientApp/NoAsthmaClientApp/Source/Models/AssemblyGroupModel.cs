using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NoAsthmaClientApp.Source.Models
{
    public class AssemblyGroupModel
    {
        [DataNames("AssemblyID")]
        public int AssemblyID { get; set; }
        [DataNames("PartsID")]
        public int PartsID { get; set; }
        [DataNames("PartsQuantity")]
        public int PartsQuantity { get; set; }
    }
}
