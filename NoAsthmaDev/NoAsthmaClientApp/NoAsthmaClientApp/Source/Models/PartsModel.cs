using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NoAsthmaClientApp.Source.Models
{
    public class PartsModel
    {
        [DataNames("PartsID")]
        public int PartsID { get; set; }
        [DataNames("SupplierID")]
        public int SupplierID { get; set; }
        [DataNames("PartName")]
        public string PartName { get; set; }
        [DataNames("PartPrice")]
        public double PartPrice { get; set; }

    }
}
