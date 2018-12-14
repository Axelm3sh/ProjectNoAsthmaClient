using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NoAsthmaClientApp.Source.Models
{
    public class SupplierModel
    {
        [DataNames("SupplierID")]
        public int SupplierID { get; set; }
        [DataNames("AddressID")]
        public int AddressID { get; set; }
        [DataNames("SupplyName")]
        public string SupplyName { get; set; }
    }
}
