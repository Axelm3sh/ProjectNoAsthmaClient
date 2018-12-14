using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NoAsthmaClientApp.Source.Models
{
    public class ProductModel
    {
        [DataNames("ProductID")]
        public int ProductID { get; set; }
        [DataNames("SupplierID")]
        public int SupplierID { get; set; }
        [DataNames("AssemblyID")]
        public int? AssemblyID { get; set; } //Can be nullable
        [DataNames("ProductName")]
        public string ProductName { get; set; }
        [DataNames("ProductPrice")]
        public string ProductPrice { get; set; }
    }
}
