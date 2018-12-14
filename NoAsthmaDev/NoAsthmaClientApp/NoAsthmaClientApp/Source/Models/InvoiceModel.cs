using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NoAsthmaClientApp.Source.Models
{
    public class InvoiceModel
    {
        [DataNames("InvoiceID")]
        public int InvoiceID { get; set; }
        [DataNames("RepairJobID")]
        public int? RepairJobID { get; set; }
        [DataNames("ProductID")]
        public int ProductID { get; set; }
        [DataNames("TotalPrice")]
        public double TotalPrice { get; set; }
    }
}
